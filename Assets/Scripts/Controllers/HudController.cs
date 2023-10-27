using Data;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private ScorePanelController _scorePanel;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MainMenuButtonController _mainMenuButtonController;

        private void Start()
        {
            ScoreCounterController.OnChanged += OnScoreChanged;

            ScoreCounterController.SetCount(0);

            _scoreValue.text = ScoreCounterController.CurrentScore.ToString();

            _playerController.OnEndGame += OnEndGame;
            _mainMenuButtonController.OnStartGame += OnStartGame;
        }

        private void OnStartGame()
        {
            _playerController.StartRun();
        }

        private void OnEndGame()
        {
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
}