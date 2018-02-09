using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class FSM
	{
		public Dictionary<AbstractState, List<ConditionTransitionState>> MapOfTransitions;
		public List<DefaultState> ListOfStates;


		public AbstractState CurrentState;

		private PawnComponent _target;
		private LocalSpace _space;

		public FSM(List<DefaultState> ListOfStates, Dictionary<AbstractState, List<ConditionTransitionState>> MapOfTransitions, AbstractState CurrentState)
		{
			this.ListOfStates = ListOfStates;
			this.MapOfTransitions = MapOfTransitions;
			this.CurrentState = CurrentState;
		}


		public void Init(PawnComponent Target)
		{
			_target = Target;
			_space = new LocalSpace();
			for (int i = ListOfStates.Count - 1; i >= 0; --i)
			{
				ListOfStates[i].Init(_space);
			}
			CurrentState.EnterState(Target);
		}


		public void Tick(float Deltatime)
		{
			var listTransitionsPossible = MapOfTransitions[CurrentState];
			if (listTransitionsPossible != null)
			{
				var nextTransition = GetNextState(listTransitionsPossible);
				if (nextTransition != null)
				{
					SwitchState(nextTransition);
				}
				else
				{
					CurrentState.Tick(Deltatime);
				}
			}
		}
		private AbstractState GetNextState(List<ConditionTransitionState> TransitionsToTest)
		{
			if (!CurrentState.IsMinimalTimeFinish)
				return null;

			for (int i = 0, iLength = TransitionsToTest.Count; i < iLength; i++)
			{
				if (_space.MatchThisVars(TransitionsToTest[i]))
				{
					return TransitionsToTest[i].ToState;
				}
			}

			return null;
		}
		private void SwitchState(AbstractState NextState)
		{
			CurrentState.ExitState();
			CurrentState = NextState;
			CurrentState.EnterState(_target);
		}
	}

}
