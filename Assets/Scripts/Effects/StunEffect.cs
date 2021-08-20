using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect
{
    public float m_StunDuration;

    public override void StartEffect()
    {
        m_Target.ApplyStun(m_StunDuration);
        Destroy();
    }

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Stun;
    }
}
