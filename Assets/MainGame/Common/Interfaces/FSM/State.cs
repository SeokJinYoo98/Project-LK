using Common.Interface.MVPC;
using Unity.VisualScripting;

public abstract class State<TPresenter> where TPresenter : IPresenter
{
    public string Name { get; protected set; }
    protected TPresenter _owner;
    public State (TPresenter owner, string name = null)
    { 
        _owner = owner;
        Name = name ?? GetType().Name; 
    }

    public virtual void  Reset() { }
    public virtual void Enter() { }
    public virtual void Execute(float deltaTime) { }
    public virtual void Exit() { }
}