using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ViewModels.UserInfo;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IUserInfoService
    {
        Task SaveUserInfoAsync(UserInfoViewModel model);
        Task<UserInfoViewModel> GetUserInfoAsync(string userId);
    }
}
