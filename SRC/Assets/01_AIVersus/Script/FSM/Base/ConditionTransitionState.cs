using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class ConditionTransitionState
	{
		public AbstractState ToState;
		public List<KeyValuePair<string, string>> StringConditions;
		public List<KeyValuePair<string, float>> FloatConditions;
		public List<KeyValuePair<string, int>> IntConditions;
		public List<KeyValuePair<string, bool>> BoolConditions;

		public ConditionTransitionState(AbstractState ToState, List<KeyValuePair<string, string>> StringConditions, List<KeyValuePair<string, float>> FloatConditions,
			List<KeyValuePair<string, int>> IntConditions, List<KeyValuePair<string, bool>> BoolConditions)
		{
			this.ToState = ToState;
			this.StringConditions = StringConditions ?? new List<KeyValuePair<string, string>>();
			this.FloatConditions = FloatConditions ?? new List<KeyValuePair<string, float>>();
			this.IntConditions = IntConditions ?? new List<KeyValuePair<string, int>>();
			this.BoolConditions = BoolConditions ?? new List<KeyValuePair<string, bool>>();
		}
	}
}