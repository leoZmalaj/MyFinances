using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Contract.Login
{
     public interface ILoginService
    {
        Task<bool> LoginAsync(Models.Login model);
        void LogOutAsync();
        Task<Response> RegisterAsync(Models.Login model);
    }
}
