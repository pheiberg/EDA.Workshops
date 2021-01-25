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
                Status = GameStatus.Started
            };
        }
        public GameState When(GamePlayed @event)
        {
            return new GameState
            {
                Status = GameStatus.Ended
            };
        }

        public GameStatus Status { get; private set; }
        
        public enum GameStatus
        {
            None = 0,
            ReadyToStart = 10,
            Started = 20,
            Ended = 50
        }
    }

}
