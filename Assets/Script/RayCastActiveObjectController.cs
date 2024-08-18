using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayCastActiveObjectController : MonoBehaviour
{
    public UnityEvent _event;
    public void RayCastActive()
    {
        _event.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _event.Invoke();
        }    
    }
}
