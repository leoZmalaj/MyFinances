using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Memory;
using MyFinances.Contract.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Core.Login.LoginService
{
    public class LoginServices:ILoginService
    {
        public readonly ILoginRepository LoginRepository;
        public readonly IMemoryCache MemoryChache;
        public static bool IsLogin;
        public LoginServices(ILoginRepository loginRepository,IMemoryCache memoryCache)
        {
            LoginRepository = loginRepository;
            MemoryChache = memoryCache;
        }

        public async Task<bool> LoginAsync(Models.Login model)
        {
            if(!String.IsNullOrEmpty(model.UserName) || !String.IsNullOrEmpty(model.Password))
            {
                var result= await LoginRepository.LoginAsync(model);
                if(result>0)
                {
                    int userId=0;
                    bool exists = MemoryChache.TryGetValue("LogedUserId", out userId);
                    if (!exists)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                           .SetSlidingExpiration(TimeSpan.FromDays(1));
                        MemoryChache.Set("LogedUserId", result,cacheEntryOptions);
                    }
                    IsLogin = true;
                    return true;
                }
                
            }
            return false;
        }

        public void LogOutAsync()
        {
            MemoryChache.Remove("LogedUserId");
            LoginRepository.LogOutAsync();
            IsLogin = false;
        }
        public async Task<Response> RegisterAsync(Models.Login model)
        {
            if (!String.IsNullOrEmpty(model.UserName) || !String.IsNullOrEmpty(model.Password))
                return await LoginRepository.RegisterAsync(model);
            return Response.Error;
        }
    }
}
