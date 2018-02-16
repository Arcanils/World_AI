using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class LocalSpace
	{
		public Dictionary<string, bool> BoolVars;
		public Dictionary<string, float> FloatVars;
		public Dictionary<string, int> IntVars;
		public Dictionary<string, string> StringVars;

		public LocalSpace()
		{
			BoolVars = new Dictionary<string, bool>();
			FloatVars = new Dictionary<string, float>();
			IntVars = new Dictionary<string, int>();
			StringVars = new Dictionary<string, string>();
		}

		public bool MatchThisVars(TransitionConditions Conditions)
		{

			if (Conditions.BoolConditions != null)
			{
				var conditions = Conditions.BoolConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!BoolVars.ContainsKey(key) || !conditions[i].IsValide(BoolVars[key]))
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
					if (!FloatVars.ContainsKey(key) || !conditions[i].IsValide(FloatVars[key]))
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
					if (!IntVars.ContainsKey(key) || !conditions[i].IsValide(IntVars[key]))
						return false;
				}
			}
			if (Conditions.StringConditions != null)
			{
				var conditions = Conditions.StringConditions;
				string key;
				for (int i = conditions.Count - 1; i >= 0; --i)
				{
					key = conditions[i].Key;
					if (!StringVars.ContainsKey(key) || !conditions[i].IsValide(StringVars[key]))
						return false;
				}
			}

			return true;
		}
	}
}