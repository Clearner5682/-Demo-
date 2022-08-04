using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace StockService
{
    // 订单检查（幂等）+ 处理结果（成功or失败）
    public class OrderCheck:Entity<Guid>
    {
        public OrderCheck(Guid id)
        {
            this.Id = id;
            this.ProcessResult = EnumProcessResult.Pending;
        }

        public Guid OrderId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public EnumProcessResult ProcessResult { get; set; }
    }
}
