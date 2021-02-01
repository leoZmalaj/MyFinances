using Microsoft.AspNetCore.Http;
using MyFinances.Contract.Login;
using MyFinances.Core.Login.LoginService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Helper
{
    public class SessionHelper
    {
        //public readonly ILoginService LoginService;
        //public SessionHelper(ILoginService loginService)
        //{
        //    LoginService = loginService;
        //}
        public static bool IsLogin()
        {
            var res= LoginServices.IsLogin;
            return res;
        }
    }
}
