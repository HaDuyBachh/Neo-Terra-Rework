using System.Collections.Generic;
using MyTools.Event;

public enum TypeState{
    Hello,
    Nod,
    HeadShake,
    Explain,
    Angry,
}

public class ThinkingState : BaseState{
    public List<ResponeState> nextStates;
    Dictionary<TypeState, ResponeState> _nextStates = new Dictionary<TypeState, ResponeState>();
    public GeminiRecyclingModule module;
    public StringEvent answerEvent;
    private string outputText = null;

    public override void InitState()
    {
        base.InitState();
        foreach (var state in nextStates)
        {
            _nextStates.Add(state.typeState, state);
        }
    }

    public override async void OnEnterState()
    {
        base.OnEnterState();
        outputText = await module.ProcessStringAsync(module.inputText);
        MessageManager.Instance.messages[MessageManager.Instance.messages.Count - 1] = outputText;  
        answerEvent.Raise(new StringEventStruct{value = outputText});
    }

    public void AnswerQuestioṇ(StringEventStruct data){
        _fsm.ChangeState(_nextStates[data.typeState]);
    }
}