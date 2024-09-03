using UnityEngine;
using UnityEngine.AI;
using TrashStorage;

namespace NPC{
    public class NPCController : MonoBehaviour{
        [SerializeField] private NavMeshAgent _agent;
        private TrashDisplayData _data;
        
        public void Init(Vector3 position){

        }
    }
}