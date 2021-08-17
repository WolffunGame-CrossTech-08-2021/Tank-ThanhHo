using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShellPoolConfig: PoolConfig<ShellEnum, Shell>
{

}

public class ShellPoolFamily: PoolFamily<ShellEnum, Shell>
{
    [SerializeField] List<ShellPoolConfig> m_ShellPoolConfigs;

    protected override void SetUp()
    {
        m_PoolConfigs = new List<PoolConfig<ShellEnum, Shell>>();
        for(int i=0; i< m_ShellPoolConfigs.Count; i++)
        {
            m_PoolConfigs.Add(m_ShellPoolConfigs[i]);
        }
        base.SetUp();
    }
}
