using System.Collections;
using UnityEngine;


public abstract class  State : FuncManager
{
    [SerializeField] protected StateMachine stateMachine;
    public virtual IEnumerator Enter() 
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        yield break;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStateMachine();
    }
    protected virtual void LoadStateMachine()
    {
        if (this.stateMachine != null) return;
        this.stateMachine = transform.parent.GetComponent<StateMachine>();
        Debug.Log(transform.name + "Load StateMachine ", gameObject);
    }
    public StateMachine GetStateMachine() => this.stateMachine; 
}
