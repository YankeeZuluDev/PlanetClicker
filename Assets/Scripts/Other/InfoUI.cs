using TMPro;
using UnityEngine;

/// <summary>
/// this class is responsible for updating score and score per second text
/// </summary>
public class InfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scorePerSecondText;
    [SerializeField] private string textFormat;

    public void UpdateScoreText(long score)
    {
        scoreText.text = $"score: {score.FormatHumanizeNumber(textFormat)}";
    }

    public void UpdateScorePerSecondText(long scorePerSecond)
    {
        scorePerSecondText.text = $"score/s: {scorePerSecond.FormatHumanizeNumber(textFormat)}";
    }
}
