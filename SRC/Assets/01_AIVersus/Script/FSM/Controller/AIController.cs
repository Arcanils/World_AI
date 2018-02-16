using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class AIController : MonoBehaviour
	{
		public FSM AI;
		public PawnComponent Pawn;

		public void Start()
		{
			//AI = FSMStatic.FakeTestRandomMoveShootAndReload();
			//AI.Init(Pawn);
		}

		public void Update()
		{
			AI.Tick(Time.deltaTime);
		}
	}
}