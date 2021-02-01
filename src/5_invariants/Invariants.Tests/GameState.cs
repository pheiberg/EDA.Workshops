using System;

namespace Invariants.Tests
{
    public class GameState
    {
        public Guid Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int Round { get; set; }
        public int Rounds { get; set; }
        
        public GameState When(IEvent @event) => this;
        public GameState When(GameCreated @event)
        {
            return new GameState
            {
                Id = @event.GameId,
                Player1 = @event.PlayerId,
                Player2 = default,
                Round = 1,
                Rounds = @event.Rounds
            };
        }
    }

}
