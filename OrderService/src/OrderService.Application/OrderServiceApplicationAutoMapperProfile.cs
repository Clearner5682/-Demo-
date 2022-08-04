using AutoMapper;
using AutoMapper.Internal.Mappers;
using OrderService.Dtos;

namespace OrderService;

public class OrderServiceApplicationAutoMapperProfile : Profile
{
    public OrderServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<AddOrderDto, Order>();
    }
}
