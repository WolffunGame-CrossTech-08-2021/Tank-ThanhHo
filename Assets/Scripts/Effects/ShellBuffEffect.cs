using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBuffEffect : Effect, IShellModifier, IPassThroughable
{
    public BaseShellTodo m_ShellTodoPrefab;

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.BulletBuff;
    }

    public override Effect Clone()
    {
        ShellBuffEffect effectInstance = base.Clone() as ShellBuffEffect;
        effectInstance.m_ShellTodoPrefab = m_ShellTodoPrefab;

        return effectInstance;
    }

    public void Modify(Shell shell)
    {
        shell.AddExplodeTodo(m_ShellTodoPrefab.Clone());
    }
}
