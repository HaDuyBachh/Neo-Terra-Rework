using Game.Object.Aethos;
using UnityEngine;

namespace Game.Manager{
    public class GameManager : MonoBehaviour{
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject _player;
        [SerializeField] private AethosController _aethosController;
        public LineRayCast inputLineRayCast;
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(gameObject);
            }
        }

        public GameObject GetPlayer(){
            return _player;
        }

        public AethosController GetAethosController(){
            return _aethosController;}
    }
}