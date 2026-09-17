using UnityEngine;

namespace FSM
{
    public class TutorialState : State<NothingContext>
    {
        public override string StateName => stateName;
        private string stateName;
        private GameObject[] toEnable, toDisable;
        public TutorialState(TutorialStateSetup setupData, string stateName) 
        {
            this.stateName = stateName;
            this.toEnable = setupData.objectsToEnable;
            this.toDisable = setupData.objectsToDisable;
        }

        public override void EnterState(NothingContext context)
        {
            base.EnterState(context);
            foreach (GameObject obj in toEnable)
            {
                obj.SetActive(true);
            } 
            foreach (GameObject obj in toDisable)
            {
                obj.SetActive(false);
            }
                
            Debug.Log($"Entered {StateName}");
        }

        public override void ExitState(NothingContext context)
        {
            base.ExitState(context);
            Debug.Log($"Exited {StateName}");
        }

    }
}