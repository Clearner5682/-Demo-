using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderService.Dtos;
using OrderService.EntityFrameworkCore;
using OrderService.Etos;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AbpControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;
        private readonly IObjectMapper objectMapper;
        private readonly OrderServiceDbContext dbContext;
        private readonly IDistributedEventBus distributedEventBus;
        private readonly OrderNumHelper orderNumHelper;

        public OrderController(
            ILogger<OrderController> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory,
            IObjectMapper objectMapper,
            OrderServiceDbContext dbContext,
            IDistributedEventBus distributedEventBus,
            OrderNumHelper orderNumHelper
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.clientFactory = clientFactory;
            this.objectMapper = objectMapper;
            this.dbContext = dbContext;
            this.distributedEventBus = distributedEventBus;
            this.orderNumHelper = orderNumHelper;
        }

        [Route("sendmessage")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            string queueName = "MyQueue001";
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = configuration["RabbitMQ:Connections:Default:HostName"];

            IConnection connection;
            try
            {
                connection = connectionFactory.CreateConnection();
                IModel channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, null);
                byte[] body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", queueName, null, body);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }

            return Ok(new { message = message });
        }

        [Route("addorder")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDto input)
        {
            if (string.IsNullOrWhiteSpace(input.GoodsName))
            {
                return BadRequest(new { Message = "GoodsName不能为空" });
            }
            if (input.Price < 0)
            {
                return BadRequest(new { Message="Price不能小于0"});
            }
            if(input.Amount < 0)
            {
                return BadRequest(new { Message = "Amount不能小于0" });
            }
            
            input.Id = GuidGenerator.Create();
            input.OrderNum = this.orderNumHelper.GenerateOrderNum();
            input.TotalMoney = input.Amount * input.Price;
            var order = this.objectMapper.Map<AddOrderDto, Order>(input);

            var tran = await this.dbContext.Database.BeginTransactionAsync();
            try 
            {
                // 保存订单
                order.CreationTime = DateTime.Now;
                this.dbContext.Orders.Add(order);

                // 保存本地消息
                var message = new Message(GuidGenerator.Create());
                message.Status = EnumeMessageStatus.Pending;
                message.CreationTime = DateTime.Now;
                message.EventId = order.Id;
                message.EventType = "OrderService.AddOrder";
                message.EventData = JsonConvert.SerializeObject(new AddOrderEto { OrderId=order.Id,GoodsName=order.GoodsName,Amount=order.Amount});
                //throw new Exception("保存本地消息失败了");
                this.dbContext.Messages.Add(message);

                // 同一个事务中处理
                await this.dbContext.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch(Exception ex)
            {
                await tran.RollbackAsync();

                return BadRequest(ex.Message);
            }

            // 向MQ写消息
            var addOrderEto = new AddOrderEto();
            addOrderEto.OrderId = order.Id;
            addOrderEto.GoodsName = order.GoodsName;
            addOrderEto.Amount = order.Amount;
            Task.Run(() =>
            {
                // 用于模拟订单保存成功，但是把消息发送给消息队列失败的情况
                throw new Exception("把消息发送给消息队列时失败了！！！");

                this.distributedEventBus.PublishAsync<AddOrderEto>(addOrderEto);
            });
            

            return Ok();
        }
    }
}
