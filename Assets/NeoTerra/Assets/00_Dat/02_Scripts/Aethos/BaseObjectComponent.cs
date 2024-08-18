using Game.Object.Aethos;

namespace Game.Object{
    public class BaseObjectComponent : IUpdateable, IObjectComponent, IPlayerInteractable{
        protected readonly AethosController _controller;
        
        public BaseObjectComponent(){
        }

        public BaseObjectComponent(AethosController controller) {
            _controller = controller;
        }

        public virtual void Init(){
        }

        public virtual void Update(){
            
        }

        public virtual void OnPlayerInteract(){
        }
    }   
}