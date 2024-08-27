using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckingTrashScreenControler : MonoBehaviour
{
    [SerializeField]
    private bool autoAssignObject = true;
    [SerializeField]
    private List<CanIconControl> can;
    [SerializeField]
    private int trashIconCount;
    [SerializeField]
    private Image background;
    [SerializeField]
    private CloseScreenEffect displayToChange;
    void Start()
    {
        if (autoAssignObject)
        {
            trashIconCount = 0;
            for (int i = 0; i<transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<CanIconControl>(out var _can))
                {
                    can.Add(_can);
                }   
                else
                if (transform.GetChild(i).TryGetComponent<TrashIconControl>(out var _trash))
                {
                    trashIconCount++;
                }    
            }    
          
        }    
    }
    public bool IsCorrectSort()
    {
        var remainTrash = trashIconCount;
        foreach(var _can in can)
        {
            var check = _can.CheckCorrectSort();
            if (check == -1)
            {
                return false;
            }    
            else
            {
                remainTrash -= check;
            }    
        }

        return (remainTrash == 0);
    }    

    public void ShowCorrectSortDisplay(float time)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeBackgroundColour(background.color, Color.green, time));
        StartCoroutine(CloseDisplay(time + 1f));
        StartCoroutine(ChangeDisplay(time + 1f + 1f));
    }    
    
    public IEnumerator CloseDisplay(float wait)
    {
        displayToChange.gameObject.SetActive(true);
        displayToChange.Closeimmediately();
        yield return new WaitForSeconds(wait);
        transform.GetComponent<CloseScreenEffect>().Close();
    }    
    
    public IEnumerator ChangeDisplay(float wait)
    {
        yield return new WaitForSeconds(wait);
        displayToChange.GetComponent<CloseScreenEffect>().Open();
    }    

    public void ShowWrongSortDisPlay(float time)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeBackgroundColour(background.color,Color.red,4.0f));
    }

    private IEnumerator ChangeBackgroundColour(Color startColor, Color endColor, float timeout)
    {
        while (background.color != endColor)
        {
            timeout -= Time.deltaTime;
            if (timeout > 0)
            {
                background.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
            }    
            else
            {
                background.color = startColor;
                yield break;    
            }    
            yield return null;
        }
    }

    public void SetIfCorrectSoft(float time)
    {
        if (IsCorrectSort()) ShowCorrectSortDisplay(time);
        else
            ShowWrongSortDisPlay(time);
    }    
}
