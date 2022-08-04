using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Dtos
{
    public class AddOrderDto
    {
        public Guid Id { get; set; }
        public string OrderNum { get; set; }
        // 假设一次只能买一种商品
        public string GoodsName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal TotalMoney { get; set; }
    }
}
