using UnityEngine;

namespace FSM
{

    public class TutorialStateMachine : MonoBehaviour
    {
        private FSM<NothingContext> stateMachine;
        public bool trigger1to2;
        public bool trigger2to1;
        private NothingContext nothingContext = new NothingContext();

        public TutorialStateSetupFromGO s1Setup, s2Setup;
        public ExternalScriptCondition state3ExternalScript;
        private void Start()
        {
            stateMachine = new FSM<NothingContext>();
            var entryState = new TutorialState(s1Setup.GetContext(), "State 1");
            var state2 = new TutorialState(s2Setup.GetContext(), "State 2");
            var state3 = new OtherTutorialState("State 3");
            stateMachine.AddState(entryState);
            stateMachine.AddState(state2);
            stateMachine.AddState(state3);

            stateMachine.AddTransition(entryState, state2, CheckState1);
            stateMachine.AddTransition(entryState, entryState, CheckReturnToState1);
            stateMachine.AddTransition(entryState, state3, CheckState3, 10);

            stateMachine.AddTransition(state2, entryState, CheckState2);
            stateMachine.AddTransition(state2, state3, CheckState3, 10);

            //Arent lamdas beautiful. Nice way to get around external scripts (that may not just be created for this purpose)
            //not having the correct signature
            stateMachine.AddTransition(state3, state2, context => state3ExternalScript.CheckCondition(), -1);
        }

        [ContextMenu("TICK")]
        public void Tick()
        {
            stateMachine.Tick(nothingContext);
        }

        public bool autoTick = true;
        public void Update()
        {
            if (autoTick) Tick();
        }

        public bool CheckState1(NothingContext context)
        {
            return trigger1to2;
        }
        public bool CheckState2(NothingContext context)
        {
            return trigger2to1;
        }

        public bool loopToState1;
        public bool CheckReturnToState1(NothingContext context)
        {
            return loopToState1;
        }

        public bool CheckState3(NothingContext context)
        {
            return trigger1to2 && trigger2to1;
        }
    }

}