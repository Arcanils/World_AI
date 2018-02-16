using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class TransitionStateInfo
	{
		public AbstractState TargetState;
		public TransitionConditions Conditions;

		public TransitionStateInfo(AbstractState TargetState, TransitionConditions Conditions = null)
		{
			this.TargetState = TargetState;
			this.Conditions = Conditions ?? new TransitionConditions();
		}
}

	public class TransitionConditions
	{
		public enum ETypeCondition
		{
			NONE,
			FORCE_TRANSITION,
			MANDATORY,
		}

		public List<Condition<bool>> BoolConditions;
		public List<Condition<float>> FloatConditions;
		public List<Condition<int>> IntConditions;
		public List<Condition<string>> StringConditions;

		public ETypeCondition EndStateMode;
		public ETypeCondition AfterDurationFinish;

		public TransitionConditions(
			List<Condition<bool>> BoolConditions = null,
			List<Condition<float>> FloatConditions = null,
			List<Condition<int>> IntConditions = null,
			List<Condition<string>> StringConditions = null,
			ETypeCondition EndStateMode = ETypeCondition.NONE,
			ETypeCondition AfterDurationFinish = ETypeCondition.MANDATORY)
		{
			this.BoolConditions = BoolConditions ?? new List<Condition<bool>>();
			this.FloatConditions = FloatConditions ?? new List<Condition<float>>();
			this.IntConditions = IntConditions ?? new List<Condition<int>>();
			this.StringConditions = StringConditions ?? new List<Condition<string>>();
			this.EndStateMode = EndStateMode;
			this.AfterDurationFinish = AfterDurationFinish;
		}
	}

	public struct Condition<T> where T : System.IComparable
	{
		public enum EConditionOperator
		{
			EQUAL,
			DIFFERENT,
			SUPERIOR,
			SUPERIOR_OR_EQUAL,
			INFERIOR,
			INFERIOR_OR_EQUAL,
		}
		public string Key;
		public T Value;
		public EConditionOperator Operator;


		public Condition(string Key, T Value, EConditionOperator Operator)
		{
			this.Key = Key;
			this.Value = Value;
			this.Operator = Operator;
		}

		public bool IsValide(T DataToCompare)
		{
			bool result;
			switch(Operator)
			{
				case EConditionOperator.EQUAL:
					result = Value.Equals(DataToCompare);
					break;
				case EConditionOperator.DIFFERENT:
					result = !Value.Equals(DataToCompare);
					break;
				case EConditionOperator.SUPERIOR:
					result = Value.CompareTo(DataToCompare) < 0;
					break;
				case EConditionOperator.SUPERIOR_OR_EQUAL:
					result = Value.CompareTo(DataToCompare) <= 0;
					break;
				case EConditionOperator.INFERIOR:
					result = Value.CompareTo(DataToCompare) > 0;
					break;
				case EConditionOperator.INFERIOR_OR_EQUAL:
					result = Value.CompareTo(DataToCompare) >= 0;
					break;
				default:
					result = false;
					break;
			}

			return result;
		}
	}
}