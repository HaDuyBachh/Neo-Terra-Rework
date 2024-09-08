using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrapScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> traps;
    [SerializeField]
    private bool isActive = false;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "   " + other.tag);
        if ((other.gameObject.CompareTag("player") || other.gameObject.CompareTag("Hand")) && !isActive)
        {
            isActive = true;
            ActiveTrap();
        }
    }

    public void ActiveTrap()
    {
        foreach(var tr in traps)
        {
            if (tr.TryGetComponent<TrapEffect>(out var c)) c.Active();
        }    
    }    
}
