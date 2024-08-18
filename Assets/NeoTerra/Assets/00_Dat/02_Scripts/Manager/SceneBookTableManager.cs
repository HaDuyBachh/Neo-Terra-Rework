using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.GameManager{
    public class SceneBookTableManager : MonoBehaviour{
        [SerializeField] private ParticleSystem _openBook;
        [SerializeField] private GameObject _papper;
        [SerializeField] private GameObject _behaviourPanel;

        private InputAction _openScene;

        private void Start(){
            StartCoroutine(OpenBook());
            StartCoroutine(ActivePaper());
            StartCoroutine(ActiveBehaviourPanel());
        
            _openScene = new InputAction("Move", InputActionType.Button, "<Keyboard>/A");
            _openScene.performed += ctx => OpenGameplayScene();
            _openScene.Enable();

        }

        private void OnDestroy(){
            _openScene.Disable();
        }

        private IEnumerator OpenBook(){
            yield return new WaitForSeconds(1f);
            _openBook.Play();
        }

        private IEnumerator ActivePaper(){
            yield return new WaitForSeconds(3f);
            _papper.SetActive(true);
        }

        private IEnumerator ActiveBehaviourPanel(){
            yield return new WaitForSeconds(8f);
            _behaviourPanel.SetActive(true);
        }

        public void OpenGameplayScene(){
            SceneManager.LoadScene("Màn 1");
        }
    }
}