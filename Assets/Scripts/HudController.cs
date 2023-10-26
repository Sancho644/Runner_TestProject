using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private ScorePanelController _scorePanel;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        ScoreCounterController.OnChanged += OnScoreChanged;
        
        ScoreCounterController.SetCount(0);
        
        _scoreValue.text = ScoreCounterController.CurrentScore.ToString();
        
        _playerController.OnEndGame += OnEndGame;
    }

    private void OnEndGame()
    {
        Debug.Log("endGame");
        _scorePanel.SetScore(ScoreCounterController.CurrentScore, ScoreProgress.BestScore);
        _scorePanel.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        ScoreCounterController.OnChanged -= OnScoreChanged;
        _playerController.OnEndGame -= OnEndGame;
    }

    private void OnScoreChanged()
    {
        _scoreValue.text = ScoreCounterController.CurrentScore.ToString();
    }
}