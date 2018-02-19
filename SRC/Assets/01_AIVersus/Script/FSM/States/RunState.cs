using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class RunState : DefaultState
	{
		private Vector3 _dir;

		public RunState(float Duration = 0f) : base(Duration)
		{

		}
		public override void Init(LocalSpace Space)
		{
			base.Init(Space);
			_space.FloatVars["InputMoveX"] = 0f;
			_space.BoolVars["InMotion"] = false;
		}

		public override void EnterState(PawnComponent Entity)
		{
			base.EnterState(Entity);
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
			_dir.Set(_space.FloatVars["InputMoveX"], 0f, 0f);
			_target.Move(_dir);
		}
	}
}