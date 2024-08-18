using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.Manager;
using Game.Object;
using Game.Object.Interact;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
namespace Game.Object.Aethos
{
    public enum AethosComponent
    {
        None,
        Display,
        State,
        ActionLogic,
    }

    public class AethosController : MonoBehaviour, IResolvable
    {
        [SerializeField] private NavMeshAgent _navmeshAgent;
        [SerializeField] private NavMeshSurface _navmeshSurface;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _objectToScan;
        [SerializeField] private TTSModule _ttsModule;
        [SerializeField] private TMP_Text _chatText;
        [SerializeField] private GameObject _starVFX;
        [SerializeField] private GeminiRecyclingModule _scanModule;
        [SerializeField] private Transform _holdPivot;

        private Dictionary<AethosComponent, BaseObjectComponent> _components = new Dictionary<AethosComponent, BaseObjectComponent>();
        [SerializeField] private InputAction _moveAction;
        [SerializeField] private InputAction _awakeAction;
        [SerializeField] private InputAction _talkAction;
        [SerializeField] private InputAction _interactAction;
        [SerializeField] private InputAction _scanAction;

        #region  Unity Callbacks
        private void Awake()
        {
            InitComponent();

            #region  Su kien ban phim
            //_chatText.gameObject.transform.parent.gameObject.SetActive(false);
            //_moveAction = new InputAction("Move", InputActionType.Button, "<Keyboard>/space");
            //_moveAction.performed += ctx =>
            //{
            //    Resolve<AethosActionLogicComponent>().GoToScannedObject(_objectToScan.GetComponent<Trash>(), false);
            //};
            //_moveAction.Enable();

            //_awakeAction = new InputAction("Awake", InputActionType.Button, "<Keyboard>/a");
            //_awakeAction.performed += ctx =>
            //{
            //    Resolve<AethosActionLogicComponent>().Awake();
            //};
            //_awakeAction.Enable();

            //_interactAction = new InputAction("Interact", InputActionType.Button, "<Keyboard>/i");
            //_interactAction.performed += ctx =>
            //{
            //    OnPlayerInteract();
            //};
            //_interactAction.Enable();

            //_scanAction = new InputAction("Scan", InputActionType.Button, "<Keyboard>/s");
            //_scanAction.performed += ctx =>
            //{
            //    Resolve<AethosActionLogicComponent>().GoToScannedObject(_objectToScan.GetComponent<Trash>(), true);
            //};
            //_scanAction.Enable();
            #endregion

            #region Su kien VR
            _chatText.gameObject.transform.parent.gameObject.SetActive(false);
            //_moveAction = new InputAction("Move", InputActionType.Button, "<OculusTouchController>/buttonSouth");
            _moveAction.started += ctx =>
            {
                if (Game.Manager.GameManager.Instance.inputLineRayCast.lineCastMode == 2 &&
                    Game.Manager.GameManager.Instance.inputLineRayCast.getHit().HasValue)
                {
                    if (Game.Manager.GameManager.Instance.inputLineRayCast.GetTrashFromHit(out var trash))
                    {
                        Resolve<AethosActionLogicComponent>().GoToScannedObject(trash, false);
                    }
                    else
                    {
                        Resolve<AethosActionLogicComponent>().MoveToTarget(Game.Manager.GameManager.Instance.inputLineRayCast.getHit().GetValueOrDefault().point);
                    }
                }      
                    
            };
            _moveAction.Enable();

            //_awakeAction = new InputAction("Awake", InputActionType.Button, "<OculusTouchController>/buttonEast");
            //_awakeAction.performed += ctx =>
            //{
            //    Resolve<AethosActionLogicComponent>().Awake();
            //};
            //_awakeAction.Enable();

            //_interactAction = new InputAction("Interact", InputActionType.Button, "<OculusTouchController>/buttonNorth");
            _interactAction.performed += ctx =>
            {
                OnPlayerInteract();
            };
            _interactAction.Enable();

            //_scanAction = new InputAction("Scan", InputActionType.Button, "<OculusTouchController>/buttonWest");
            _scanAction.started += ctx =>
            {
                if (Game.Manager.GameManager.Instance.inputLineRayCast.lineCastMode == 1 &&
                    Game.Manager.GameManager.Instance.inputLineRayCast.getHit().HasValue)
                {
                    if (Game.Manager.GameManager.Instance.inputLineRayCast.GetTrashFromHit(out var trash))
                    {
                        Resolve<AethosActionLogicComponent>().GoToScannedObject(trash, true);
                    }
                    else
                    {
                        Resolve<AethosActionLogicComponent>().MoveToTarget(Game.Manager.GameManager.Instance.inputLineRayCast.getHit().GetValueOrDefault().point);
                    }
                }
            };
            _scanAction.Enable();
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var component in _components.Values)
            {
                component.Update();
            }
        }
        #endregion

        private void InitComponent()
        {
            _components = new Dictionary<AethosComponent, BaseObjectComponent>
            {
                { AethosComponent.Display, new AethosDisplayComponent(this, _animator) },
                { AethosComponent.State, new AethosStateComponent(this, _navmeshAgent, _navmeshSurface, _ttsModule, _chatText, _scanModule) },
                { AethosComponent.ActionLogic, new AethosActionLogicComponent(this)}
            };
            foreach (var component in _components.Values)
            {
                component.Init();
            }
        }

        public void OnPlayerInteract()
        {
            foreach (var component in _components.Values)
            {
                component.OnPlayerInteract();
            }
        }

        public T Resolve<T>() where T : class, IObjectComponent
        {
            var componentType = GetComponentType<T>();
            if (componentType == AethosComponent.None)
            {
                return null;
            }
            return _components[componentType] as T;
        }

        protected virtual AethosComponent GetComponentType<T>() where T : class, IObjectComponent
        {
            if (typeof(T) == typeof(AethosDisplayComponent))
            {
                return AethosComponent.Display;
            }
            if (typeof(T) == typeof(AethosStateComponent))
            {
                return AethosComponent.State;
            }
            if (typeof(T) == typeof(AethosActionLogicComponent))
            {
                return AethosComponent.ActionLogic;
            }
            return AethosComponent.None;
        }

        public void SetScanObject(GameObject _obj)
        {
            _objectToScan = _obj;
        }

        //public ObjectDataController TargetScanner {get; set;}
        public Trash TargetObject { get; set; }
    }
}