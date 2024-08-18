using System.Collections;
using Game.Manager;
using UnityEngine;

namespace Game.Particle{
    public class BasicParticle : MonoBehaviour{
        private void OnEnable(){
            float time = GetComponent<ParticleSystem>().main.duration;
            StartCoroutine(ReturnToPool(time));
        }

        IEnumerator ReturnToPool(float time){
            yield return new WaitForSeconds(time);
            Debug.Log("Return to pool");
            ParticleManager.Instance.ReturnVFX(ParticleManager.VFXType.Star, gameObject);
        }
    }
}