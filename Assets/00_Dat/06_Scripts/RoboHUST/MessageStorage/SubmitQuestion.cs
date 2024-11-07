using MyTools.Event;
using TMPro;
using UnityEngine;

public class SubmitQuestion : MonoBehaviour {
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private StringEvent _listenQuestionEvent;
    
    public void Submit(){
        if (string.IsNullOrEmpty(_inputField.text)) return;
        MessageManager.Instance.messages.Add(_inputField.text);
        MessageManager.Instance.messages.Add("Thinking ...");
        _listenQuestionEvent.Raise(new StringEventStruct{value = _inputField.text});
        _inputField.text = "";
    }    
}