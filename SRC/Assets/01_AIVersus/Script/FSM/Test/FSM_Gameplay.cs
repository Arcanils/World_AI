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
			Move = CreateFSMMovement(); //CreateFSMMovement();
			Shoot = CreateFSMShoot(); //CreateFSMShoot();
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
									new Condition<bool>("InputReload", true, Condition<bool>.EConditionOperator.EQUAL),
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
		
		public static FSM CreateFSMMovement()
		{
			var ListOfStates = new List<DefaultState>()
				{
					new DefaultState(0f),
					new RunState(0f),
				};

			var MapOfTransitions = new Dictionary<AbstractState, List<TransitionStateInfo>>()
				{
					{
						ListOfStates[0], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[1],  new TransitionConditions(
								null,
								new List<Condition<float>>()
								{
									new Condition<float>("InputMoveX", 0f, Condition<float>.EConditionOperator.DIFFERENT),
								}
							)),
						}
					},
					{
						ListOfStates[1], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[0],  new TransitionConditions(
								null,
								new List<Condition<float>>()
								{
									new Condition<float>("InputMoveX", 0f, Condition<float>.EConditionOperator.EQUAL),
								}
							)),
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
							new TransitionStateInfo(ListOfStates[1],  new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("InputShoot", true, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
						}
					},
					{
						ListOfStates[1], new List<TransitionStateInfo>()
						{
							new TransitionStateInfo(ListOfStates[0],  new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("IsShooting", false, Condition<bool>.EConditionOperator.EQUAL),
									new Condition<bool>("InputShoot", false, Condition<bool>.EConditionOperator.EQUAL),
								}
							)),
							new TransitionStateInfo(ListOfStates[0],  new TransitionConditions(
								new List<Condition<bool>>()
								{
									new Condition<bool>("OutOfAmmos", true, Condition<bool>.EConditionOperator.EQUAL),
									new Condition<bool>("IsShooting", false, Condition<bool>.EConditionOperator.EQUAL),
								}
							))
						}
					}
				};
			var CurrentState = ListOfStates[0];

			return new FSM(ListOfStates, MapOfTransitions, CurrentState);
		}
	}
}
