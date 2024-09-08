using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
using Data;
using Core.GamePlay.Support;
using UnityEngine.InputSystem;
using TMPro;

namespace Game.Manager
{
    public class MenuSceneManager : SingletonMonoBehaviour<MenuSceneManager>
    {
        [Header("Scene Name")]
        public string gameplay1;
        public string gameplay2;
        public string gameplay3;

        [Header("Scene Data")]
        public GameData gameData;

        [Header("Scene Controll")]
        public HPBarController processBar;
        public TMP_Text progressText;

        private InputAction _test;

        public override void Awake()
        {
            base.Awake();
            gameData.Init();
            processBar.Init(gameData.process, 100);
            
            _test = new InputAction("test", InputActionType.Button, "<Keyboard>/space");
            _test.Enable();
            _test.started += ctx => {
                SetLevelDataGame(1);
                StartGame(3);
                };
        }

        public void OnEnable(){
            processBar.SetHP(gameData.process, 100);
            if(gameData.process == 0){
                progressText.text = "Démarrer le jeu";
            }
            else{
                progressText.text = "Poursuivez votre progression à " + gameData.process + "%";
            }
        }

        public void StartGame(int chapter)
        {
            switch (chapter)
            {
                case 1:
                    SceneManager.LoadScene(gameplay1);
                    break;
                case 2:
                    SceneManager.LoadScene(gameplay2);
                    break;
                case 3:
                    SceneManager.LoadScene(gameplay3);
                    break;
            }
        }

        public void SetLevelDataGame(int level)
        {
            gameData.levelWillPlay = level;
        }
    }
}