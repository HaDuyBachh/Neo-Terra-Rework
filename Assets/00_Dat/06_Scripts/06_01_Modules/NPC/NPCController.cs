using UnityEngine;
using UnityEngine.AI;
using TrashStorage;
using UnityEngine.UI;
using System;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private GameObject _unitCanvas;
        [SerializeField] private Image _trashImage;
        [SerializeField] private Animator _animator;

        private Trash _data;
        private NPCGroup _group;
        private Action _onCompleteMoving;
        private NPCSO _npcSO;
        private bool _isInQueue;

        public void Init(NPCSO _so, Vector3 position)
        {
            _npcSO = _so;
            _group = _so.npcGroup;
            _group.Add(this);
            _data = _so.trashDatas.trashes[UnityEngine.Random.Range(0, _so.trashDatas.trashes.Length)];
            _unitCanvas.SetActive(false);
            _agent.Warp(position);

            _animator.SetBool("IsMove", false);
            _animator.SetBool("IsIdle", true);
        }

        public void Despawn()
        {
            _npcSO.DespawnObject(gameObject);
            _npcSO.Spawn();
        }

        public void MoveTo(Vector3 position, Action completeMovingCallback = null, Action startMovingCallback = null)
        {
            _agent.SetDestination(position);
            _onCompleteMoving = completeMovingCallback;
            startMovingCallback?.Invoke();
            _animator.SetBool("IsMove", true);
            _animator.SetBool("IsIdle", false);
        }

        public void Update()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animator.SetBool("IsMove", false);
                _animator.SetBool("IsIdle", true);
                _onCompleteMoving?.Invoke();
                _onCompleteMoving = null;
            }
        }

        public void ShowUnitCanvas()
        {
            _unitCanvas.SetActive(true);
            Sprite sprite = Sprite.Create(_data.trashImage, new Rect(0, 0, _data.trashImage.width, _data.trashImage.height), new Vector2(0.5f, 0.5f));
            _trashImage.sprite = sprite;
        }

        public void HideUnitCanvas()
        {
            _unitCanvas.SetActive(false);
        }

        public Trash Data => _data;
    }
}