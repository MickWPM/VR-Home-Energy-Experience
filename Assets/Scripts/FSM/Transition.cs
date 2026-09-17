using System;

namespace FSM
{
    public class Transition<TContext>
    {
        private State<TContext> fromState;
        private State<TContext> toState;
        private System.Func<TContext, bool> condition;
        private float priority = 0;

        public State<TContext> FronState => fromState;
        public State<TContext> ToState => toState;
        public float Priority => priority;

        public Transition(State<TContext> fromState, State<TContext> toState, Func<TContext, bool> condition, float priority = 0)
        {
            this.fromState = fromState;
            this.toState = toState;
            this.condition = condition;
            this.priority = priority;
        }

        public bool ConditionMet(TContext context)
        {
            return condition(context);
        }
    }
}