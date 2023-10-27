using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class MainMenuButtonController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _curtain;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private Transform _uiRoot;

        public Action OnStartGame;
        
        private GameObject _curtainGO;

        private void Start()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _curtainGO = Instantiate(_curtain, _uiRoot);

            StartCoroutine(LoadLevel());
        }

        private IEnumerator LoadLevel()
        {
            yield return new WaitForSeconds(1.2f);
            
            OnStartGame?.Invoke();
            
            Destroy(_curtainGO);
            Destroy(_mainMenu);
        }
    }
}