using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
using Data;
using Core.GamePlay.Support;
using UnityEngine.InputSystem;

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

        [Header("Scene Data")]
        public HPBarController processBar;

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