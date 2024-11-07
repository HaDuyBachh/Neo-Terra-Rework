using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class MessageManager : SingletonMonoBehaviour<MessageManager>{
    public List<string> messages = new List<string>();

    public void AddQuestion(string question){
        messages.Add(question);

    }
}