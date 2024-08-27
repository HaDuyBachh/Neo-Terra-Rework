using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    [SerializeField]
    private bool isActiveOnStart = false;

    public void Awake()
    {
        if (isActiveOnStart)
        {
            gameObject.SetActive(true);
        }    
        else
        {
            transform.GetComponent<CloseScreenEffect>().Closeimmediately();
            gameObject.SetActive(false);
        }    
    }
}
