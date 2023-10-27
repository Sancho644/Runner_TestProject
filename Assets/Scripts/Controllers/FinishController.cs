using UnityEngine;

namespace Controllers
{
    public class FinishController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.SetFinish();
        }
    }
}