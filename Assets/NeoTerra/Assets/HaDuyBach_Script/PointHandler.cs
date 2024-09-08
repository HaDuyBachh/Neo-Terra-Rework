using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.Object;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class PointHandler : MonoBehaviour
{
    [SerializeField]
    private int _point = 0;
    [SerializeField]
    private int scalePoint = 1;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private bool donePoint = false;

    public UnityEvent updatePointEvent;
    public UnityEvent pointDoneEvent;

    public void Rescale(int value)
    {
        scalePoint = value;
    }    

    public void Awake()
    {
        _text.text = "Charge " + _point + "%";
    }
    public void UpdatePoint(int p)
    {
        _point += p * scalePoint;
        _point = Mathf.Min(_point, 100);
        _text.text = "Charge " + _point + "%";

        if (_point == 100 && !donePoint)
        {
            GameManager.Instance.GetAethosController().Resolve<AethosActionLogicComponent>().Awake();
            pointDoneEvent.Invoke();
            donePoint = true;
        }

        updatePointEvent.Invoke();
    }    
}
