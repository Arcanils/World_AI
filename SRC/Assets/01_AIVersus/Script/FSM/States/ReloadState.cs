using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class ReloadState : DefaultState
	{
		public ReloadState(float DurationMinimal) : base(DurationMinimal)
		{

		}
		public override void Init(LocalSpace Space)
		{
			base.Init(Space);
			_space.BoolVars["IsReloading"] = false;
			_space.BoolVars["OutOfAmmos"] = false;
		}
		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
			_space.BoolVars["IsReloading"] = true;
			_target.StartReload();
		}

		public override void ExitState()
		{
			_target.StopReload();
			base.ExitState();
			_space.BoolVars["IsReloading"] = false;
		}

		public override void Tick(float Deltatime)
		{
			base.Tick(Deltatime);
			if (!_target.IsReloading)
				EndState();
		}

		private void EndState()
		{
			_space.BoolVars["IsReloading"] = false;
			_space.BoolVars["OutOfAmmos"] = false;
		}
	}

}
