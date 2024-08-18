using Game.Event;
using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object{
    public class AethosActionLogicComponent : BaseObjectComponent{
        public AethosActionLogicComponent(AethosController controller) : base(controller){
        }

        public override void Init(){
            base.Init();
        }

        private bool _isAwake = false;

        public override void Update(){
            base.Update();
        }

        public void ScanAnObject(GameObject target){
            MoveToTarget(target.transform.position);
            GameEvent.OnReachTarget += () => {
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Scan);
            };
        }

        public void MoveToTarget(Vector3 _targetPosition) {
            _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Move);
            GameEvent.OnPlayerMove?.Invoke(_targetPosition);
            GameEvent.OnReachTarget += () =>
            {
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Idle);
            };
        }

        public void PlayerInteracted(){

        }

        public void TalkToPlayer(string message, bool isChangeToTalking = false){
            if(isChangeToTalking)
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Talking);
            else 
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.SpecialTalking);
            GameEvent.OnTalkToPlayer?.Invoke(message);
        }

        public void GoToScannedObject(Trash trash, bool isLearning){
            MoveToTarget(trash.transform.position);
            GameEvent.OnReachTarget += () => {
                _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Scan);
                GameEvent.OnScanObject?.Invoke(trash, isLearning);
            };
        }

        public void Awake(){
            if(_isAwake) return;
            _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Idle);
            _isAwake = true;
        }
    }
}