using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.Tests
{
    public class ShippingPolicy
    {
        public static ICommand When(PaymentRecieved @event, Order state)
        {
            return Ship(state.When(@event));
        }
        public static ICommand When(GoodsPicked @event, Order state)
        {
            return Ship(state.When(@event));
        }

        private static ICommand Ship(Order state)
        {
            return state.Packed && state.Payed ? new Ship() : null;
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
