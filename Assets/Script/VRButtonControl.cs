using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButtonControl : MonoBehaviour
{
    public float threshhold = 1.0f;
    public float theshhold_delta = 0;
    public UnityEvent _event;
    private void Update()
    {
        if (theshhold_delta > 0) theshhold_delta -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (theshhold_delta <= 0 && other.tag.Contains("Hand"))
        {
            //FindObjectOfType<Game.GameManager.TrashSpawnManager>().ResetTrashManager();
            _event.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Hand"))
        {
            theshhold_delta = threshhold;
        }
    }
}
