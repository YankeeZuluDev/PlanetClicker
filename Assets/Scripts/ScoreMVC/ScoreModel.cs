using System;

namespace Score.Model
{
    /// <summary>
    /// This class is responsible for holding the data and handling business logic related to the score
    /// </summary>
    public class ScoreModel : IScoreInfoProvider
    {
        private long score;
        private long scorePerMouseClick;
        private long scorePerSecond;
        private bool allowAddingScoreEverySecond;

        // Callbacks to notify score changes
        public event Action OnScoreChanged;
        public event Action OnScorePerSecondAmountIncreased;
        public event Action OnScorePerClickAdded;

        public long Score => score;
        public long ScorePerSecond => scorePerSecond;
        public long ScorePerMouseClick => scorePerMouseClick;
        public bool AllowAddingScoreEverySecond { get => allowAddingScoreEverySecond; set => allowAddingScoreEverySecond = value; }

        public void Initialize(long score, long scorePerMouseClick, long scorePerSecond)
        {
            AddScore(score);
            IncreaseScorePerMouseClickAmount(scorePerMouseClick);
            IncreaseScorePerSecondAmount(scorePerSecond);
        }

        public void AddScore(long amount)
        {
            score += amount;
            OnScoreChanged?.Invoke();
        }

        public void RemoveScore(long amount)
        {
            score -= amount;
            OnScoreChanged?.Invoke();
        }

        public void IncreaseScorePerMouseClickAmount(long amount)
        {
            scorePerMouseClick += amount;
        }


        public void IncreaseScorePerSecondAmount(long amount)
        {
            scorePerSecond += amount;
            OnScorePerSecondAmountIncreased?.Invoke();
        }

        public void AddScorePerClick()
        {
            AddScore(scorePerMouseClick);
            OnScorePerClickAdded?.Invoke();
        }
    }
}
