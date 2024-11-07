public class GoodResponseState : BaseState
{
    public BaseState idleState;
    
    public void CompleteResponse()
    {
        _fsm.ChangeState(idleState);
    }
}