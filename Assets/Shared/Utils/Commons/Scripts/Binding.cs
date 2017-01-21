using UnityEngine;
using System.Collections;

namespace Shiva.Utils {

	public class Binding {

		public static int ToI (string f, int def = 0)
		{
			if (f.Trim ().Length == 0)
				return def;
			
			int result = 0;
			bool b = int.TryParse (f, out result);
			if (b)
				return result;
			else
				return int.MinValue;
		}
		
		public static float ToF (string f, float def = 0)
		{
			if (f.Trim ().Length == 0)
				return def;
			
			float result = 0;
			bool b = float.TryParse (f, out result);
			
			if (b)
				return result;
			else
				return float.NaN;
		}
	}
}
