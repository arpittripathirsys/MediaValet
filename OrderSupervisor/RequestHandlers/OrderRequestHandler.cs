using AutoMapper;
using MediatR;
using OrderSupervisor.Commands;
using OrderSupervisor.Generators.Interfaces;
using OrderSupervisor.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderSupervisor.RequestHandlers
{
    public class OrderRequestHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
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

        public async Task<OrderResponse> Handle(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
        {
            Order order = _mapper.Map<Order>(createOrderCommand.OrderRequest);

            order.OrderId = _orderIdGenerator.GetNextOrderId();
            order.MagicNumber = _magicNumberGenerator.Next(1, 10);

            if (string.IsNullOrEmpty(order.OrderText))
                order.OrderText = $"Order# {order.OrderId} MagicNumber {order.MagicNumber}";

            OrderQueueItem orderQueueItem = _mapper.Map<OrderQueueItem>(order);

            await _mediator.Send(orderQueueItem);

            Console.WriteLine($"Send order {order.OrderId} with random number {order.MagicNumber}");

            GetOrderConfirmationCommand command = new GetOrderConfirmationCommand(order.OrderId);

            var orderConfirmation = await _mediator.Send(command);

            OrderResponse orderResponse = _mapper.Map<OrderResponse>(orderConfirmation);

            return orderResponse;
        }
    }
}
