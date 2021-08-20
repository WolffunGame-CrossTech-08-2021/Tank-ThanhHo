using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static bool LayerMaskContain(this LayerMask layerMask, int layerValue)
	{
		if (((1 << layerValue) | layerMask.value) == 0) return false;

		return true;
	}

	public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 axis, float angle)
    {
		Quaternion rotation = Quaternion.AngleAxis(angle, axis);
		return rotation * (point - pivot) + pivot;
    }
}
