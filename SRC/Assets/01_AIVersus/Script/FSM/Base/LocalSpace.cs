using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class LocalSpace
	{
		public Dictionary<string, string> StringVars;
		public Dictionary<string, float> FloatVars;
		public Dictionary<string, int> IntVars;
		public Dictionary<string, bool> BoolVars;

		public LocalSpace()
		{
			StringVars = new Dictionary<string, string>();
			FloatVars = new Dictionary<string, float>();
			IntVars = new Dictionary<string, int>();
			BoolVars = new Dictionary<string, bool>();
		}

		public bool MatchThisVars(ConditionTransitionState Conditions)
		{
			if (Conditions.StringConditions != null)
			{
				var conditions = Conditions.StringConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!StringVars.ContainsKey(key) || StringVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.FloatConditions != null)
			{
				var conditions = Conditions.FloatConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!FloatVars.ContainsKey(key) || FloatVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.IntConditions != null)
			{
				var conditions = Conditions.IntConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!IntVars.ContainsKey(key) || IntVars[key] != conditions[i].Value)
						return false;
				}
			}

			if (Conditions.BoolConditions != null)
			{
				var conditions = Conditions.BoolConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!BoolVars.ContainsKey(key) || BoolVars[key] != conditions[i].Value)
						return false;
				}
			}

			return true;
		}
	}
}