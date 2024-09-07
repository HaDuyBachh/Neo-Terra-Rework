using NPC;
using UnityEngine;
using Singleton;
using Core.GamePlay.Support;

namespace Game.Manager
{
    public class Chapter3Manager : SingletonMonoBehaviour<Chapter3Manager>
    {
        [Header("Scene Data")]
        public float completePercent = 0;
        public int maxNPCInQueue = 5;
        public int maxNPCInScene = 10;
        public int correctPoint;
        public int wrongPoint;
        public NPCSO npcSO;

        [Header("Scene Control")]
        public Transform queuePivot;
        public Transform player;
        public HPBarController hpBarController;
        public int levelPlay = 1;

        private int _currentPoint = 0;

        public override void Awake()
        {
            base.Awake();
            npcSO.npcGroup.Init(queuePivot);
            completePercent = 0;
        }

        public void Start()
        {
            npcSO.Init(0);
            for (int i = 0; i < maxNPCInScene; i++)
            {
                npcSO.Spawn();
            }
        }

        public void StartGame()
        {
            npcSO.Init(0);
            for (int i = 0; i < maxNPCInScene; i++)
            {
                npcSO.Spawn();
            }
        }

        public Trash.Type GetTopTrash()
        {
            return npcSO.npcGroup.queue.Peek().Data._type;
        }

        public void OnClickTrashCan(Trashcan trashcan)
        {
            if (trashcan._trashcanType == GetTopTrash())
            {
                npcSO.npcGroup.MoveTopNPCToTrashCan(trashcan,
                    () =>
                    {
                        OnIncreaseProcessPoint();
                    }
                );
            }
            else
            {
                OnDecreaseProcessPoint();
            }
        }

        public void OnIncreaseProcessPoint()
        {
            _currentPoint += correctPoint;
            completePercent = _currentPoint / 100.0f;
            hpBarController.SetHP(completePercent);
            Debug.Log("Correct " + completePercent);
        }

        public void OnDecreaseProcessPoint()
        {
            _currentPoint -= wrongPoint;
            _currentPoint = Mathf.Max(0, _currentPoint);
            completePercent = _currentPoint / 100.0f;
            hpBarController.SetHP(completePercent);
        }
    }
}