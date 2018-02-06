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
			CurrentState.ExitState();
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
		protected VarSpace _space;
		protected AIVersusFSM _target;

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
		public Vector2 Speed;


		private Vector2 _safePosition;

		public override void EnterState(AIVersusFSM Entity)
		{
			base.EnterState(Entity);
			CalculNextSafePosition();
			//_space.MapOfVars["InMotion"] = true as Object;
		}

		public override void Tick(float Deltatime)
		{
			base.Tick(Deltatime);
			var posPlayer = new Vector2(_target.transform.position.x, _target.transform.position.z);
			Vector2 vecDistance = _safePosition - posPlayer;
			Vector2 dirNormalize = vecDistance.normalized;
			Vector2 deltaMove = new Vector2(dirNormalize.x * Speed.x * Deltatime, dirNormalize.y * Speed.y * Deltatime);

			if (vecDistance.sqrMagnitude <= deltaMove.sqrMagnitude)
			{
				FinishMove();
			}

		}

		private void CalculNextSafePosition()
		{
			_safePosition = Vector2.zero;
		}


		private void FinishMove()
		{
			//_space.MapOfVars["InMotion"] = false;
			_target.transform.position = new Vector3(_safePosition.x, _target.transform.position.y, _safePosition.y);
		}
	}
}
