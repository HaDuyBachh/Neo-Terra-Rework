using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class Level_2_Chapter_1 : MonoBehaviour ,ILevelControl
{
    [SerializeField]
    private float timeout = 15;
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
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CapsuleControl[] caps;

    public void Start()
    {
        caps = FindObjectsOfType<CapsuleControl>();
    }
    public void Using()
    {
        gameObject.SetActive(true);
        processBar.gameObject.SetActive(true);

        processBar.maxValue = timeout;
        processBar.minValue = 0;
        ResetTime();
        var pointHandler = FindAnyObjectByType<PointHandler>();

        pointHandler.Rescale(5);
        pointHandler.updatePointEvent.AddListener(ResetTime);
        pointHandler.updatePointEvent.AddListener(RandomCap);
        pointHandler.pointDoneEvent.AddListener(Done);

        fill.color = gradient.Evaluate(1f);
    }

    public void UnUsing()
    {
        var pointHandler = FindAnyObjectByType<PointHandler>();
        pointHandler.Rescale(1);

        pointHandler.updatePointEvent.RemoveListener(ResetTime);
        pointHandler.updatePointEvent.RemoveListener(RandomCap);
        pointHandler.pointDoneEvent.RemoveListener(Done);

        gameObject.SetActive(false);
        processBar.gameObject.SetActive(false);
    }

    public void RandomCap()
    {
        var cnt = Random.Range(2, 4);

        var trashInPlace = FindObjectsOfType<Trash>();

        var filteredTrash = trashInPlace
            .Where(trash => trash.transform.position.y - player.transform.position.y <= 1.5f)
            .ToList();

        if (filteredTrash.Count > 0)
        {
            foreach (var cap in caps)
            {
                cap.HardDisableCapsule();
            }

            filteredTrash.OrderBy(trash => Vector3.Distance(trash.transform.position, player.transform.position));

            Debug.Log("Đang thực hiện lọc rác " + filteredTrash.Count);

            foreach (var tr in filteredTrash)
            {
                if (cnt == 0) break;
                foreach(var cap in caps)
                {
                    if (cap.type == tr._type && !cap.GetComponent<Collider>().enabled)
                    {
                        cnt--;
                        cap.HardEnableCapsule();
                    }
                }
            }
        }
        else
        {
            foreach (var cap in caps)
            {
                cap.HardEnableCapsule();
            }
        }    
    }
    public void ResetTime()
    {
        processBar.value = 100;

    }
    public void Done()
    { 
        levelComplete = true;

        processBar.gameObject.SetActive(false);
        congratulationsScreen.gameObject.SetActive(true);
        congratulationsScreen.Open();
        congratulationsScreen.Closeimmediately();
        scoreScreen.Close();
    }
    public void Lose()
    {
        levelDefeat = true;

        processBar.gameObject.SetActive(false);
        loseScreen.Open();
        loseScreen.Closeimmediately();
        scoreScreen.Close();
    }

    public void Update()
    {
        if (!levelComplete || !levelDefeat)
        {
            if (processBar.value > 0)
            {
                processBar.value -= processBar.maxValue / timeout * Time.deltaTime;
                fill.color = gradient.Evaluate(processBar.normalizedValue);
                text.text = ((int)processBar.value).ToString();

                if (processBar.value == 0)
                {
                    Lose();
                }
            }
        }
        
    }
}
