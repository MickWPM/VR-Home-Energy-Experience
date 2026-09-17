using System;
using System.Collections.Generic;

namespace FSM
{

    public class FSM<TContext>
    {
        private List<State<TContext>> statesDictionary;
        private State<TContext> currentState;
        private Dictionary<State<TContext>, List<Transition<TContext>>> transitionsDictionary;
        private List<Transition<TContext>> currentStateTransitions;
        //Default behaviour is state changed takes one "tick" - can override this here
        public bool RunUpdateOnStateChanged = false;
        private bool initialised = false;
        public FSM()
        {
            statesDictionary = new List<State<TContext>>();
            currentState = null;
            transitionsDictionary = new Dictionary<State<TContext>, List<Transition<TContext>>> ();
        }

        public void AddState(State<TContext> newState, bool setAsDefault = false)
        {
            statesDictionary.Add(newState);
            if (currentState == null || setAsDefault) SetDefaultState(newState);
        }

        public void AddTransition(State<TContext> fromState, State<TContext> toState, Func<TContext, bool> condition, float priority = 0)
        {
            Transition<TContext> transition = new Transition<TContext>(fromState, toState, condition, priority);
            AddTransition(fromState, transition);
        }

        public void AddTransition(State<TContext> state, Transition<TContext> transition)
        {
            if (transitionsDictionary.ContainsKey(state) == false) transitionsDictionary.Add(state, new List<Transition<TContext>>());

            transitionsDictionary[state].Add(transition);
        }

        private void SetDefaultState(State<TContext> state)
        {
            currentState = state;
        }

        public void Tick(TContext context)
        {
            if (initialised == false)
            {
                currentState.EnterState(context);
                initialised = true;
            }
            bool changedStatesThisTick = CheckCondition(context);
            if (changedStatesThisTick == false || RunUpdateOnStateChanged) currentState.UpdateState(context);
        }

        private bool CheckCondition(TContext context)
        {
            if (currentState == null)
            {
                throw new System.NullReferenceException("No current state set for FSM. Did you forget to add states?");
            }

            if (transitionsDictionary.ContainsKey(currentState) == false)
            {
                //No transitions so we stay in this state forever
                return false;
            }

            var transitions = transitionsDictionary[currentState];
            Transition<TContext> selectedTransition = null;
            float selectedTransitionPriority = float.MinValue;

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].ConditionMet(context))
                {
                    if (transitions[i].Priority > selectedTransitionPriority)
                    {
                        selectedTransition = transitions[i];
                        selectedTransitionPriority = transitions[i].Priority;
                    }
                }
            }

            if (selectedTransition != null)
            {
                ChangeStateTo(selectedTransition.ToState, context);
                return true;
            }
            return false;
        }

        private void ChangeStateTo(State<TContext> newState, TContext context)
        {
            currentState.ExitState(context);
            currentState = newState;
            newState.EnterState(context);
        }

    }
}