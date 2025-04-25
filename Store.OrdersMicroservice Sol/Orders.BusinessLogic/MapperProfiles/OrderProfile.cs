using AutoMapper;
using Orders.BusinessLogic.Dtos;
using Orders.DataAccess.Entities;

namespace Orders.BusinessLogic.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddOrderDto, Order>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<AddOrUpdateOrderItemDto, OrderItem>();
        }
    }
}
