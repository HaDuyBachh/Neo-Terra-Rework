using System.Collections;
using System.Collections.Generic;
using MyTools.Event;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFSM : MonoBehaviour
{
    public StringEvent listenQuestionEvent;
    public StringEvent answerQuestionEvent;

    InputAction _fireQuestionAction;
    InputAction _fireAnswerAction;

    public BaseState _currentState;

    public void Awake(){
        _fireQuestionAction = new InputAction("FireQuestion", binding: "<Keyboard>/q");
        _fireQuestionAction.performed += ctx => listenQuestionEvent.Raise(new StringEventStruct{value = "What is your name?"});
        _fireQuestionAction.Enable();

        _fireAnswerAction = new InputAction("FireAnswer", binding: "<Keyboard>/e");
        _fireAnswerAction.performed += ctx => answerQuestionEvent.Raise(new StringEventStruct{value = "My name is RoboHUST"});
        _fireAnswerAction.Enable();
    }

    public void ChangeState(BaseState nextState){
        _currentState.OnExitState();
        _currentState = nextState;
        nextState.OnEnterState();
    }

    public void Start(){
        _currentState.OnEnterState();
    }
}
