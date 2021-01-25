using System.Collections.Generic;
using System.Linq;

namespace RPS.Tests
{
    public class HighScoreView
    {
        private GameState _state;

        public HighScoreView()
        {
            _state = new GameState();
        }
        
        public HighScoreView When(IEvent @event) => this;

        public HighScoreView When(GameCreated @event)
        {
            _state.RoundsToPlay = @event.Rounds;
        }
        
        public HighScoreView When(RoundEnded @event)
        {
            @event.Looser 
            
        }
        
        public HighScoreView When(GameEnded @event)
        {
            
            Rows = Rows.Concat(new[]
            {
                new ScoreRow(),
                new ScoreRow()
            }).ToArray();
        }
        
        public ScoreRow[] Rows { get; set; }
        public class ScoreRow
        {
            public int Rank { get; set; }
            public string PlayerId { get; set; }
            public int GamesWon { get; set; }
            public int RoundsWon { get; set; }
            public int GamesPlayed { get; set; }
            public int RoundsPlayed { get; set; }
        }

        private class GameState
        {
            public int RoundsToPlay { get; set; }
            public int Round { get; set; }
            public List<string> Winners { get; set; }
        }
    }
}
