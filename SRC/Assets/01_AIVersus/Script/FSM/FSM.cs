using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class LocalSpace
	{
		public Dictionary<string, string> StringVars;
		public Dictionary<string, float> FloatVars;
		public Dictionary<string, int> IntVars;
		public Dictionary<string, bool> BoolVars;

		public LocalSpace()
		{
			StringVars = new Dictionary<string, string>();
			FloatVars = new Dictionary<string, float>();
			IntVars = new Dictionary<string, int>();
			BoolVars = new Dictionary<string, bool>();
		}

		public bool MatchThisVars(ConditionTransitionState Conditions)
		{
			if (Conditions.StringConditions != null)
			{
				var conditions = Conditions.StringConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!StringVars.ContainsKey(key) || StringVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.FloatConditions != null)
			{
				var conditions = Conditions.FloatConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!FloatVars.ContainsKey(key) || FloatVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.IntConditions != null)
			{
				var conditions = Conditions.IntConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!IntVars.ContainsKey(key) || IntVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.BoolConditions != null)
			{
				var conditions = Conditions.BoolConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!BoolVars.ContainsKey(key) || BoolVars[key] != conditions[i].Value)
						return false;
				}
			}

			return true;
		}
	}


	public class ConditionTransitionState
	{
		public AbstractState ToState;
		public List<KeyValuePair<string, string>> StringConditions;
		public List<KeyValuePair<string, float>> FloatConditions;
		public List<KeyValuePair<string, int>> IntConditions;
		public List<KeyValuePair<string, bool>> BoolConditions;

		public ConditionTransitionState(AbstractState ToState, List<KeyValuePair<string, string>> StringConditions, List<KeyValuePair<string, float>> FloatConditions, 
			List<KeyValuePair<string, int>> IntConditions, List<KeyValuePair<string, bool>> BoolConditions)
		{
			this.ToState = ToState;
			this.StringConditions = StringConditions ?? new List<KeyValuePair<string, string>>();
			this.FloatConditions = FloatConditions ?? new List<KeyValuePair<string, float>>();
			this.IntConditions = IntConditions ?? new List<KeyValuePair<string, int>>();
			this.BoolConditions = BoolConditions ?? new List<KeyValuePair<string, bool>>();
		}
	}

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

	public abstract class AbstractState
	{
		public bool IsMinimalTimeFinish { get; protected set; }

		public abstract void Init(LocalSpace Space);
		public abstract void EnterState(PawnComponent Entity);
		public abstract void ExitState();
		public abstract void Tick(float Deltatime);
	}

	public class DefaultState : AbstractState
	{
		public float DurationToWait;


		protected LocalSpace _space;
		protected PawnComponent _target;

		private float _currentTimeLeft;

		public DefaultState(float Duration = 0f)
		{
			DurationToWait = Duration;
		}

		public override void Init(LocalSpace Space)
		{
			_space = Space;
		}

		public override void EnterState(PawnComponent Entity)
		{
			_target = Entity;
			_currentTimeLeft = DurationToWait;
			IsMinimalTimeFinish = false;
			Debug.Log("Entering State : " + GetType());
		}

		public override void Tick(float Deltatime)
		{
			_currentTimeLeft -= Deltatime;

			if (_currentTimeLeft < 0f)
			{
				IsMinimalTimeFinish = true;
			}
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
		private Transform _trans;

		public Run(Vector2 Speed, float Duration = 0f) : base(Duration)
		{
			this.Speed = Speed;
		}

		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
			_trans = Entity.transform;
			CalculNextSafePosition();
			_space.BoolVars["InMotion"] = true;
		}

		public override void ExitState()
		{
			base.ExitState();
			_space.BoolVars["InMotion"] = false;
		}

		public override void Tick(float Deltatime)
		{
			base.Tick(Deltatime);
			var posPlayer = new Vector2(_trans.position.x, _trans.position.y);
			Vector2 vecDistance = _safePosition - posPlayer;
			Vector2 dirNormalize = vecDistance.normalized;
			Vector2 deltaMove = new Vector2(dirNormalize.x * Speed.x * Deltatime, dirNormalize.y * Speed.y * Deltatime);

			if (vecDistance.sqrMagnitude <= deltaMove.sqrMagnitude)
			{
				FinishMove(vecDistance);
			}
			else
			{
				_target.Move(new Vector3(deltaMove.x, deltaMove.y, _trans.position.z));
			}

		}

		private void CalculNextSafePosition()
		{
			_safePosition = new Vector2(Random.Range(-2f, 2f) + _trans.position.x, Random.Range(-2f, 2f) + _trans.position.y);
		}


		private void FinishMove(Vector2 DistanceToAdd)
		{
			_space.BoolVars["InMotion"] = false;
			_target.Move(new Vector3(DistanceToAdd.x, DistanceToAdd.y, _trans.position.z));
		}
	}

	public class Shoot : DefaultState
	{
		private Vector3 _direction;

		public Shoot(float DurationToWait) : base(DurationToWait)
		{

		}
		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
			_space.BoolVars["IsShooting"] = true;
			_space.BoolVars["OutOfAmmos"] = false;
			GetTarget();
		}

		public override void ExitState()
		{
			base.ExitState();
			_space.BoolVars["IsShooting"] = false;
		}

		public override void Tick(float Deltatime)
		{
			base.Tick(Deltatime);
			if (_target.NAmmoLeft > 0)
			{
				_target.Shoot(_direction);
			}
			else
			{
				OutOfAmmos();
			}
		}

		private void OutOfAmmos()
		{
			_space.BoolVars["OutOfAmmos"] = true;
			_space.BoolVars["IsShooting"] = false;
		}

		private void GetTarget()
		{
			_direction = Vector3.up;
		}
		
	}
}
