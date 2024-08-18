using UnityEngine;
namespace Game.GameManager
{
    public class TrashSpawnManager : MonoBehaviour
    {
        public static TrashSpawnManager Instance { get; private set; }
        [SerializeField] private string _trashPath;
        [SerializeField] private GameObject[] _trashPrefabs;
        [SerializeField] private Transform _center;
        [SerializeField] private float _radius;
        [SerializeField] private int _trashSpawnCount = 3;
        [SerializeField] private int _trashCount;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _trashCount = FindObjectsOfType<Trash>().Length;
            if (_trashCount == 0) SpawnTrashLine();
        }

        public void ResetTrashManager()
        {
            foreach (var tr in FindObjectsOfType<Trash>())
            {
                Destroy(tr.gameObject);
            }
            _trashCount = 0;
            SpawnTrashLine();
        }

        public void SpawnTrashRadius()
        {
            for (int i = 0; i < _trashSpawnCount; i++)
            {
                Vector3 spawnPosition = _center.position + Random.insideUnitSphere * _radius;
                spawnPosition.y = _center.position.y + 1;
                GameObject trash = Instantiate(_trashPrefabs[Random.Range(0, _trashPrefabs.Length)], spawnPosition, Quaternion.identity);
            }
        }    

        public void RemoveTrash()
        {
            _trashCount--;
        }    
        public void SpawnTrashLine()
        {
            var cnt = Random.Range(-2, _trashSpawnCount);
            if (_trashCount == 0)
            {
                cnt = 8;
                Debug.Log("đã hết rác đang, thêm mới ");
            }

            Debug.Log(cnt);
                    
            for (int i = 0; i < cnt; i++)
            {
                Vector3 spawnPosition = _center.position + Random.Range(-_radius, _radius) * Vector3.left;
                spawnPosition.y = _center.position.y + 1;
                Instantiate(_trashPrefabs[Random.Range(0, _trashPrefabs.Length)], spawnPosition, Quaternion.identity);
                _trashCount++;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Load Trash")]
        public void LoadTrash()
        {
            string[] prefabGUIDs = UnityEditor.AssetDatabase.FindAssets("t:Prefab", new string[] { _trashPath });
            _trashPrefabs = new GameObject[prefabGUIDs.Length];
            for (int i = 0; i < prefabGUIDs.Length; i++)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(prefabGUIDs[i]);
                _trashPrefabs[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            }
        }
#endif

    }
}