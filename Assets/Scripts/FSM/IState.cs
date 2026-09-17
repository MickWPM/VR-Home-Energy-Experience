
namespace FSM
{
    //Extend the fish state machine to be a generic for "context type"
    //This means we dont have to create custom methods with arbitrary inputs
    //Slight overhead by creating machine specific context but means it is fully reusable
    public interface IState<TContext>
    {
        string StateName { get; }

        void EnterState(TContext context);
        void UpdateState(TContext context); //Note no delta time passed - this can be inluded as context if needed
        void ExitState(TContext context);
    }
}