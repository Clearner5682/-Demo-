using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace OrderService
{
    // 本地消息
    public class Message:Entity<Guid>
    {
        public Message(Guid id)
        {
            this.Id = id;
        }

        // 事件Id（比如订单Id）
        public Guid EventId { get; set; }
        // 事件类型
        public string EventType { get; set; }
        // 事件数据
        public string EventData { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public EnumeMessageStatus Status { get; set; }
    }
}
