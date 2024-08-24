using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorControl : MonoBehaviour
{
    public GameObject usingObject;
    public void Awake()
    {
        usingObject = this.gameObject;
    }
}
