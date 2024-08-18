using System.Collections.Generic;
using Game.Object.Action;
using Game.Object.Aethos;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine.AI;

namespace Game.Object
{

    public enum ActionEnum
    {
        None,
        PowerOff,
        Idle,
        Move,
        Talking,
        SpecialTalking,     
        PickUp,
        Drop,
        Scan,
        Holding,
        HoldingAndWalking,
    }

    public class AethosStateComponent : BaseObjectComponent
    {
        private Dictionary<ActionEnum, BaseObjectAction> _dictAction;
        private ActionEnum _currentAction;
        private ActionEnum _beforeAction;
        public AethosStateComponent()
        {
        }

        public AethosStateComponent(AethosController controller, NavMeshAgent agent, NavMeshSurface surface, TTSModule ttsModule, TMP_Text chatText, GeminiRecyclingModule scanModule) : base(controller)
        {
            _dictAction ??= new Dictionary<ActionEnum, BaseObjectAction>(){
                {ActionEnum.Idle, new IdleAction(_controller)},
                {ActionEnum.Move, new AIMovingAction(_controller, agent, surface, ActionEnum.Move)},
                {ActionEnum.PowerOff, new PowerOffAction(_controller)},
                {ActionEnum.PickUp, new PickupAction(_controller)},
                {ActionEnum.Holding, new HoldingAction(_controller)},
                {ActionEnum.HoldingAndWalking, new HoldingWalkingAction(_controller, agent, surface, ActionEnum.HoldingAndWalking)},
                {ActionEnum.Scan, new ScanAction(_controller, ActionEnum.Scan, scanModule)},
                {ActionEnum.Talking, new TalkingAction(_controller, ActionEnum.Talking, ttsModule, chatText)},
                {ActionEnum.SpecialTalking, new SpecialTalkingAction(_controller, ActionEnum.SpecialTalking, ttsModule, chatText)},
                //{ActionEnum.PickUp, new AIPickUpAction(_controller, null)},
                //{ActionEnum.Drop, new AIDropAction(_controller, null)},
                //{ActionEnum.Scan, new AIScanAction(_controller, null)},
            };
        }

        public override void Init()
        {
            _currentAction = ActionEnum.PowerOff;
            _dictAction[_currentAction].Enter();
        }

        public override void Update()
        {
            _dictAction[_currentAction].Update();
        }

        public override void OnPlayerInteract()
        {
            base.OnPlayerInteract();
            _dictAction[_currentAction].OnPlayerInteract();
        }

        public void ChangeState(ActionEnum action)
        {
            if (_currentAction == action) return;
            if (_dictAction[_currentAction].Exit(action))
            {
                _beforeAction = _currentAction;
                _currentAction = action;
                _dictAction[_currentAction].Enter();
            }
        }

        public ActionEnum BeforeAcion => _beforeAction;
    }
}