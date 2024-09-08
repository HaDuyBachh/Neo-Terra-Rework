using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionScript : MonoBehaviour
{
    public VaccumControl vaccumm;
    public void OnTriggerEnter(Collider other)
    {
        var p = other.transform.parent;
        if (p.transform.CompareTag("trash"))
        {
            vaccumm.AddObj(p);
        }
    } 
}
