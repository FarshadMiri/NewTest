using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface IUserInfoRepository
    {
        Task AddUserInfoAsync(Tbl_UserInfo userInfo);
        Task<Tbl_UserInfo> GetUserInfoByUserIdAsync(string userId);
    }
}
