namespace RPS.Tests
{
    public class GameState
    {
        public GameState()
        {
            Status = GameStatus.None;
        }
        
        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event)
        {
            return new GameState
            {
                Status = GameStatus.ReadyToStart
            };
        }
        
        public GameState When(GameStarted @event)
        {
            return new GameState
            {
                Status = GameStatus.ReadyToStart
            };
        }
        
        public GameState When(RoundStarted @event)
        {
            return new GameState
            {
                Status = GameStatus.Started
            };
        }
        
        public GameState When(GameEnded @event)
        {
            return new GameState
            {
                Status = GameStatus.Ended
            };
        }
        
        public GameStatus Status { get; set; }
    }

}
