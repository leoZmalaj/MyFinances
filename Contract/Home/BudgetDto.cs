using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Contract.Home
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CameFrom { get; set; }
        public DateTime? DateTake { get; set; }
        public DateTime DateInsert { get; set; }
        public decimal Amounts { get; set; }
        public DateTime? UpdateDate { get; set; }
        public decimal AmountSpent { get; set; }

        public string SpentOn { get; set; }
        public decimal AmountLeft { get; set; }
        public decimal TotalAmounts { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public decimal TotalAmountLeft { get; set; }
    }
   
}
