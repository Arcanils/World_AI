using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class ShootState : DefaultState
	{
		private float _durationBeforeShoot;
		private Vector3 _direction;

		public ShootState(float FireRate, float DurationToWait) : base(DurationToWait)
		{

		}
		public override void Init(LocalSpace Space)
		{
			base.Init(Space);
			_space.BoolVars["IsShooting"] = false;
			_space.BoolVars["OutOfAmmos"] = false;
			_durationBeforeShoot = 0f;
		}
		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
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
			if (_durationBeforeShoot < 0f)
			{
				if (_target.NAmmoLeft > 0)
				{
					_target.Shoot(_direction);
				}
				else
				{
					OutOfAmmos();
				}
			}
			else
			{
				_durationBeforeShoot -= Deltatime;
				_space.BoolVars["IsShooting"] = _durationBeforeShoot > 0f;
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