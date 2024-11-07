using TMPro;
using UnityEngine;

public class MessageViewItem : MonoBehaviour {
    public TMP_Text text;
    public void SetText(string message, int order = 0){
        text.alignment = order == 0 ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;
        text.text = message;
    }    
}