using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Services.Data
{
    public interface IUserService
    {
        T GetUser<T>(string userId);
    }
}
