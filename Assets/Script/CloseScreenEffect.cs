using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloseScreenEffect : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> target;
    private List<Vector3> originScale;
    [SerializeField]
    private List<GameObject> obj;
    [SerializeField]
    private bool isClose = false;
    [SerializeField]
    private bool isOpen = false;
    [SerializeField]
    private bool first = false;
    private void Awake()
    {
        originScale = new();
        for (int i = 0; i < target.Count; i++)
        {
            originScale.Add(target[i].localScale);
        }

        //StartCoroutine(StartAfter(5));
    }

    IEnumerator StartAfter(float time)
    {
        Close();
        yield return new WaitForSeconds(time);
        Open();
    }    

    public void Close()
    {
        isClose = true;
        isOpen = false;
        first = true;
    }
    public void Open()
    {
        isClose = false;
        isOpen = true;
        first = true;
    }
    public void CloseAfter(float time)
    {
        StopAllCoroutines();
        StartCoroutine(CloseAfterMinute(time));
    }
    public IEnumerator CloseAfterMinute(float time)
    {
        yield return new WaitForSeconds(time);
        Close();
    }

    public void OpenAfter(float time)
    {
        StopAllCoroutines();
        StartCoroutine(OpenAfterMinute(time));
    }
    public IEnumerator OpenAfterMinute(float time)
    {
        yield return new WaitForSeconds(time);
        Open();
    }    

    public void Update()
    {
        if (isClose)
            Closing();
        if (isOpen)
            Opening();
    }
    public void Closeimmediately()
    {
        foreach (var _o in obj) _o.SetActive(false);
        for (int i = 0; i < target.Count; i++)
        {
            var sc = target[i].localScale;
            sc = new Vector3(sc.x, 0, sc.z);
            target[i].localScale = sc;
            target[i].gameObject.SetActive(false);
        }
    }    
    private void Closing()
    {
        if (first)
        {
            foreach (var _o in obj) _o.SetActive(false);
            first = false;
        }

        isClose = false;
        for (int i = 0; i < target.Count; i++)
        {
            var sc = target[i].localScale;
            sc = Vector3.Lerp(sc, new Vector3(sc.x, 0, sc.z), 8 * Time.deltaTime);

            if (Mathf.Abs((sc - new Vector3(sc.x, 0, sc.z)).magnitude) < 0.001f)
            {
                target[i].gameObject.SetActive(false);
                sc = new Vector3(sc.x, 0, sc.z);
            }
            else
            {
                isClose = true;
            }
            target[i].localScale = sc;
        }
    }    
    private void Opening()
    {
        if (first)
        {
            foreach (var _t in target) _t.gameObject.SetActive(true);
        }

        isOpen = false;
        for (int i = 0; i < target.Count; i++)
        {
            var sc = target[i].localScale;
            sc = Vector3.Lerp(sc, originScale[i], 8 * Time.deltaTime);

            if (Mathf.Abs((sc - originScale[i]).magnitude) < 0.001f)
            {
                sc = originScale[i];
            }
            else
            {
                isOpen = true;
            }

            target[i].localScale = sc;
        }

        if (!isOpen) foreach (var _o in obj) _o.SetActive(true);
    }    
}
