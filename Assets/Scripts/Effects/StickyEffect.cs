using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyEffect : Effect
{
    public float m_FlatSlowValue;
    public float m_PercentSlowValue;

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Sticky;
    }
}
