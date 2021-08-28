using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : BaseItem
{
    public Effect m_EffectPrototype;
    public float m_MaxDuration;

    private float m_CurrentDuation;

    protected override void OnTankCollect(TankInfo tankInfo)
    {
        var effect = m_EffectPrototype.Clone();
        effect.ResetDuration();
        effect.enabled = true;

        tankInfo.m_TankEffectManager.AddEffect(effect);
    }

    private void Update()
    {
        m_CurrentDuation += Time.deltaTime;

        if(m_CurrentDuation >= m_MaxDuration)
        {
            Destroy();
        }
    }

    public override void Destroy()
    {
        m_EffectPrototype.Destroy();
        base.Destroy();
    }
}
