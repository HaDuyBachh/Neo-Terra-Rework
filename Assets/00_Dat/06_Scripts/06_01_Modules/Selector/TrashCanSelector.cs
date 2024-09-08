using Game.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Selector
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
        public LineRenderer lineRenderer;
        private Transform _currentTrashCan;

        private InputAction _selectAction;

        public HandInputValue _rightHand;

        private void Awake()
        {
            _selectAction = new InputAction( );
            _selectAction.Enable();
            _selectAction.started += ctx => OnClick();
        }

        public void Update()
        {

            // Tạo một tia từ camera đi qua điểm chuột
            Ray ray = GetRay();
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * 100);
            // Bắn raycast
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Xử lý khi raycast trúng một đối tượng
                if (hit.collider.gameObject.CompareTag("trashAndCanIcon"))
                {
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

        public void OnClick(){
            if (_currentTrashCan != null){
                //Debug.Log("Trash Can Selected " + _currentTrashCan.GetComponent<Trashcan>()._trashcanType + " " + Chapter3Manager.Instance.GetTopTrash());
                Chapter3Manager.Instance.OnClickTrashCan(_currentTrashCan.GetComponent<Trashcan>());
            }
        }
    }
}