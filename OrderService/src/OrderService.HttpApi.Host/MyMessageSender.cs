using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderService.Etos;
using Quartz;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.EventBus.Distributed;

namespace OrderService
{
    public class MyMessageSender : QuartzBackgroundWorkerBase
    {
        private readonly ILogger<MyMessageSender> logger;
        private readonly IMessageRepository messageRepository;
        private readonly IDistributedEventBus distributedEventBus;

        public MyMessageSender(ILogger<MyMessageSender> logger,IMessageRepository messageRepository,IDistributedEventBus distributedEventBus)
        {
            this.logger = logger;
            this.messageRepository = messageRepository;
            this.distributedEventBus = distributedEventBus;
            JobDetail = JobBuilder.Create<MyMessageSender>().WithIdentity(nameof(MyMessageSender)).Build();
            Trigger = TriggerBuilder.Create().WithIdentity(nameof(MyMessageSender)).WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();
            ScheduleJob = async scheduler =>
            {
                if (!await scheduler.CheckExists(JobDetail.Key))
                {
                    await scheduler.ScheduleJob(JobDetail, Trigger);
                }
            };
        }

        // 作为补偿，定时任务扫描Pending状态的消息，然后把消息发送给消息队列
        // 防止订单创建成功，但是把消息丢到消息队列时消息队列挂了或网络原因导致消息没有被消息队列保存
        // 这是实现可靠消息的关键
        // 正因为有定时任务不断往队列发送消息，所以导致消息可能会重复的发给消费者，所以消费者端必须要做幂等
        public override async Task Execute(IJobExecutionContext context)
        {
            this.logger.LogWarning($"MyMessageSender Executed:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

            var pendingMessages=await this.messageRepository.GetListAsync(o=>o.Status==EnumeMessageStatus.Pending);
            foreach(var message in pendingMessages)
            {
                var addOrderEto = JsonConvert.DeserializeObject<AddOrderEto>(message.EventData);
                this.distributedEventBus.PublishAsync<AddOrderEto>(addOrderEto);
            }
        }
    }
}
