using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedEffect : Effect, IPassThroughable
{
    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Targeted;
    }

    public Vector3 GetTargetPosition()
	{
		return m_Target.transform.position;
	}
}
