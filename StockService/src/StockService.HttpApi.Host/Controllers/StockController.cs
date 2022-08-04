using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace StockService.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : AbpControllerBase
    {
        private readonly IOrderCheckRepository orderCheckRepository;

        public StockController(IOrderCheckRepository orderCheckRepository)
        {
            this.orderCheckRepository = orderCheckRepository;
        }

        // 提供一个校准接口给OrderService来校准消息执行结果
        [Route("ordercheck")]
        [HttpGet]
        public async Task<IActionResult> OrderCheck(Guid orderId)
        {
            var orderCheck = await this.orderCheckRepository.FindAsync(o => o.OrderId == orderId);
            if (orderCheck != null)
            {
                return Ok(orderCheck);
            }

            return Ok();
        }
    }
}
