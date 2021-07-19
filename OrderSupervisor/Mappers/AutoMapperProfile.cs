using AutoMapper;
using OrderSupervisor.Models;

namespace OrderSupervisor.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderRequest, Order>();
            //.AfterMap((src, dest) =>
            //{
            //    dest.OrderText = src.OrderText;
            //});

            CreateMap<Order, OrderQueueItem>();
            //.AfterMap((src, dest) =>
            //{
            //    dest.OrderId = src.OrderId;
            //    dest.MagicNumber = src.MagicNumber;
            //    dest.OrderText = src.OrderText;
            //});

            CreateMap<OrderConfirmation, OrderResponse>();
                //.AfterMap((src, dest) =>
                //{
                //    dest.OrderId = src.OrderId;
                //    dest.AgentId = src.AgentId;
                //});
        }
    }
}
