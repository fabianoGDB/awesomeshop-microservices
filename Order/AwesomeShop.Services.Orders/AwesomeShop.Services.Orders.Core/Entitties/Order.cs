using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Core.Enums;
using AwesomeShop.Services.Orders.Core.Events;
using AwesomeShop.Services.Orders.Core.ValueObjects;

namespace AwesomeShop.Services.Orders.Core.Entitties
{
    public class Order : AggregateRoot
    {
        public Order(Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress, PaymentInfo paymentInfo, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            TotalPrice = items.Sum(x => x.Quantity * x.Price);
            CreatedAt = DateTime.Now;
            Customer = customer;
            DeliveryAddress = deliveryAddress;
            PaymentAddress = paymentAddress;
            PaymentInfo = paymentInfo;
            Items = items;

            AddEvent(new OrderCreated(Id, TotalPrice, paymentInfo, Customer.FullName, Customer.Email));

        }

        public decimal TotalPrice { get; private set; }
        public Customer Customer { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }
        public PaymentAddress PaymentAddress { get; private set; }
        public PaymentInfo PaymentInfo { get; set; }

        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }

        public void SetAsCompleted()
        {
            Status = OrderStatus.Completed;
        }

        public void SetAsRejected()
        {
            Status = OrderStatus.Rejected;
        }


    }
}