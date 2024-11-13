using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.ViewModels.User;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
	public class UserService : IUserSevice
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
		{


			var users = await _userRepository.GetAllUsersAsync();

			// فرض کنید این متد کاربر را برمی‌گرداند

			// تبدیل داده‌ها به ViewModel
			var uservm = _mapper.Map<IEnumerable<UserViewModel>>(users);



			return uservm;


		}




	}
}

