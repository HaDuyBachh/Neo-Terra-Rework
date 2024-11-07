using UnityEngine;
using Animancer;

public class BaseState : MonoBehaviour {
    [SerializeField] protected CharacterFSM _fsm;
    [SerializeField] protected AnimancerComponent _animancer;
    public ClipTransition[] _transitions;

    private void Awake(){
        InitState();
    }

    public virtual void InitState() {
        this.gameObject.SetActive(false);
    }

    public virtual void OnEnterState() {
        this.gameObject.SetActive(true);
        int rand = GetRandomAnimIndex();
        _animancer.Play(_transitions[rand]);
    }

    public virtual void OnExitState() {
        this.gameObject.SetActive(false);
    }

    protected virtual int GetRandomAnimIndex() {
        return Random.Range(0, _transitions.Length);
    }
}