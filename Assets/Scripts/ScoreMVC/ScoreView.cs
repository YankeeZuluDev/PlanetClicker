using Zenject;

namespace Score.View
{
    /// <summary>
    /// This class is responsible for presenting the data related to score
    /// </summary>
    public class ScoreView
    {
        private IScoreTextSpawner scoreTextSpawner;
        private InfoUI infoUI;

        [Inject]
        private void Construct(IScoreTextSpawner scoreTextSpawner, InfoUI infoUI)
        {
            this.scoreTextSpawner = scoreTextSpawner;
            this.infoUI = infoUI;
        }

        public void UpdateScoreText(long score)
        {
            infoUI.UpdateScoreText(score);
        }

        public void UpdateScorePerSecondText(long scorePerSecond)
        {
            infoUI.UpdateScorePerSecondText(scorePerSecond);
        }

        public void SpawnAndAnimateScorePerSecondText(long scorePerSecond)
        {
            scoreTextSpawner.SpawnAndAnimateScorePerSecondText(scorePerSecond);
        }

        public void SpawnAndAnimateClickScoreText(long scorePerClick)
        {
            scoreTextSpawner.SpawnAndAnimateClickScoreText(scorePerClick);
        }
    }
}
