using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{

	public class AIVersusFSM : MonoBehaviour
	{
		public FSM AI;
	}

	public class VarSpace
	{
		public Dictionary<string, Object> MapOfVars;
	}

	public class FSM
	{
		public Dictionary<AbstractState, List<ConditionTransitionState>> MapOfTransitions;
		public List<AbstractState> ListOfStates;


		public AbstractState CurrentState;

		private AIVersusFSM _target;
		private VarSpace _space;

		public void Init(AIVersusFSM Target)
		{
			_target = Target;
			_space = new VarSpace();
			for (int i = ListOfStates.Count; i >= 0; --i)
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
		private AbstractState GetNextState(List<ConditionTransitionState> TransitionsToTest)
		{
			return null;
		}
		private void SwitchState(AbstractState NextState)
		{
			CurrentState = NextState;
			CurrentState.EnterState(_target);
		}
	}


	public class ConditionTransitionState
	{
		public List<KeyValuePair<string, Object>> Conditions;
		public AbstractState ToState;
	}

	public abstract class AbstractState
	{
		public abstract void Init(VarSpace Space);
		public abstract void EnterState(AIVersusFSM Entity);
		public abstract void ExitState();
		public abstract void Tick(float Deltatime);
	}

	public class DefaultState : AbstractState
	{
		private VarSpace _space;
		private AIVersusFSM _target;

		public override void Init(VarSpace Space)
		{
			_space = Space;
		}

		public override void EnterState(AIVersusFSM Entity)
		{
			_target = Entity;
		}

		public override void Tick(float Deltatime)
		{
			throw new System.NotImplementedException();
		}

		public override void ExitState()
		{
			_target = null;
		}
	}


	public class Run : DefaultState
	{

	}
}
