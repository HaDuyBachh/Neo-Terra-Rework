using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_0_Chapter_1 : MonoBehaviour, ILevelControl
{
    public void Using()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(3);
        gameObject.SetActive(true);
    }

    public void UnUsing()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(1);
        gameObject.SetActive(false);
    }    
}
