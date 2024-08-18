using Game.Object.Aethos;
using Unity.AI.Navigation;
using UnityEngine.AI;

namespace Game.Object.Action{
    public class HoldingWalkingAction : AIMovingAction{
        public HoldingWalkingAction(AethosController controller, NavMeshAgent agent, NavMeshSurface surface, ActionEnum type) : base(controller, agent, surface, type){
        }

        public override bool Enter()
        {
            return base.Enter();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}