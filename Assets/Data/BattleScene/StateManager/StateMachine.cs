using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : FuncManager
{
    [SerializeField] protected State currentState;
    [SerializeField] protected Coroutine currentCoroutine;
    [SerializeField] protected List<State> states;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStates();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(DelayedStateChange());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopAllCoroutines();
    }

    private IEnumerator DelayedStateChange()
    {
        yield return new WaitForSeconds(2f);
        this.ChangeToNextState(); 
    }

    protected virtual void LoadStates()
    {
        if (this.states.Count > 0) return;

        foreach (Transform obj in this.transform)
        {
            if (obj == null) continue;
            State state = obj.transform.GetComponent<State>();
            if (state == null) continue;
            this.states.Add(state);
        }



        Debug.Log(transform.name + " Load States ", gameObject);
    }

    public void ChangeState(State newState)
    {
        if (this.currentState != null && this.currentCoroutine != null)
        {
            this.StopCoroutine(this.currentCoroutine);
        }

        this.currentState = newState;
        this.currentCoroutine = this.StartCoroutine(this.currentState.Enter());
    }

    public virtual void ResetState()
    {

        if (this.currentState != null && this.currentCoroutine != null)
        {
            this.StopCoroutine(this.currentCoroutine);
        }
        BossIdleState bossIdleState = transform.GetComponentInChildren<BossIdleState>();
        this.currentState = bossIdleState; 
        this.currentCoroutine = this.StartCoroutine(this.currentState.Enter());
    }

    public void ChangeToNextState()
    {
        if (this.states.Count == 0) return;

        State nextState = states[Random.Range(0, this.states.Count)];
        this.ChangeState(nextState);
    }

}
