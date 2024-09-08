using UnityEngine;
using UnityEngine.AI;
using TrashStorage;
using UnityEngine.UI;
using System;
using Game.Manager;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private GameObject _unitCanvas;
        [SerializeField] private Image _trashImage;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _timeCounter;

        private Trash _data;
        private NPCGroup _group;
        private Action _onCompleteMoving;
        private NPCSO _npcSO;
        private bool _isInQueue;
        private int _level;

        public void Init(NPCSO _so, Vector3 position, int level)
        {
            _npcSO = _so;
            _group = _so.npcGroup;
            _group.Add(this);
            _data = _so.trashDatas.trashes[UnityEngine.Random.Range(0, _so.trashDatas.trashes.Length)];
            _unitCanvas.SetActive(false);
            _agent.Warp(position);

            _animator.SetBool("IsMove", false);
            _animator.SetBool("IsIdle", true);

            if (level > 1)
            {
                _timeCounter.SetActive(true);
            }
            else
            {
                _timeCounter.SetActive(false);
            }
            _level = level;
        }

        public void Despawn()
        {
            _npcSO.DespawnObject(gameObject);
            _npcSO.Spawn(_level);
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
            if(_isInQueue){
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Chapter3Manager.Instance.player.position - transform.position), Time.deltaTime * 10);
            }
        }

        public void SetInQueue(bool value){
            _isInQueue = value;
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