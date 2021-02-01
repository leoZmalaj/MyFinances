using Microsoft.EntityFrameworkCore;
using MyFinances.Contract.Home;
using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Core.Home.Repository
{
    public class HomeRepositories : MyFinancesContext, IHomeRepository
    {
        public async Task<bool> CreateNewAsync(Budget model)
        {
            if (model.AmountSpent > model.Amounts)
                return false;
            if (model.AmountSpent > 0)
                model.AmountLeft = model.Amounts - model.AmountSpent;
            model.UpdateDate = DateTime.Now;
            await base.Budgets.AddAsync(model);
            var result = await base.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteFinanceAsync(int id)
        {
            var element = await base.Budgets.SingleOrDefaultAsync(x => x.Id == id);
            if (element != null)
            {

                base.Budgets.Remove(element);
                var result = await base.SaveChangesAsync();
                if (result > 0)
                    return true;

                return false;
            }
            return false;
        }

        public async Task<IEnumerable<Budget>> GetBudgetAsync(int userId)
        {
            return await base.Budgets.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateBudgetAsync(Budget model)
        {

            if (model.AmountLeft < model.AmountSpent)
                return false;
            model.AmountLeft = model.AmountLeft - model.AmountSpent;
            base.Budgets.Attach(model);
            base.Entry(model).Property(x => x.CameFrom).IsModified = true;
            base.Entry(model).Property(x => x.DateTake).IsModified = true;
            base.Entry(model).Property(x => x.AmountLeft).IsModified = true;
            base.Entry(model).Property(x => x.AmountSpent).IsModified = true;
            base.Entry(model).Property(x => x.SpentOn).IsModified = true;
            var res = await base.SaveChangesAsync();
            if (res > 0)
                return true;
            return false;
        }
    }
}
