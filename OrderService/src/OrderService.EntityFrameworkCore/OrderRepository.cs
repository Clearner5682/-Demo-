using OrderService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OrderService
{
    public class OrderRepository : EfCoreRepository<OrderServiceDbContext, Order>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
