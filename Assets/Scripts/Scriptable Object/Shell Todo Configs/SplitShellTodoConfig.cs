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
        if (!(m_ShellTodoPrefab is SplitShellTodo))
        {
            Debug.LogError("Split shell todo config cannot create todo that is not type of SplitShellTodo");
            return null;
        }

        SplitShellTodo todoinstance = base.GetShellTodo() as SplitShellTodo;
        todoinstance.m_SubShellForce = m_SubShellForce;
        todoinstance.m_SubShellConfig = m_SubShellConfig;
        todoinstance.m_SplitAngle = m_SplitAngle;
        todoinstance.m_SubShellLiftUpAngle = m_SubShellLiftUpAngle;
        todoinstance.m_SubShellExplodeTodoConfig = m_SubShellExplodeTodoConfig;

        return todoinstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(SplitShellTodo);
    }
}
