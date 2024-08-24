using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanIconControl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> trashIcon;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trashIcon"))
        {
            trashIcon.Add(other.gameObject);
        }    
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("trashIcon"))
        {
            trashIcon.Remove(other.gameObject);
        }
    }

    public bool CheckCorrectSort()
    {
        return false;
    }    
}
