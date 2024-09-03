using NPC;
using UnityEngine;
using Singleton;

namespace Game.Manager {
    public class Chapter3Manager : SingletonMonoBehaviour<Chapter3Manager>{
        [Header("Scene Data")]
        public int completePercent = 0;
        public int maxNPCInQueue = 5;
        public int maxNPCInScene = 10;
        public NPCSO npcSO;

        [Header("Scene Control")]
        public Transform queuePivot;
        public Transform player;

        public override void Awake()
        {
            base.Awake();
            npcSO.npcGroup.Init(queuePivot);
            completePercent = 0;
        }

        public void Start(){
            npcSO.Init(0);
            for (int i = 0; i < maxNPCInScene; i++){
                npcSO.Spawn();
            }
        }

        public Trash.Type GetTopTrash(){
            return npcSO.npcGroup.queue.Peek().Data._type;
        }
    }
}