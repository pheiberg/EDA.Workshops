using System.Collections.Generic;

namespace homework_1
{
    public class Location
    {
        public Location(string name, int duration, IEnumerable<Cargo> cargo = null)
        {
            Name = name;
            Duration = duration;
            Cargo = cargo != null ? new Queue<Cargo>(cargo) : new Queue<Cargo>();
        }

        private Queue<Cargo> Cargo { get; }
        public int Duration { get; }
        public string Name { get; }

        public int GoodsCount => Cargo.Count;

        public Cargo PickUp() => Cargo.TryDequeue(out var result) ? result : null;

        public void DropOff(Cargo cargo) => Cargo.Enqueue(cargo);
    }
}