using UnityEngine;

namespace FSM
{
    //Example of another tutorial state class.... 
    //Not sure how we could do this
    public class OtherTutorialState : State<NothingContext>
    {
        public override string StateName => stateName;
        private string stateName;
        public OtherTutorialState(string stateName) 
        {
            this.stateName = stateName;
        }

        public override void EnterState(NothingContext context)
        {
            base.EnterState(context);
            Debug.Log($"Entered the other state type {StateName}");
        }

        public override void ExitState(NothingContext context)
        {
            base.ExitState(context);
            Debug.Log($"Exited the other state type {StateName}");
        }

    }
}