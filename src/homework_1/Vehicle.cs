namespace homework_1
{

    public class Vehicle
    {
        private readonly IRouter _router;

        public Vehicle(IRouter router, string name, Location origin)
        {
            _router = router;
            Name = name;
            State = new ReadyState(origin);
        }

        private VehicleState State { get; set; }
        public string Name { get; }

        public void Move()
        {
            State = State.Move();
        }
        
        public void Load()
        {
            State = State.Load(_router);
        }

        private abstract class VehicleState
        {
            protected Location Origin { get; init; }
            
            public abstract VehicleState Load(IRouter router);
            public abstract VehicleState Move();
        }

        private class DrivingState : VehicleState
        {
            private string Content { get; }
            private Location Destination { get; }
            private int Duration { get; }
            
            public DrivingState(Location origin, Location destination, int duration, string content)
            {
                Duration = duration;
                Origin = origin;
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
                    return new DrivingState(Origin, Destination, Duration - 1, Content);
                
                Destination.DropOff(Content);
                return new ReturningState(Origin, Destination.Duration);
            }
        }

        private class ReadyState : VehicleState
        {
            public ReadyState(Location origin)
            {
                Origin = origin;
            }
            
            public override VehicleState Load(IRouter router)
            {
                var content = Origin.PickUp();
                if (content == null)
                    return this;
                
                var destination = router.Route(content);
                return new DrivingState(Origin, destination, destination.Duration, content);
            }

            public override VehicleState Move()
            {
                return this;
            }
        }
        
        private class ReturningState : VehicleState
        {
            private int Duration { get; }
            
            public ReturningState(Location origin, int duration)
            {
                Duration = duration;
                Origin = origin;
            }
            
            public override VehicleState Load(IRouter router)
            {
                return this;
            }

            public override VehicleState Move()
            {
                if (Duration - 1 == 0)
                    return new ReadyState(Origin);
                
                return new ReturningState(Origin, Duration - 1);
            }
        }
    }
}