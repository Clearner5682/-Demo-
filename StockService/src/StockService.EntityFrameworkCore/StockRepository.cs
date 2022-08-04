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
    public class StockRepository : EfCoreRepository<StockServiceDbContext, Stock>, IStockRepository
    {
        public StockRepository(IDbContextProvider<StockServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
