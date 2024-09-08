using UnityEngine;

namespace Teleport{
    public class TeleportGate : MonoBehaviour{
        [SerializeField] private GameObject _acceptPanel;

        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("Player")){
                _acceptPanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other){
            if (other.CompareTag("Player")){
                _acceptPanel.SetActive(false);
            }
        }
    }
}