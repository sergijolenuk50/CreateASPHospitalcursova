using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class OrderService : IOrdersServices
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;
       // private readonly ICartService cartService;

        public OrderService(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            //this.cartService = cartService;
        }
        public void Create(string userId, int doctorId)
        {
            // create order
            var newOrder = new Order()
            {
                CreatedAt = DateTime.Now,
               // Products = cartService.GetProductsEntity(),
                UserId = userId,
               DoctorId = doctorId
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

           // cartService.Clear();
        }

        public List<OrderDto> GetOrders(string userId)
        {
            var orders = context.Orders.Include(x => x.User)
                                        .Include(x => x.Doctor)
                                        .Where(x => x.UserId == userId)
                                        .ToList();
            return mapper.Map<List<OrderDto>>(orders);
        }
    }
}

