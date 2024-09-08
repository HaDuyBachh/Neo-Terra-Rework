using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveBlockScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> traps;
    [SerializeField]
    private UnityEvent activeEvents;
    [SerializeField]
    private bool isActive = false;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "   " + other.tag);
        if ((other.gameObject.CompareTag("player") || other.gameObject.CompareTag("Hand")) && !isActive)
        {
            isActive = true;
            ActiveTrap();
            activeEvents.Invoke();
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
