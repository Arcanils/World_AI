using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class ReloadState : DefaultState
	{
		public float DurationReload;

		private float _timeLeftBeforeReloadingComplete;

		public ReloadState(float DurationReload, float DurationMinimal) : base(DurationMinimal)
		{
			this.DurationReload = DurationReload;
		}
		public override void Init(LocalSpace Space)
		{
			base.Init(Space);
			_space.BoolVars["InputReload"] = false;
			_space.BoolVars["IsReloading"] = false;
			_space.BoolVars["OutOfAmmos"] = false;
		}
		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
			_timeLeftBeforeReloadingComplete = DurationReload;
			_space.BoolVars["IsReloading"] = true;
		}

		public override void ExitState()
		{
			base.ExitState();
			_space.BoolVars["IsReloading"] = false;
		}

		public override void Tick(float Deltatime)
		{
			base.Tick(Deltatime);
			_timeLeftBeforeReloadingComplete -= Deltatime;
			if (_timeLeftBeforeReloadingComplete < 0)
				EndState();
		}

		private void EndState()
		{
			_space.BoolVars["IsReloading"] = false;
			_space.BoolVars["OutOfAmmos"] = false;
			_target.ReloadAmmo();
		}
	}

}
