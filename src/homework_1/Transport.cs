using System;

namespace homework_1
{
    public class Transport
    {
        public int Execute(string[] goods)
        {
            var factory = new Location("Factory", 0, goods);
            var port = new Location("Port", 1);
            var a = new Location("A", 4);
            var b = new Location("B", 5);

            var truckRouter = new TruckRouter(port, b);
            var lorry1 = new Vehicle(truckRouter, "Lorry1", factory);
            var lorry2 = new Vehicle(truckRouter, "Lorry2", factory);
            var ship = new Vehicle(new ShipRouter(a), "Ship", port);

            var vehicles = new[] { lorry1, lorry2, ship };
            var time = 0;
            while (a.GoodsCount + b.GoodsCount != goods.Length)
            {
                foreach (var vehicle in vehicles)
                {
                    vehicle.Load();
                }
                foreach (var vehicle in vehicles)
                {
                    vehicle.Move();
                }

                time++;
            }
            
            return time;
    }
    }
}