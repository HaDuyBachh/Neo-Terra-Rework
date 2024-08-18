using System.Collections;
using System.Collections.Generic;
using Game.Manager;
using Game.Object;
using UnityEngine;
using TMPro;
public class PointHandler : MonoBehaviour
{
    [SerializeField]
    private int _point = 0;

    public TextMeshProUGUI _text;

    public void Awake()
    {
        _text.text = "Charge " + _point + "%";
    }
    public void UpdatePoint(int p)
    {
        _point += p*30;
        _point = Mathf.Min(_point, 100);
        if (_point == 100)
        {
            GameManager.Instance.GetAethosController().Resolve<AethosActionLogicComponent>().Awake();
        }

        _text.text = "Charge " + _point + "%";
    }    
}
