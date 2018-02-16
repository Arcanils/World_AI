using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public static class FSM_Gameplay
	{
		public static void CreateFSMGameplay(out FSM Reload, out FSM Move, out FSM Shoot)
		{
			Reload = CreateFSMReload();
			Move = CreateFSMReload(); //CreateFSMMovement();
			Shoot = CreateFSMReload(); //CreateFSMShoot();
		}


		public static FSM CreateFSMReload()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new DefaultState(0f),
					new ReloadState(3f, 0.5f),
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<TransitionStateInfo>>()
				{
					{
						ListOfStates[0], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1], new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("InMotion", false, Condition<bool>.EConditionOperator.EQUAL),
									new Condition<bool>("IsShooting", false, Condition<bool>.EConditionOperator.EQUAL),
									new Condition<bool>("InputReload", false, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
						}
					},
					{
						ListOfStates[1], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[0], new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("IsReloading", false, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
							new TransitionStateInfo(ListOfStates[0], new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("InMotion", true, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
							new TransitionStateInfo(ListOfStates[0], new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("IsShooting", true, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}
		/*
		public static FSM CreateFSMMovement()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new DefaultState(0f),
					new RunState(Vector2.one),
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<TransitionStateInfo>>()
				{
					{
						ListOfStates[0], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1], null, new List<KeyValuePair<string, float>>()
							{
								new KeyValuePair<string, float>( "InputMoveX", 1f),
							}, null, null),
							new TransitionStateInfo(ListOfStates[1], null, new List<KeyValuePair<string, float>>()
							{
								new KeyValuePair<string, float>( "InputMoveX", -1f),
							}, null, null)
						}
					},
					{
						ListOfStates[1], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1], null, new List<KeyValuePair<string, float>>()
							{
								new KeyValuePair<string, float>( "InputMoveX", 0f),
							}, null, null),
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}

		public static FSM CreateFSMShoot()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new DefaultState(0f),
					new ShootState(0.2f, 0f),
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<TransitionStateInfo>>()
				{
					{
						ListOfStates[0], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1], null, null, null, new List<KeyValuePair<string, bool>>()
							{
								new KeyValuePair<string, bool>( "InputShoot", true ),
							}),
						}
					},
					{
						ListOfStates[0], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1], null, null, null, new List<KeyValuePair<string, bool>>()
							{
								new KeyValuePair<string, bool>( "Shooting", false),
								new KeyValuePair<string, bool>( "InputShoot", false),
							}),
							new TransitionStateInfo(ListOfStates[1], null, null, null, new List<KeyValuePair<string, bool>>()
							{
								new KeyValuePair<string, bool>( "OutOfAmmos", true),
								new KeyValuePair<string, bool>( "Shooting", false),
							}),
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}
		*/
	}
}
