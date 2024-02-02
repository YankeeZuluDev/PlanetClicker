public interface IScoreController
{
    void IncreaseScorePerMouseClickAmount(long amount);
    void IncreaseScorePerSecondAmount(long amount);
    void RemoveScore(long amount);
}
