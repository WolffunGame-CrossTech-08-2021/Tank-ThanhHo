using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectPoolConfig : PoolConfig<EffectEnum, Effect>
{

}

public class EffectPoolFamily : PoolFamily<EffectEnum, Effect>
{
    [SerializeField] List<EffectPoolConfig> m_EffectPoolConfig;

    protected override void SetUp()
    {
        m_PoolConfigs = new List<PoolConfig<EffectEnum, Effect>>();

        for (int i = 0; i < m_EffectPoolConfig.Count; i++)
        {
            m_PoolConfigs.Add(m_EffectPoolConfig[i]);
        }

        base.SetUp();
    }
}
