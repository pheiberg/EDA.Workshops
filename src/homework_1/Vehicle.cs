using System;

namespace homework_1
{

    public class Vehicle
    {
        private readonly IRouter _router;

        public Vehicle(IRouter router, string name, Location origin)
        {
            _router = router;
            Name = name;
            Origin = origin;
        }
        
        public VehicleState State { get; private set; }
        public string Content { get; private set; }
        public Location Destination { get; private set; }
        public int Duration { get; private set; }
        public Location Origin { get; }
        public string Name { get; }
        
        public void Deliver()
        {
            switch (State)
            {
                case VehicleState.Driving:
                {
                    Duration--;
                    if (Duration == 0)
                    {
                        Unload();
                    }

                    break;
                }
                case VehicleState.Returning:
                {
                    Duration--;
                    if (Duration == 0)
                    {
                        State = VehicleState.Ready;
                    }

                    break;
                }
            }
        }
        
        private void Unload()
        {
            Destination.DropOff(Content);
            Content = null;
            State = VehicleState.Returning;
            Duration = Destination.Duration;
            Destination = null;
        }

        public void Load()
        {
            if (State != VehicleState.Ready)
                return;
            
            Content = Origin.PickUp();
            Destination = _router.Route(Content);

            if (Content == null)
                return;

            Duration = Destination.Duration;
            State = VehicleState.Driving;
        }
    }
}