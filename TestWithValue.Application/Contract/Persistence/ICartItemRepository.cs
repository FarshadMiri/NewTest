using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ICartItemRepository
    {
        Task<bool> AddToCart(Tbl_CartItem item);
        Task<bool> RemoveFromCart(int cartItemId);
        Task<IEnumerable<Tbl_CartItem>> GetCartItemsByUserId(int userId);
    }
}
