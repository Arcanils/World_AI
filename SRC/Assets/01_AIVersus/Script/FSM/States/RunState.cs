using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class RunState : DefaultState
	{
		public Vector2 Speed;

		private Vector2 _safePosition;
		private Transform _trans;

		public RunState(Vector2 Speed, float Duration = 0f) : base(Duration)
		{
			this.Speed = Speed;
		}
		public override void Init(LocalSpace Space)
		{
			base.Init(Space);
			_space.BoolVars["InMotion"] = false;
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
}