using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.OperationResult;
using TestWithValue.Domain.ViewModels.CartItem;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartItemService(ICartItemRepository cartItemRepository,IMapper mapper)
        {
            _cartRepository = cartItemRepository;
            _mapper = mapper;
                
        }
        public async Task AddToCart(CartItemViewModel cartItemViewModel)
        {
            var cartItem = new Tbl_CartItem()
            {
                TopicId = cartItemViewModel.TopicId,
                UserId = cartItemViewModel.UserId, 

            };
           await _cartRepository.AddToCart(cartItem);
            

        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItemsByUserId(int userId)
        {
            var cartItems = await _cartRepository.GetCartItemsByUserId(userId);
            var cartItemVM=_mapper.Map<IEnumerable<CartItemViewModel>>(cartItems);

            return cartItemVM;
        }

        public async Task RemoveFromCart(int cartItemId)
        {
            await _cartRepository.RemoveFromCart(cartItemId);
        }
    }
}
