using StockService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace StockService
{
    public class OrderCheckRepository : EfCoreRepository<StockServiceDbContext, OrderCheck>, IOrderCheckRepository
    {
        public OrderCheckRepository(IDbContextProvider<StockServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> IsDuplicateOrder(Guid orderId)
        {
            try
            {
                var entity = new OrderCheck(GuidGenerator.Create());
                entity.OrderId = orderId;
                entity.CreationTime = DateTime.Now;
                await this.InsertAsync(entity,true);
            }
            catch(Exception ex)
            {
                return true;
            }

            return false;
        }
    }
}
