using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRayCast : MonoBehaviour
{
    public LineRenderer line;
    public Transform lineOrigin;
    public HandInputValue handInput;
    public int lineCastMode = 0;
    public RaycastHit? hit;
    public List<Material> mode;
    public GameObject Screen;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        handInput.primaryPressEvent.AddListener(TurnOnOff);
        handInput.activePressEvent.AddListener(GetRayCast);
    }
    public bool GetTrashFromHit(out Trash trashObj)
    {
        return hit.Value.collider.transform.parent.TryGetComponent(out trashObj);
    }
    public RaycastHit? getHit()
    {
        return hit;
    }
    private void GetRayCast()
    {
        this.hit = null;
        if (Physics.Raycast(new Ray(lineOrigin.position, lineOrigin.forward), out var hit))
        {
            Debug.Log("đã bắn trúng " + hit.collider.name);
            Debug.DrawRay(lineOrigin.position, lineOrigin.forward, Color.yellow, 10f);
            this.hit = hit;
            if (hit.transform.TryGetComponent<RayCastActiveObjectController>(out var _active))
            {
                _active.RayCastActive();
            }
        }
    }

    private void ShowScreen(int id)
    {
        Screen.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < Screen.transform.GetChild(0).childCount; i++)
        {
            Screen.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        if (id > -1)
        {
            Screen.transform.GetChild(0).gameObject.SetActive(true);
            Screen.transform.GetChild(0).gameObject.SetActive(true);
            Screen.transform.GetChild(0).GetChild(id).gameObject.SetActive(true);
        }
    }

    private void TurnOnOff()
    {
        if (lineCastMode < 2)
        {
            lineCastMode++;
            transform.GetComponent<Renderer>().material = mode[lineCastMode - 1];
        }
        else
            lineCastMode = 0;

        ShowScreen(lineCastMode-1);
    }
    // Update is called once per frame
    void Update()
    {
        if (lineCastMode == 0)
        {
            line.SetPosition(0, lineOrigin.position);
            line.SetPosition(1, lineOrigin.position);
        }
        else
        if (lineCastMode > 0)
        {
            line.SetPosition(0, lineOrigin.position);
            line.SetPosition(1, lineOrigin.position + lineOrigin.forward.normalized * 100);
        }

    }
}
