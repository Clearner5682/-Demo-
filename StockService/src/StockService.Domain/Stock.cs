using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace StockService
{
    public class Stock:Entity<Guid>
    {
        // 商品名称
        public string GoodsName { get; set; }
        // 库存
        public int Amount { get; set; }
    }
}
