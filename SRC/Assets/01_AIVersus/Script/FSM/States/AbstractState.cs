using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public abstract class AbstractState
	{
		public bool IsMinimalTimeFinish { get; protected set; }

		public abstract void Init(LocalSpace Space);
		public abstract void EnterState(PawnComponent Entity);
		public abstract void ExitState();
		public abstract void Tick(float Deltatime);
	}
}