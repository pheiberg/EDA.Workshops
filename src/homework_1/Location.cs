using System.Collections.Generic;

namespace homework_1
{
    public class Location
    {
        public Location(string name, int duration, IEnumerable<string> goods = null)
        {
            Name = name;
            Duration = duration;
            Goods = goods != null ? new Queue<string>(goods) : new Queue<string>();
        }

        private Queue<string> Goods { get; }
        public int Duration { get; }
        public string Name { get; }

        public int GoodsCount => Goods.Count;

        public string PickUp() => Goods.TryDequeue(out var result) ? result : null;

        public void DropOff(string good) => Goods.Enqueue(good);
    }
}