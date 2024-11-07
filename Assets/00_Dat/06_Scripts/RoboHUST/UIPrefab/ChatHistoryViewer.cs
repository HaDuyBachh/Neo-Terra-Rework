using MyTools.Event;
using UIS;
using UnityEngine;

public class ChatHistoryViewer : MonoBehaviour {

    [SerializeField]
    Scroller List;

    [SerializeField] private float _configCharacterHeight;
    [SerializeField] private float _configMaxCharacterPerLine;

    void Start() {
        if (List == null) {
            List = GetComponent<Scroller>();
        }
        List.OnFill += OnFillItem;
        List.OnHeight += OnHeightItem;
        Refresh();
    }

    public void Refresh() {
        List.InitData(MessageManager.Instance.messages.Count);
        List.ScrollTo(MessageManager.Instance.messages.Count - 1);
    }

    void OnFillItem(int index, GameObject item) {
        item.GetComponent<MessageViewItem>().SetText(MessageManager.Instance.messages[index], index % 2);
    }

    int OnHeightItem(int index) {
        string data = MessageManager.Instance.messages[index];
        float height = Mathf.Ceil(data.Length / _configMaxCharacterPerLine) * _configCharacterHeight;
        return Mathf.CeilToInt(height);
    }

    public void ReceiveAnswer(StringEventStruct data) {
        MessageManager.Instance.messages[MessageManager.Instance.messages.Count - 1] = data.value;
        Refresh();
    }
}