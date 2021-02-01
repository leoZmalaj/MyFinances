using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Contract.Home
{
    public interface IHomeService
    {
        Task<IEnumerable<BudgetDto>> GetBudgetAsync(int userId);
        Task<bool> DeleteFinanceAsync(int id);
        Task<bool> CreateNewAsync(Budget model);
        Task<bool> UpdateBudgetAsync(Budget model);
    }
}
