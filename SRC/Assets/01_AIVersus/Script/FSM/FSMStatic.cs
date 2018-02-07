using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public static class FSMStatic
	{
		public static FSM FakeTestRandomMove()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new Run(Vector2.one * 1f, 3f)
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<ConditionTransitionState>>()
				{
					{
						ListOfStates[0], new List<ConditionTransitionState>()
						{
							new ConditionTransitionState(ListOfStates[0], null, null, null, new List<KeyValuePair<string, bool>>()
							{
								new KeyValuePair<string, bool>( "InMotion", false )
							})
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}

		public static FSM FakeTestRandomMoveShoot()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new Run(Vector2.one * 1f, 0f),
					new Shoot(2f)
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<ConditionTransitionState>>()
				{
					{
						ListOfStates[0], new List<ConditionTransitionState>()
						{
							new ConditionTransitionState(ListOfStates[1], null, null, null, new List<KeyValuePair<string, bool>>()
							{
								new KeyValuePair<string, bool>( "InMotion", false )
							})
						}
					},
					{
						ListOfStates[1], new List<ConditionTransitionState>()
						{
							new ConditionTransitionState(ListOfStates[0], null, null, null, null)
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}
	}
}
