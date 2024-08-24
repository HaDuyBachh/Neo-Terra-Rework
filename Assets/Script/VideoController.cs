using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer video;
    private bool isActive;
    public void Awake()
    {
        isActive = false;
        video = GetComponentInChildren<VideoPlayer>();
        SetActiveVideo(false);
    }
    public IEnumerator DisableTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.gameObject.SetActive(false);
    }
    private void SetActiveVideo(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }    
    public void ActiveVideo()
    {
        isActive = !isActive;

        SetActiveVideo(isActive);
        if (isActive)
        {
            StartCoroutine(DisableTime((float)video.length));
            video.Play();
        }    
        else
        {
            StopAllCoroutines();
            video.Stop();
        }    
        
    }
}
