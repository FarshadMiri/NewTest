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
    public class CartItemRepository : ICartItemRepository
    {
        private readonly TestWithValueDbContext _context;
        public CartItemRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCart(Tbl_CartItem item)
        {
            try
            {
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var inner =  ex.InnerException.Message;
                return false;
            }
        }

        public async Task<IEnumerable<Tbl_CartItem>> GetCartItemsByUserId(int userId)
        {
            return await _context.tbl_CartItems
                            .Include(c => c.Topic)
                            .Where(c => c.UserId == userId)
                            .ToListAsync();
        }



        public async Task<bool> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.tbl_CartItems.FindAsync(cartItemId);
            try
            {
                if (cartItem != null)
                {
                    _context.tbl_CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;  // در صورتی که cartItem وجود نداشته باشد
        }

    }
}
