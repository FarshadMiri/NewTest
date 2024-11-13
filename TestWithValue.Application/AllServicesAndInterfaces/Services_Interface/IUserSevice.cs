using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ViewModels.User;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface IUserSevice
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    }
}
