using TMPro;
using UnityEngine;

public class ScorePanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _bestScore;

    public void SetScore(int currentScore, int bestScore)
    {
        _score.text = currentScore.ToString();
        _bestScore.text = bestScore.ToString();
        
        SaveScore(currentScore);
    }

    private void SaveScore(int currentScore)
    {
        if (ScoreProgress.BestScore < currentScore)
        {
            ScoreProgress.BestScore = currentScore;
        }
    }
}