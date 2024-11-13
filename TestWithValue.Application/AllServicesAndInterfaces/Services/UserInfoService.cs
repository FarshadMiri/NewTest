using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.UserInfo;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;

        public UserInfoService(IUserInfoRepository userInfoRepository, IMapper mapper)
        {
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
        }

        public async Task SaveUserInfoAsync(UserInfoViewModel model)
        {
            // تبدیل ویومدل به انتیتی
            var userInfo = _mapper.Map<Tbl_UserInfo>(model);

            // ذخیره اطلاعات در دیتابیس
            await _userInfoRepository.AddUserInfoAsync(userInfo);
        }

        public async Task<UserInfoViewModel> GetUserInfoAsync(string userId)
        {
            var userInfo = await _userInfoRepository.GetUserInfoByUserIdAsync(userId);
            return _mapper.Map<UserInfoViewModel>(userInfo);
        }
    }
}
