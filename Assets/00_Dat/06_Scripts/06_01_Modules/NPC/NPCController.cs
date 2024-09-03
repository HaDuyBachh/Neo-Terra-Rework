using UnityEngine;
using UnityEngine.AI;
using TrashStorage;
using UnityEngine.UI;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private GameObject _unitCanvas;
        [SerializeField] private Image _trashImage;
        private Trash _data;
        private NPCGroup _group;
        private NPCSO _npcSO;
        private bool _isInQueue;

        public void Init(NPCSO _so, Vector3 position)
        {
            _npcSO = _so;
            _group = _so.npcGroup;
            _group.Add(this);
            _data = _so.trashDatas.trashes[Random.Range(0, _so.trashDatas.trashes.Length)];
            _unitCanvas.SetActive(false);
            _agent.Warp(position);
        }

        public void Despawn()
        {
            _group.Dequeue();
            _npcSO.DespawnObject(gameObject);
        }

        public void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        public Trash Data => _data;
    }
}