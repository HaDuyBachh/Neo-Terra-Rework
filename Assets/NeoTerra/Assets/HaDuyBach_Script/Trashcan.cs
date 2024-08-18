using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public Trash.Type _trashcanType;
    public PointHandler pointHandler;

    public void Start()
    {
        pointHandler = FindAnyObjectByType<PointHandler>();
    }
    public void UpdatePoint(int p)
    {
        pointHandler.UpdatePoint(p);
    }    
}
