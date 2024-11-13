using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync(); // داده‌ها به صورت IdentityUser بازگشت می‌شوند
    }
}
