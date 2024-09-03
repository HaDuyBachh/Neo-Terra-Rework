using Core.SystemGame.Factory;
using TrashStorage;
using UnityEngine;

namespace NPC{
    [CreateAssetMenu(fileName = "NPCSO", menuName = "NPC/NPCSO")]
    public class NPCSO : BaseSOWithPool{
        public NPCGroup npcGroup;
        public TrashDataSO trashDatas;

        public void Spawn(){
            var npc = SpawnObject().GetComponent<NPCController>();
            var randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            npc.Init(this, randomPosition);
        }
    }
}