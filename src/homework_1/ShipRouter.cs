namespace homework_1
{
    public class ShipRouter : IRouter
    {
        private readonly Location _a;

        public ShipRouter(Location a)
        {
            _a = a;
        }
        
        public Location Route(string good)
        {
            return good == null ? null : _a;
        }
    }
}