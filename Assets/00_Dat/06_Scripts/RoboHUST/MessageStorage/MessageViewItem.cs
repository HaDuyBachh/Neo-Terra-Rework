using TMPro;
using UnityEngine;

public class MessageViewItem : MonoBehaviour {
    public RectTransform root;
    public TMP_Text text;
    public float _configCharacterHeight;
    public float _configMaxCharacterPerLine;

    public void SetText(string message, int order = 0){
        text.alignment = order == 0 ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;
        text.text = message;
        SetHeighText();
    }    

    public void SetHeighText(){
        string data = text.text;
        float height = Mathf.Ceil(data.Length / _configMaxCharacterPerLine) * _configCharacterHeight;
        var sizeDelta = root.sizeDelta;
        sizeDelta.y = height;
        root.sizeDelta = sizeDelta;
    }

}