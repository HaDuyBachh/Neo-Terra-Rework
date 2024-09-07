using System;
using System.Collections.Generic;
using Game.Manager;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "NPCGroup", menuName = "NPC/NPCGroup")]
    public class NPCGroup : ScriptableObject
    {
        public Queue<NPCController> queue = new Queue<NPCController>();
        private Transform _pivot;
        private int _maxNPCInQueue = 5;

        public void Init(Transform pivot)
        {
            _pivot = pivot;
        }

        public void Add(NPCController npc)
        {
            queue.Enqueue(npc);
            UpdateNPCPosition();
        }

        public NPCController Dequeue()
        {
            var npc = queue.Dequeue();
            UpdateNPCPosition();
            return npc;
        }

        public void UpdateNPCPosition()
        {
            int i = 0;
            var direction = (Chapter3Manager.Instance.player.position - _pivot.position).normalized;
            foreach (var npc in queue)
            {
                npc.MoveTo(_pivot.position - direction * i++,
                    i == 1 ?
                    () =>
                    {
                        npc.ShowUnitCanvas();
                    }
                : null);
                if (i >= _maxNPCInQueue)
                {
                    break;
                }
            }
        }

        public void MoveTopNPCToTrashCan(Trashcan can, Action onComplete = null)
        {
            var npc = Dequeue();
            Debug.Log(queue.Count);
            npc.MoveTo(can.standPivot.position,
                () =>
                {
                    onComplete?.Invoke();
                    npc.Despawn();
                }, 
                () =>
                {
                    npc.HideUnitCanvas();
                });
        }

        public void MoveNPCToSomeWhere()
        {
            var npc = Dequeue();
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            npc.MoveTo(randomPos, 
                () => {
                    npc.Despawn();
                },
                () => {
                    Chapter3Manager.Instance.OnDecreaseProcessPoint();
                    npc.HideUnitCanvas();
                }
            );
        }
    }
}