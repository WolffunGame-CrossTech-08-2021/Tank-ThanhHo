using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageEffect : Effect, IPassThroughable
{
    public float damage;

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.InstantDamage;
    }

    protected override void Update()
    {
        m_Target.m_TankHealth.TakeDamage(damage);
        Destroy();
    }
}
