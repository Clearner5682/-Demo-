using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace StockService.Etos
{
    [EventName("StockService.UpdateStock")]
    public class UpdateStockEto
    {
        public Guid OrderId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
