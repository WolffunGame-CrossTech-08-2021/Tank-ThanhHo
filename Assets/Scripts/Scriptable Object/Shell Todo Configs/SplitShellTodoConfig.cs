using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Split Shell Todo", menuName = "Scriptable Object/Shell Todos/Split Shell Todo")]
public class SplitShellTodoConfig : BaseShellTodoConfig
{
    public float m_SubShellForce;
    public BaseShellConfig m_SubShellConfig;
    public float m_SplitAngle;
    public float m_SubShellLiftUpAngle;
    public BaseShellTodoConfig m_SubShellExplodeTodoConfig;

    public override BaseShellTodo GetShellTodo()
    {
        SplitShellTodo todoInstance = base.GetShellTodo() as SplitShellTodo;
        todoInstance.m_SubShellForce = m_SubShellForce;
        todoInstance.m_SubShellConfig = m_SubShellConfig;
        todoInstance.m_SplitAngle = m_SplitAngle;
        todoInstance.m_SubShellLiftUpAngle = m_SubShellLiftUpAngle;
        todoInstance.m_SubShellExplodeTodoConfig = m_SubShellExplodeTodoConfig;

        return todoInstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(SplitShellTodo);
    }
}
