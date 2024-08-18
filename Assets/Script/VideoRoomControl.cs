using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoRoomControl : MonoBehaviour
{
    [SerializeField] private Transform TiviScreen;
    [SerializeField] private Transform VideoArea;
    [SerializeField] private Transform PlayArea;
    [SerializeField] private Transform Player;
    void Start()
    {
        VideoPlay(0);
    }
    private IEnumerator StartVideo(int id)
    {
        var vid = TiviScreen.GetChild(id).GetChild(1).GetComponent<VideoPlayer>();
        vid.transform.parent.gameObject.SetActive(true);
        Player.position = VideoArea.position;
        yield return new WaitForSeconds((float)vid.length);
        vid.transform.parent.gameObject.SetActive(false);
        Player.position = PlayArea.position;
    }    
    public void VideoPlay(int id)
    {
        StartCoroutine(StartVideo(id));
    }    
}
