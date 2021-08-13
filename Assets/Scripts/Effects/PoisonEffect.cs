using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    public float m_Dps;
    private TankHealth m_TargetHealth;

    public override TankInfo m_Target 
    { 
        get => base.m_Target; 
        set
        {
            if(value != null)
            {
                m_TargetHealth = value.m_TankHealth;
            }
            else
            {
                m_TargetHealth = null;
            }
            base.m_Target = value;
        }
    }

    protected override void Update()
    {
        DealDamage();
        base.Update();
    }

    void DealDamage()
    {
        if(m_TargetHealth == null)
        {
            return;
        }
        else
        {
            float damage = m_Dps * Mathf.Min(Time.deltaTime, m_CurrentDuration);

            m_TargetHealth.TakeDamage(damage);
        }
    }

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Poison;
    }
}
