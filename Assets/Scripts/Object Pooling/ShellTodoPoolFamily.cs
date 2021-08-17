using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShellTodoPoolConfig: PoolConfig<ShellTodoEnum, BaseShellTodo>
{

}

[System.Serializable]
public class ShellTodoPoolFamily : PoolFamily<ShellTodoEnum, BaseShellTodo>
{
    [SerializeField] List<ShellTodoPoolConfig> m_ShellTodoConfigs;
    protected override void SetUp()
    {
        m_PoolConfigs = new List<PoolConfig<ShellTodoEnum, BaseShellTodo>>();

        for(int i=0; i< m_ShellTodoConfigs.Count; i++)
        {
            m_PoolConfigs.Add(m_ShellTodoConfigs[i]);
        }

        base.SetUp();
    }
}
