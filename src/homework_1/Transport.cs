using System;
using System.Linq;

namespace homework_1
{
    public class Transport
    {
        private readonly IEventPublisher _eventPublisher;

        public Transport(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public int Execute(string[] goods)
        {
            var cargo = goods.Select((g, i) => new Cargo(i, "FACTORY", g));
            var factory = new Location("FACTORY", 0, cargo);
            var port = new Location("PORT", 1);
            var a = new Location("A", 4);
            var b = new Location("B", 5);

            var truckRouter = new TruckRouter(port, b);
            var lorry1 = new Vehicle(_eventPublisher, truckRouter, 0, "Lorry1", "TRUCK", factory);
            var lorry2 = new Vehicle(_eventPublisher, truckRouter, 1, "Lorry2", "TRUCK", factory);
            var ship = new Vehicle(_eventPublisher, new ShipRouter(a), 2, "Ship", "SHIP", port);

            var vehicles = new[] { lorry1, lorry2, ship };
            var time = 0;
            while (a.GoodsCount + b.GoodsCount != goods.Length)
            {
                foreach (var vehicle in vehicles)
                {
                    vehicle.Load(time);
                }
                foreach (var vehicle in vehicles)
                {
                    vehicle.Move(time);
                }

                time++;
            }
            
            return time;
    }
    }
}