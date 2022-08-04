using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace OrderService.Etos
{
    [EventName("OrderService.AddOrder")]
    public class AddOrderEto
    {
        // OrderId用于库存服务做幂等校验
        public Guid OrderId { get; set; }
        // 假设一次只能买一种商品
        public string GoodsName { get; set; }
        public int Amount { get; set; }
    }
}
