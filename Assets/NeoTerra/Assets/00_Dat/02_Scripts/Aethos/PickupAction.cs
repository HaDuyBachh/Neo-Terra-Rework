using Game.Event;
using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object.Action{
    public class PickupAction : BaseObjectAction{
        private float _timer = 0;
        private Transform _holdingPivot;
        public PickupAction(AethosController controller) : base(controller, ActionEnum.PickUp){
        }

        public override bool Enter()
        {
            _timer = _controller.Resolve<AethosDisplayComponent>().GetLengthOfAnimation(_actionType);
            return base.Enter();
        }

        public override void Update()
        {
            _timer -= UnityEngine.Time.deltaTime;
            if(_timer <= 0){
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Holding);
                GameEvent.OnPlayerMove?.Invoke(Vector3.zero);
                return;
            }
            base.Update();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public void PickUp(){
            //_controller.TargetObject.transform.SetParent(_holdingPivot);
            //_controller.TargetObject.transform.position = _controller.Resolve<AethosDisplayComponent>().GetHoldingPivot().position;
            //_controller.TargetObject.transform.rotation = _controller.Resolve<AethosDisplayComponent>().GetHoldingPivot().rotation;
        }
    }
}