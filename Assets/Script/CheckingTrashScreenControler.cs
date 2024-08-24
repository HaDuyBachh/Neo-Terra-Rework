using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingTrashScreenControler : MonoBehaviour
{
    [SerializeField]
    private bool autoAssignObject = true;
    [SerializeField]
    private List<CanIconControl> can;
    void Start()
    {
        if (autoAssignObject)
        {
            for (int i = 0; i<transform.childCount; i++)
            {
                if ( transform.GetChild(i).TryGetComponent<CanIconControl>(out var _can))
                {
                    can.Add(_can);
                }    
            }    
          
        }    
    }
    public bool IsCorrectSort()
    {
        foreach(var _can in can)
        {
            if (!_can.CheckCorrectSort())
            {
                return false;
            }    
        }

        return true; ;
    }    
}
