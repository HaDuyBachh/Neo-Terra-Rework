using UnityEngine;

namespace Game.Environment
{
    public class TornadorController : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 0f;
        public void Update()
        {
            RotateObject();
        }

        private void RotateObject()
        {
            transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
        }
    }
}