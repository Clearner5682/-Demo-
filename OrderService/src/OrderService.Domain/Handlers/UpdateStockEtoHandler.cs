using StockService.Etos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace OrderService.EtoHandlers
{
    public class UpdateStockEtoHandler : IDistributedEventHandler<UpdateStockEto>, ITransientDependency
    {
        private readonly IMessageRepository messageRepository;

        public UpdateStockEtoHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task HandleEventAsync(UpdateStockEto eventData)
        {
            // 接收订单的库存扣减结果
            // 修改本地消息表中的消息状态（既然消费方已经消费完创建订单的消息了，就不要再继续发了）
            var message = await this.messageRepository.FindAsync(o => o.EventId == eventData.OrderId&&o.Status==EnumeMessageStatus.Pending);
            if (message != null)
            {
                message.Status = eventData.IsSuccess ? EnumeMessageStatus.Succeed : EnumeMessageStatus.Failed;
                message.LastModificationTime = DateTime.Now;
                await this.messageRepository.UpdateAsync(message);
            }

            // 按照尽最大努力通知的要求
            // StockService除了通过消息队列通知库存扣减结果外，还需要提供一个库存扣减结果查询的接口（供OrderService来校准执行结果）
        }
    }
}
