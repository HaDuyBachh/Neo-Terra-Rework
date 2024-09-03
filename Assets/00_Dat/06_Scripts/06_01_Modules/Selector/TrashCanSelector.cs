using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Selector
{
    public enum SelectorType
    {
        ByMouse,
        ByHand,
    }

    public class TrashCanSelector : MonoBehaviour
    {
        public SelectorType selectorType;
        [Header("By Hand")]
        public GameObject handPivot;

        private Transform _currentTrashCan;

        public void Update()
        {

            // Tạo một tia từ camera đi qua điểm chuột
            Ray ray = GetRay();

            // Bắn raycast
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Xử lý khi raycast trúng một đối tượng
                if (hit.collider.gameObject.CompareTag("trashAndCanIcon"))
                {
                    if (_currentTrashCan != null) return;
                    _currentTrashCan = hit.collider.gameObject.transform.parent.parent;
                    _currentTrashCan.GetComponent<Outline>().enabled = true;
                    return;
                }
            }


            // Xử lý khi raycast không trúng đối tượng nào
            if (_currentTrashCan != null)
            {
                _currentTrashCan.GetComponent<Outline>().enabled = false;
            }
            _currentTrashCan = null;


        }

        private Ray GetRay()
        {
            switch (selectorType)
            {
                case SelectorType.ByMouse:
                    return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                case SelectorType.ByHand:
                    return new Ray(handPivot.transform.position, handPivot.transform.forward);
                default:
                    return new Ray();
            }
        }
    }
}