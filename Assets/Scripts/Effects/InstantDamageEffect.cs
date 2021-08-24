using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageEffect : Effect, IPassThroughable
{
    public float damage;

    public override Effect Clone()
    {
        InstantDamageEffect instance = base.Clone() as InstantDamageEffect;
        instance.damage = damage;

        return instance;
    }

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
