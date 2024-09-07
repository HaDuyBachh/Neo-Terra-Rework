using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Level_1_Chapter_1 : MonoBehaviour, ILevelControl
{
    [SerializeField]
    private float timeout = 20;
    [SerializeField]
    private float timeoutDelta;
    [SerializeField]
    private Slider processBar;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image fill;
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
        Debug.Log("Level 1 đang được sử dụng");
        gameObject.SetActive(true);
        processBar.gameObject.SetActive(true);

        processBar.maxValue = timeout;
        processBar.minValue = 0;
        ResetTime();
        var pointHandler = FindAnyObjectByType<PointHandler>();

        pointHandler.Rescale(10);
        pointHandler.updatePointEvent.AddListener(ResetTime);
        pointHandler.pointDoneEvent.AddListener(Done);

        fill.color = gradient.Evaluate(1f);
    }

    public void UnUsing()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(1);
        
        pointHandler.updatePointEvent.RemoveListener(ResetTime);
        pointHandler.pointDoneEvent.RemoveListener(Done);

        gameObject.SetActive(false);
        processBar.gameObject.SetActive(false);
    }

    public void ResetTime()
    {
        processBar.value = 100;
       
    }    
    public void Done()
    {
        Debug.Log("Đã hoàn thành");

        processBar.gameObject.SetActive(false);
        levelComplete = true;
        congratulationsScreen.gameObject.SetActive(true);
        congratulationsScreen.Open();
        congratulationsScreen.Closeimmediately();
        scoreScreen.Close();
    }
    public void Lose()
    {
        levelDefeat = true;

        processBar.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        loseScreen.Open();
        loseScreen.Closeimmediately();
        scoreScreen.Close();
    }


    public void Update()
    {
        if (processBar.value > 0 && (!levelComplete || !levelDefeat))
        {
            processBar.value -=  processBar.maxValue / timeout * Time.deltaTime;
            fill.color = gradient.Evaluate(processBar.normalizedValue);
            text.text = ((int)processBar.value).ToString();

            if (processBar.value == 0)
            {
                Lose();
            }
        }    
    }
}
