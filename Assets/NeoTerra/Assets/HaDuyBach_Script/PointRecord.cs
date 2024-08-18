using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRecord : MonoBehaviour
{
    public Trashcan trashcan;
    public void Awake()
    {
        trashcan = GetComponentInParent<Trashcan>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("trash"))
        {
            var trash = other.transform.parent.GetComponent<Trash>();
            if (trash._type == trashcan._trashcanType)
            {
                trashcan.UpdatePoint(trash._point);
            }
            Destroy(trash.gameObject, 0.1f);
        }
    }

}
