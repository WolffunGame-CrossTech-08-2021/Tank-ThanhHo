using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBuffEffect : Effect, IShellModifier
{
    BaseShellTodo shellTodoPrefab;

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.BulletBuff;
    }

    public override Effect Clone()
    {
        return base.Clone();
    }

    public void Modify(Shell shell)
    {
        shell.AddExplodeTodo(shellTodoPrefab.Clone());
    }
}
