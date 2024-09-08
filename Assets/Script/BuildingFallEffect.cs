using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFallEffect : MonoBehaviour, TrapEffect
{
    public Vector3 rotation;
    public float fallTime = 1f;
    public void Start()
    {
        Active();
    }
    public void Active()
    {
        StartCoroutine(SetFall());
    }

    IEnumerator SetFall()
    {
        Debug.Log(transform.eulerAngles + "   " + rotation);
        while (transform.rotation != Quaternion.Euler(rotation))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), fallTime * Time.deltaTime);
            yield return null;
        }
    }
}
