using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
    }
}
