using UnityEngine;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        playerController.SetFinish();
    }
}