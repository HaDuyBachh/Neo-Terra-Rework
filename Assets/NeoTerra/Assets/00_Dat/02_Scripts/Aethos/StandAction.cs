using Game.Object.Aethos;

namespace Game.Object.Action{
    public class IdleAction : BaseObjectAction{
        public IdleAction(AethosController controller) : base(controller, ActionEnum.Idle){
        }

        public override bool Enter()
        {
            return base.Enter();
        }

        public override void Update(){

        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}