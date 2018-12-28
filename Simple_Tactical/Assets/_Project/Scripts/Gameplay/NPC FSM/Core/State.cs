using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

namespace Game.FSM
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {

        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        public void UpdateState(NPC_Character controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(NPC_Character controller)
        {
            foreach (var action in actions)
            {
                action.Act(controller);
            }
        }

        private void CheckTransitions(NPC_Character controller)
        {
            bool decisionSucceeded;

            if (transitions.Length == 0)
                return;


            foreach (var transition in transitions)
            {
                decisionSucceeded = transition.decision == null || transition.decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(transition.trueState);
                    //todo: handle as a Selector or Sequence?
                    return;
                }
                else
                {
                    controller.TransitionToState(transition.falseState);
                }
            }
        }


    }
}