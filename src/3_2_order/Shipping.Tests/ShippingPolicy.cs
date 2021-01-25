﻿using System;
using System.Collections.Generic;

namespace Shipping.Tests
{
    public class App
    {
        List<IEvent> history = new List<IEvent>();

        public void Given(params IEvent[] events) => history.AddRange(events);

        public void When(IEvent @event)
        {
            var cmd = ShippingPolicy.When((dynamic)@event);
            var state = history.Rehydrate<Order>();
            history.AddRange(OrderBehavior.Handle(state, (dynamic)cmd));
        }

        public void Then(Action<IEvent[]> f)
            => f(history.ToArray());
    }


    public class ShippingPolicy
    {
        public static ICommand When(PaymentRecieved @event) => new CompletePayment();
        public static ICommand When(GoodsPicked @event) => new CompletePacking();
    }

    public static class OrderBehavior
    {
        public static IEnumerable<IEvent> Handle(this Order order, CompletePayment command)
        {
            yield return new PaymentRecieved();
            if (order.Packed)
                yield return new GoodsShipped();
        }
            

        public static IEnumerable<IEvent> Handle(this Order order, CompletePacking command)
        {
            yield return new PackingComplete();
            if (order.Payed)
                yield return new GoodsShipped();
        }
    }

    public class Order
    {
        public bool Payed;
        public bool Packed;

        public Order When(IEvent @event) => this;

        public Order When(PaymentRecieved @event)
        {
            return new Order
            {
                Payed = true,
                Packed = Packed
            };
        }
        public Order When(GoodsPicked @event)
        {
            return new Order
            {
                Payed = Payed,
                Packed = true
            };
        }
    }
}
