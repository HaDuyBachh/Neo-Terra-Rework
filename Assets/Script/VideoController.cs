using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer video;
    public void Awake()
    {
        video = GetComponentInChildren<VideoPlayer>();
    }
    public IEnumerator DisableTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.gameObject.SetActive(false);
    }    
    public void OnEnable()
    {
        StartCoroutine(DisableTime((float)video.length));
        video.Play();
    }

    
}
