using Game.Object.Aethos;

namespace Game.Object{
    public class BaseObjectAction : IObjectAction, IPlayerInteractable{
        protected readonly AethosController _controller;
        protected readonly ActionEnum _actionType;
        public BaseObjectAction(AethosController controller, ActionEnum action){
            _controller = controller;
            _actionType = action;
        }

        public virtual bool Enter(){
            _controller.Resolve<AethosDisplayComponent>().SetValueParam(_actionType, true);
            return true;
        }

        public virtual bool Exit(ActionEnum actionAfter){
            _controller.Resolve<AethosDisplayComponent>().SetValueParam(_actionType, false);
            return true;
        }

        public virtual void Update(){
            
        }

        public virtual void OnPlayerInteract(){
        }
    }
}