using Game.Event;
using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object.Action{
    public class HoldingAction : BaseObjectAction{
        private float _timer = 0;
        public HoldingAction(AethosController controller) : base(controller, ActionEnum.Holding){
        }

        public override bool Enter()
        {
            _timer = _controller.Resolve<AethosDisplayComponent>().GetLengthOfAnimation(_actionType);
            return base.Enter();
        }

        public override void Update(){
            _timer -= UnityEngine.Time.deltaTime;
            if(_timer <= 0){
                Debug.Log("HoldingAction:Update");
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.HoldingAndWalking);
                GameEvent.OnPlayerMove?.Invoke(Vector3.zero);
                return;
            }
            //_controller.Resolve<AethosDisplayComponent>().SetAllValueParam(true);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}