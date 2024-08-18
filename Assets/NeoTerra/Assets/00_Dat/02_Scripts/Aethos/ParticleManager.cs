using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class ParticleManager : MonoBehaviour
    {
        public static ParticleManager Instance { get; private set; }

        public enum VFXType
        {
            Star,
            // Add more VFX types here
        }

        [SerializeField] private GameObject _starVFX;
        [SerializeField] private int poolSize = 10;

        private Dictionary<VFXType, Queue<GameObject>> _vfxPools;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            _vfxPools = new Dictionary<VFXType, Queue<GameObject>>();
            _vfxPools[VFXType.Star] = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject vfx = Instantiate(_starVFX);
                vfx.SetActive(false);
                _vfxPools[VFXType.Star].Enqueue(vfx);
            }
        }

        public GameObject GetVFX(VFXType vfxType)
        {
            if (_vfxPools[vfxType].Count == 0)
            {
                GameObject vfx = Instantiate(_starVFX);
                _vfxPools[vfxType].Enqueue(vfx);
            }

            GameObject vfxInstance = _vfxPools[vfxType].Dequeue();
            vfxInstance.SetActive(true);
            vfxInstance.GetComponent<ParticleSystem>().Play();
            return vfxInstance;
        }

        public void ReturnVFX(VFXType vfxType, GameObject vfx)
        {
            vfx.SetActive(false);
            _vfxPools[vfxType].Enqueue(vfx);
        }
    }
}