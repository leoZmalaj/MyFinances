using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyFinances.Core.Login.LoginRepository.LoginRepositories;

namespace MyFinances.Contract.Login
{
    public interface ILoginRepository
    {
        Task<int> LoginAsync(Models.Login model);
        Task<Response> RegisterAsync(Models.Login model);
        void LogOutAsync();
    }
}
