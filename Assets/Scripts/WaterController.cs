using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] private int _torchBurningValue;
    [SerializeField] private float _torchLightValue;
    
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        playerController.SetTorchLight(_torchBurningValue, _torchLightValue);
    }
}