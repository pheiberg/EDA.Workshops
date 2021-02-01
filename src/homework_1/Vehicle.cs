namespace homework_1
{

    public class Vehicle
    {
        private readonly IRouter _router;
        public string Kind { get; }
        public int Id { get; }
        private VehicleState State { get; set; }
        public string Name { get; }
        public int Time { get; private set; }
        
        public Vehicle(IEventPublisher eventPublisher, IRouter router, int id, string name, string kind, Location origin)
        {
            _router = router;
            Kind = kind;
            Id = id;
            Name = name; 
            State = new ReadyState(this, eventPublisher, origin);
            Time = 0;
        }

        public void Move(int time)
        {
            Time = time;
            State = State.Move();
        }
        
        public void Load(int time)
        {
            Time = time;
            State = State.Load(_router);
        }

        private abstract class VehicleState
        {
            protected readonly Vehicle Vehicle;
            protected IEventPublisher EventPublisher { get; }
            protected Location Origin { get; }

            protected VehicleState(Vehicle vehicle, IEventPublisher publisher, Location origin)
            {
                Vehicle = vehicle;
                EventPublisher = publisher;
                Origin = origin;
            }
            
            public abstract VehicleState Load(IRouter router);
            public abstract VehicleState Move();
        }

        private class DrivingState : VehicleState
        {
            private Cargo Content { get; }
            private Location Destination { get; }
            private int Duration { get; }
            
            public DrivingState(Vehicle vehicle, IEventPublisher eventPublisher, Location origin, Location destination, int duration, Cargo content) 
                : base(vehicle, eventPublisher, origin)
            {
                Duration = duration;
                Destination = destination;
                Content = content;
            }
            
            public override VehicleState Load(IRouter router)
            {
                return this;
            }

            public override VehicleState Move()
            {
                if (Duration - 1 != 0) 
                    return new DrivingState(Vehicle, EventPublisher, Origin, Destination, Duration - 1, Content);
                
                EventPublisher.Publish(new Arrived
                {
                    Time = Vehicle.Time + 1,
                    TransportId = Vehicle.Id,
                    Kind = Vehicle.Kind,
                    Location = Destination.Name,
                    Cargo = new []{ 
                        new CargoInfo
                        {
                            CargoId = Content.Id,
                            Destination = Content.Destination,
                            Origin = Content.Origin
                        }
                    }
                });
                
                Destination.DropOff(Content);
                
                EventPublisher.Publish(new Departed
                {
                    Time = Vehicle.Time + 1,
                    TransportId = Vehicle.Id,
                    Kind = Vehicle.Kind,
                    Location = Destination.Name,
                    Destination = Origin.Name
                });
                return new ReturningState(Vehicle, EventPublisher, Origin, Destination.Duration);
            }
        }

        private class ReadyState : VehicleState
        {
            public ReadyState(Vehicle vehicle, IEventPublisher eventPublisher, Location origin) 
                : base(vehicle, eventPublisher, origin)
            {
            }
            
            public override VehicleState Load(IRouter router)
            {
                var content = Origin.PickUp();
                if (content == null)
                    return this;
                
                var destination = router.Route(content);
                EventPublisher.Publish(new Departed
                {
                    Time = Vehicle.Time,
                    TransportId = Vehicle.Id,
                    Kind = Vehicle.Kind,
                    Location = Origin.Name,
                    Destination = destination.Name,
                    Cargo = new [] { 
                        new CargoInfo
                        {
                            CargoId = content.Id,
                            Destination = content.Destination,
                            Origin = content.Origin
                        }
                    }
                });
                return new DrivingState(Vehicle, EventPublisher, Origin, destination, destination.Duration, content);
            }

            public override VehicleState Move()
            {
                return this;
            }
        }
        
        private class ReturningState : VehicleState
        {
            private int Duration { get; }
            
            public ReturningState(Vehicle vehicle, IEventPublisher eventPublisher, Location origin, int duration) 
                : base(vehicle, eventPublisher, origin)
            {
                Duration = duration;
            }
            
            public override VehicleState Load(IRouter router)
            {
                return this;
            }

            public override VehicleState Move()
            {
                if (Duration - 1 > 0) 
                    return new ReturningState(Vehicle, EventPublisher, Origin, Duration - 1);
                
                EventPublisher.Publish(new Arrived
                {
                    Time = Vehicle.Time + 1,
                    TransportId = Vehicle.Id,
                    Kind = Vehicle.Kind,
                    Location = Origin.Name
                });
                return new ReadyState(Vehicle, EventPublisher, Origin);

            }
        }
    }
}