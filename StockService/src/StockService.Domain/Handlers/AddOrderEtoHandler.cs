using OrderService.Etos;
using StockService.Etos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace StockService.EtoHandlers
{
    public class AddOrderEtoHandler : IDistributedEventHandler<AddOrderEto>, ITransientDependency
    {
        private readonly IStockRepository stockRepository;
        private readonly IOrderCheckRepository orderCheckRepository;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly IDistributedEventBus distributedEventBus;

        public AddOrderEtoHandler(
            IStockRepository stockRepository,
            IOrderCheckRepository orderCheckRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus
            )
        {
            this.stockRepository = stockRepository;
            this.orderCheckRepository = orderCheckRepository;
            this.unitOfWorkManager = unitOfWorkManager;
            this.distributedEventBus = distributedEventBus;
        }

        public async Task HandleEventAsync(AddOrderEto eventData)
        {
            if (await this.orderCheckRepository.IsDuplicateOrder(eventData.OrderId))
            {
                // 说明该订单已经消费过了，直接确认消息
                // 什么也不做（自动确认）
                return;
            }

            Stock stock=null;
            OrderCheck orderCheck=null;
            try
            {
                stock = await this.stockRepository.GetAsync(o => o.GoodsName == eventData.GoodsName);
                orderCheck = await this.orderCheckRepository.FindAsync(o => o.OrderId == eventData.OrderId);
                if (stock != null)
                {
                    stock.Amount = stock.Amount - eventData.Amount;
                    if (stock.Amount < 0)
                    {
                        throw new Exception("库存不足");
                    }
                    using (var uow = this.unitOfWorkManager.Begin())
                    {
                        await this.stockRepository.UpdateAsync(stock);
                        if (orderCheck != null)
                        {
                            orderCheck.ProcessResult = EnumProcessResult.Succeed;
                            orderCheck.LastModificationTime = DateTime.Now;
                            await this.orderCheckRepository.UpdateAsync(orderCheck);
                        }

                        await uow.CompleteAsync();
                    }
                }
            }
            catch (Exception ex) 
            {
                if(orderCheck != null)
                {
                    orderCheck.ProcessResult = EnumProcessResult.Failed;
                    orderCheck.LastModificationTime= DateTime.Now;
                    await this.orderCheckRepository.UpdateAsync(orderCheck);
                }
            }
            finally
            {
                var updateStockEto = new UpdateStockEto();
                updateStockEto.OrderId = eventData.OrderId;
                updateStockEto.IsSuccess=(orderCheck != null&&orderCheck.ProcessResult==EnumProcessResult.Succeed);
                this.distributedEventBus.PublishAsync<UpdateStockEto>(updateStockEto);// 把库存扣减的结果再通过消息队列告诉OrderService（尽最大努力通知）
            }
        }
    }
}
