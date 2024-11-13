using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly TestWithValueDbContext _context;

        public UserInfoRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task AddUserInfoAsync(Tbl_UserInfo userInfo)
        {
            try
            {
                await _context.tbl_UserInfos.AddAsync(userInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                inner.Trim();

            }
        }

        public async Task<Tbl_UserInfo> GetUserInfoByUserIdAsync(string userId)
        {
            return await _context.tbl_UserInfos.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
