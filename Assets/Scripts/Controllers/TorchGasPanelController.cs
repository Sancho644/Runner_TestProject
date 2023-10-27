using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class TorchGasPanelController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _fireGasList = new List<GameObject>();

        private void Start()
        {
            TorchGasController.OnChange += SetGasIcon;
        }

        private void OnDestroy()
        {
            TorchGasController.OnChange -= SetGasIcon;
        }

        private void SetGasIcon(int value)
        {
            foreach (GameObject fireGasIcon in _fireGasList)
            {
                switch (value)
                {
                    case < 0:
                    {
                        if (fireGasIcon.activeSelf)
                        {
                            fireGasIcon.gameObject.SetActive(false);

                            return;
                        }

                        break;
                    }
                    case > 0:
                    {
                        if (!fireGasIcon.activeSelf)
                        {
                            fireGasIcon.gameObject.SetActive(true);

                            return;
                        }

                        break;
                    }
                }
            }
        }
    }
}