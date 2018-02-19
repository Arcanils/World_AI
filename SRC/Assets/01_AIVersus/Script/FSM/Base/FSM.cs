using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class FSM
	{
		public Dictionary<AbstractState, List<TransitionStateInfo>> MapOfTransitions;
		public List<DefaultState> ListOfStates;


		public AbstractState CurrentState;

		private PawnComponent _target;
		private LocalSpace _space;

		public FSM(List<DefaultState> ListOfStates, Dictionary<AbstractState, List<TransitionStateInfo>> MapOfTransitions, AbstractState CurrentState)
		{
			this.ListOfStates = ListOfStates;
			this.MapOfTransitions = MapOfTransitions;
			this.CurrentState = CurrentState;
		}

		public static void LinksFSMToThisSpace(LocalSpace Space, params FSM[] FSMs)
		{
			for (int i = 0; i < FSMs.Length; i++)
			{
				FSMs[i].LinkToThisSpace(Space);
			}
		}

		public static void InitFSMs(PawnComponent TargetFSM, params FSM[] FSMs)
		{
			for (int i = 0; i < FSMs.Length; i++)
			{
				FSMs[i].Init(TargetFSM);
			}
		}

		public void Init(PawnComponent Target)
		{
			_target = Target;
			CurrentState.EnterState(Target);
		}

		public void LinkToThisSpace(LocalSpace NewSpace)
		{
			_space = NewSpace ?? new LocalSpace();
			for (int i = ListOfStates.Count - 1; i >= 0; --i)
			{
				ListOfStates[i].Init(_space);
			}
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
		private AbstractState GetNextState(List<TransitionStateInfo> TransitionsToTest)
		{
			if (!CurrentState.IsMinimalTimeFinish)
				return null;

			for (int i = 0, iLength = TransitionsToTest.Count; i < iLength; i++)
			{
				if (_space.MatchThisVars(TransitionsToTest[i].Conditions))
				{
					return TransitionsToTest[i].TargetState;
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
