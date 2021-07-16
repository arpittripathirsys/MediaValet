using AutoMapper;
using MediatR;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.RequestHandlers
{
    public class OrderRequestHandler : AsyncRequestHandler<Order>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly Random _magicNumberGenerator;
        private readonly IOrderIdGenerator _orderIdGenerator;

        public OrderRequestHandler(IMediator mediator, IMapper mapper, IOrderIdGenerator orderIdGenerator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _orderIdGenerator = orderIdGenerator;
            _magicNumberGenerator = new Random();
        }

        protected override async Task Handle(Order order, CancellationToken cancellationToken)
        {
            order.OrderId = _orderIdGenerator.GetNextOrderId();
            order.MagicNumber = _magicNumberGenerator.Next(1, 10);

            if (string.IsNullOrEmpty(order.OrderText))
                order.OrderText = $"Order# {order.OrderId} MagicNumber {order.MagicNumber}";

            OrderQueueItem orderQueueItem = new OrderQueueItem();
            _mapper.Map(order, orderQueueItem);
            await _mediator.Send(orderQueueItem);
            Console.WriteLine($"Send order {order.OrderId} with random number {order.MagicNumber}");
        }
    }
}
