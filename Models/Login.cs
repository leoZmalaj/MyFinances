using System;
using System.Collections.Generic;

#nullable disable

namespace MyFinances.Models
{
    public partial class Login
    {
        public Login()
        {
            Budgets = new HashSet<Budget>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
