
namespace FSM
{
    //Declaring a base abstract class with virtual methods means we dont have to explicitly define methods for each state
    //This is an improvement on the fish state machine where all states had all interface methods (eg. empty update state)
    public abstract class State<TContext> : IState<TContext>
    {
        public abstract string StateName { get; }
        public virtual void EnterState(TContext context) { }

        public virtual void UpdateState(TContext context) { }//update parameters

        public virtual void ExitState(TContext context) { }
    }
}