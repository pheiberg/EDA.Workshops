namespace homework_1
{
    public class ShipRouter : IRouter
    {
        private readonly Location _a;

        public ShipRouter(Location a)
        {
            _a = a;
        }
        
        public Location Route(Cargo goods)
        {
            return goods == null ? null : _a;
        }
    }
}