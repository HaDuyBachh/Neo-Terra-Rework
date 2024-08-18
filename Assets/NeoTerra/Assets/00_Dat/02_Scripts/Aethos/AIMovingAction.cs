using Game.Event;
using Game.Object.Aethos;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Object.Action
{
    public class AIMovingAction : BaseObjectAction
    {

        private readonly NavMeshAgent _agent;
        private readonly NavMeshSurface _surface;

        public AIMovingAction(AethosController controller, NavMeshAgent agent, NavMeshSurface surface, ActionEnum type) : base(controller, type)
        {
            _agent = agent;
            _surface = surface;
        }

        private Vector3 _moveTarget;
        private bool _isMoving;

        public override bool Enter()
        {
            _isMoving = true;
            //_surface.BuildNavMesh();
            GameEvent.OnPlayerMove += SetMoveTarget;
            return base.Enter();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _isMoving = false;
            _agent.ResetPath();
            GameEvent.OnPlayerMove = null;
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            //_agent.SetDestination(_moveTarget);
            //Debug.Log("AIMovingAction:Update " + Vector3.Distance(_agent.transform.position, _moveTarget));
            if(Vector3.Distance(new Vector3(_agent.transform.position.x, 0, _agent.transform.position.z), new Vector3( _moveTarget.x, 0, _moveTarget.z)) < 1f){
                
                GameEvent.OnReachTarget?.Invoke();
                GameEvent.OnReachTarget = null;
            }
        }

        private void SetMoveTarget(Vector3 moveTarget)
        {
            Debug.Log("SetMoveTarget " + moveTarget + " " + _actionType);
            if (!_isMoving) return;
            _moveTarget = moveTarget;
            _agent.SetDestination(_moveTarget);
        }
    }
}