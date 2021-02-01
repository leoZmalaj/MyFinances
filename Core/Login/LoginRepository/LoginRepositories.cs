using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyFinances.Contract.Login;
using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyFinances.Core.Login.LoginRepository
{
    public class LoginRepositories : MyFinancesContext, ILoginRepository
    {
        private readonly IDataProtector Protector;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginRepositories(IDataProtectionProvider protector, IHttpContextAccessor httpContextAccessor)
        {
            Protector = protector.CreateProtector("Password");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> LoginAsync(Models.Login model)
        {
              var exists = await base.Logins.AnyAsync(x => x.UserName == model.UserName);
                if (exists)
                {
                    var pass = await base.Logins.FirstAsync(x => x.UserName == model.UserName);
                    var decode = Protector.Unprotect(pass.Password);
                    if (decode.Trim() == model.Password.Trim())
                    {
                        var isLogin = new List<Claim> {

                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim("Password", model.Password)

                    };
                        var identity = new ClaimsIdentity(isLogin, "LogedIn");
                        var userPrincipal = new ClaimsPrincipal(new[] { identity });
                        await _httpContextAccessor.HttpContext.SignInAsync(userPrincipal);
                        return pass.Id;
                    }
                    return 0;
                }
                return 0;
        }

        public  void LogOutAsync()
        {
             _httpContextAccessor.HttpContext.Response.Cookies.Delete("LogIn");
        }

        public async Task<Response> RegisterAsync(Models.Login model)
        {
                var exists = await base.Logins.AnyAsync(x => x.UserName == model.UserName);
                if (!exists)
                {
                    model.Password = Protector.Protect(model.Password);
                    await base.Logins.AddAsync(model);
                    var sucess = await base.SaveChangesAsync();
                    if (sucess > 0)
                        return Response.Sucess;
                    return Response.Error;
                }
                else
                {
                    return Response.Exists;
                }
                return Response.Error;
        }
    }
}
