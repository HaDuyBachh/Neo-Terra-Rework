using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TrashStorage {
    [CreateAssetMenu(fileName = "TrashDataSO", menuName = "TrashDataSO", order = 0)]
    public class TrashDataSO : ScriptableObject
    {
        public Trash[] trashes;

// #if UNITY_EDITOR
//         public string trashDataPath;

//         [ContextMenu("Add Trash")]
//         public void AddTrash()
//         {
//             var tmp = AssetDatabase.LoadAllAssetsAtPath(trashDataPath);
//             List<Trash> newTrashes = new();
//             for (int i = 0; i < tmp.Length; i++)
//             {
//                 if (tmp[i] is Trash trash)
//                 {
//                     newTrashes.Add(trash);
//                 }
//             }
//             trashes = newTrashes.ToArray();
//         }
// #endif

    }
}