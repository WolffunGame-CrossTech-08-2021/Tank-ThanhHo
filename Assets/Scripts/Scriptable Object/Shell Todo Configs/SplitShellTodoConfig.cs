using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Split Shell Todo", menuName = "Scriptable Object/Shell Todos/Split Shell Todo")]
public class SplitShellTodoConfig : BaseShellTodoConfig
{
    public float m_SubShellForce;
    public float m_SplitAngle;
    public float m_SubShellLiftUpAngle;

    public override BaseShellTodo GetShellTodo()
    {
        SplitShellTodo todoInstance = base.GetShellTodo() as SplitShellTodo;
        todoInstance.m_SubShellForce = m_SubShellForce;
        todoInstance.m_SplitAngle = m_SplitAngle;
        todoInstance.m_SubShellLiftUpAngle = m_SubShellLiftUpAngle;

        return todoInstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(SplitShellTodo);
    }
}
