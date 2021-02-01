using MyFinances.Contract.Home;
using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Core.Home.Service
{
    public class HomeServices : IHomeService
    {
        public IHomeRepository HomeRepository { get; set; }
        public HomeServices(IHomeRepository homeRepository)
        {
            HomeRepository = homeRepository;
        }
        public async Task<IEnumerable<BudgetDto>> GetBudgetAsync(int userId)
        {
            var list = new List<BudgetDto>();
            var result = await HomeRepository.GetBudgetAsync(userId);
            decimal TotalAmountLeft = 0;
            decimal TotalAmounts = 0;
            decimal TotalAmountSpent = 0;
            foreach (var item in result)
            {
                TotalAmountLeft = TotalAmountLeft + item.AmountLeft;
                TotalAmounts = TotalAmounts + item.Amounts;
                TotalAmountSpent = TotalAmountSpent + item.AmountSpent;
                list.Add(new BudgetDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    CameFrom = item.CameFrom,
                    DateTake = item.DateTake,
                    DateInsert = item.DateInsert,
                    Amounts = item.Amounts,
                    UpdateDate = item.UpdateDate,
                    AmountSpent = item.AmountSpent,
                    SpentOn = item.SpentOn,
                    AmountLeft = item.AmountLeft,
                    TotalAmountLeft=TotalAmountLeft,
                    TotalAmounts=TotalAmounts,
                    TotalAmountSpent=TotalAmountSpent
                });

            }

        
            return list;
        }

        public async Task<bool> DeleteFinanceAsync(int id)
        {
            return await HomeRepository.DeleteFinanceAsync(id);
        }

        public async Task<bool> CreateNewAsync(Budget model)
        {
            return await HomeRepository.CreateNewAsync(model);
        }

        public async Task<bool> UpdateBudgetAsync(Budget model)
        {
            if (model.AmountSpent <= 0 || !String.IsNullOrEmpty(model.CameFrom))
                return await HomeRepository.UpdateBudgetAsync(model);
            return false;
        }
    }
}
