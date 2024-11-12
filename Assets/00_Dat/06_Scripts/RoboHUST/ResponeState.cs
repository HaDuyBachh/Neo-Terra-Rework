public class ResponeState : BaseState
{
    public TypeState typeState;
    public BaseState idleState;
    
    public void CompleteResponse()
    {
        _fsm.ChangeState(idleState);
    }
}