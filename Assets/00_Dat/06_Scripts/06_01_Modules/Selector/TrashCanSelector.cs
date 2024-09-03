using UnityEngine;

namespace Modules.Selector
{
    public enum SelectorType { 
        ByMouse,
        ByHand,
    }

    public class TrashCanSelector : MonoBehaviour
    {
        public SelectorType selectorType;
        [Header("By Hand")]
        public GameObject handPivot;

        public void Update(){
            Vector3 pivotPosition = GetPivotPosition();
            Vector3 selectDirection = GetSelectDirection();
        
        }

        private Vector3 GetPivotPosition(){
            return default;
        }

        private Vector3 GetSelectDirection(){
            return default;
        }
    }
}