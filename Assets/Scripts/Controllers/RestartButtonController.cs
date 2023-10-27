using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class RestartButtonController : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _curtain;
        [SerializeField] private string _sceneName;
        [SerializeField] private Transform _uiRoot;

        private GameObject _curtainGO;

        private void Start()
        {
            _button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _curtainGO = Instantiate(_curtain, _uiRoot);

            StartCoroutine(LoadLevel());
        }

        private IEnumerator LoadLevel()
        {
            SceneManager.LoadScene(_sceneName);
            
            yield return new WaitForSeconds(1.2f);
            
            Destroy(_curtainGO);
        }
    }
}