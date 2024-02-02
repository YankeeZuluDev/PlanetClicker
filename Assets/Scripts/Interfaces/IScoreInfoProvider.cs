public interface IScoreInfoProvider
{
    long Score { get; }
    long ScorePerSecond { get; }
    long ScorePerMouseClick { get; }
}
