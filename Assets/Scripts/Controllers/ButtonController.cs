using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private string _sceneName;
        [SerializeField] private GameObject _curtain;
        [SerializeField] private Transform _uiRoot;

        private Action OnLoaded;
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
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(_sceneName);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            Destroy(_curtainGO);
        }
    }
}