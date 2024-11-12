using System.Collections.Generic;
using Core.SystemGame.Pooling;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessageScroll : MonoBehaviour {
    public Transform Container;
    public GameObject MessageItem;

    private List<GameObject> _messageItems = new List<GameObject>();

    private ScrollRect scrollView;

    public void Start(){
        scrollView = GetComponent<ScrollRect>();
        SpawnAll(MessageManager.Instance.messages);
        scrollView.verticalNormalizedPosition = 0;
    }

    public void ReSpawnText(){
        for (int i = 0; i < _messageItems.Count; i++){
            SimplePool.Instance.ReturnObject(_messageItems[i]);
        }
        _messageItems.Clear();
        SpawnAll(MessageManager.Instance.messages);
        scrollView.verticalNormalizedPosition = 0;
    }


    public void SpawnMessage(string message, int order = 0){
        var messageItem = SimplePool.Instance.SpawnObject(MessageItem, Container);
        messageItem.GetComponent<MessageViewItem>().SetText(message, order);
        _messageItems.Add(messageItem);
    }    

    public void SpawnAll(List<string> messages){
        for (int i = 0; i < messages.Count; i++){
            SpawnMessage(messages[i], i % 2);
        }
    }


}