using MyTools.Event;

public class IdleState : BaseState{
    public BaseState thinkingState;
    public GeminiRecyclingModule module;

    public void ChangeAnimation(){
        int rand = GetRandomAnimIndex();
        _animancer.Play(_transitions[rand]);
    }

    public void Thinking(StringEventStruct data ){
        module.inputText = data.value;
        _fsm.ChangeState(thinkingState);
    }
}