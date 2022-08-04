using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace OrderService
{
    public class Order:Entity<Guid>
    {
        public string OrderNum { get; set; }
        // 假设一次只能买一种商品
        public string GoodsName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
