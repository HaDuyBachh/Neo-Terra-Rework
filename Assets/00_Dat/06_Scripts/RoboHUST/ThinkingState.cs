using MyTools.Event;

public class ThinkingState : BaseState{
    public BaseState goodResponseState;
    public BaseState badResponseState;
    public GeminiRecyclingModule module;
    public StringEvent answerEvent;
    private string outputText = null;

    public override async void OnEnterState()
    {
        base.OnEnterState();
        outputText = await module.ProcessStringAsync(module.inputText);
        answerEvent.Raise(new StringEventStruct{value = outputText});
    }

    public void AnswerQuestioṇ(StringEventStruct data){
        if(data.value != null){
            _fsm.ChangeState(goodResponseState);
        }else{
            _fsm.ChangeState(badResponseState);
        }
    }
}