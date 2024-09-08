using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_0_Chapter_1 : MonoBehaviour, ILevelControl
{
    [SerializeField]
    private bool levelComplete = false;
    [SerializeField]
    private bool levelDefeat = false;
    [SerializeField]
    private CloseScreenEffect congratulationsScreen;
    [SerializeField]
    private CloseScreenEffect scoreScreen;
    [SerializeField]
    private CloseScreenEffect loseScreen;
    public void Using()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(5);
        pointHandler.pointDoneEvent.AddListener(Done);
        gameObject.SetActive(true);
    }

    public void UnUsing()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(1);
        pointHandler.pointDoneEvent.AddListener(Done);
        gameObject.SetActive(false);
    }

    public void Done()
    {
        Debug.Log("Đã hoàn thành");
        levelComplete = true;
        congratulationsScreen.gameObject.SetActive(true);
        congratulationsScreen.Open();
        congratulationsScreen.Closeimmediately();
        scoreScreen.Close();
    }
}
