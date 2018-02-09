using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
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


}