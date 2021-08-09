using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedEffect : Effect
{
	public Vector3 GetTargetPosition()
	{
		return m_Target.transform.position;
	}
}
