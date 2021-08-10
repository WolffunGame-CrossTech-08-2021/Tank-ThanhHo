using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
	public static bool LayerMaskContain(LayerMask layerMask, int layerValue)
	{
		if (((1 << layerValue) | layerMask.value) == 0) return false;

		return true;
	}
}
