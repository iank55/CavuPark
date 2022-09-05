using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavuParkDAL
{
    public interface IPricesRepository
    {
        Task<IEnumerable<Prices>> GetPrices(DateTime dateFrom, DateTime dateTo);
    }
}
