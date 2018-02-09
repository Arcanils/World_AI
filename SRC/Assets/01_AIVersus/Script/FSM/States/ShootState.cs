using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
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