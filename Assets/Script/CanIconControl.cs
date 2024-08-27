using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanIconControl : MonoBehaviour
{
    public Trash.Type Type { get { return type; } }

    [SerializeField]
    private List<GameObject> trashIcon = new();
    [SerializeField]
    private Trash.Type type;

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

    public int CheckCorrectSort()
    {
        var cnt = 0;
        foreach (var trI in trashIcon)
        {
            if (trI.GetComponent<TrashIconControl>().Type == type) cnt++;
            else
            {
                cnt = -1;
                return cnt;
            }    
        }
        return cnt;
    }    
}
