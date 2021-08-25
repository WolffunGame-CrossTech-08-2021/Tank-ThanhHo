using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SplitShellTodo : BaseShellTodo
{
    public float m_SubShellForce;
    public float m_SplitAngle;
    public float m_SubShellLiftUpAngle;

    private void Split(Shell shell)
    {
        Vector3 fowardDirection = shell.transform.forward;
        Vector3 spawnPosition = shell.transform.position;

        SpawnSubShell(shell, spawnPosition, fowardDirection, -m_SplitAngle);
        SpawnSubShell(shell, spawnPosition, fowardDirection, 0);
        SpawnSubShell(shell, spawnPosition, fowardDirection, m_SplitAngle);
    }

    void SpawnSubShell(Shell shell, Vector3 spawnPosition, Vector3 fowardDirection, float angle)
    {
        //Shell subShell = m_SubShellConfig.GetShell();
        //subShell.m_Owner = shell.m_Owner;
        //subShell.SetLayer(shell.gameObject.layer);

        //BaseShellTodo shellTodoInstance = m_SubShellExplodeTodoConfig.GetShellTodo();

        //subShell.AddExplodeTodo(shellTodoInstance);

        //var subShellRigidBody = subShell.GetComponent<Rigidbody>();
        //Vector3 subShellDirection = Quaternion.Euler(0, angle, 0) * fowardDirection;

        //subShellDirection = Vector3.RotateTowards(subShellDirection, new Vector3(0, 1, 0), Mathf.Deg2Rad * m_SubShellLiftUpAngle, 1);
        //subShellDirection.Normalize();

        //subShell.transform.position = spawnPosition;

        //subShellRigidBody.velocity = subShellDirection * m_SubShellForce;
    }

    public override void Execute(Shell shell)
    {
        Split(shell);
    }

    public override ShellTodoEnum GetShellTodoType()
    {
        return ShellTodoEnum.Split;
    }

    public override BaseShellTodo Clone()
    {
        SplitShellTodo todoInstance = ShellTodoPoolFamily.m_Instance.GetObject(GetShellTodoType()) as SplitShellTodo;

        todoInstance.m_SubShellForce = m_SubShellForce;
        todoInstance.m_SplitAngle = m_SplitAngle;
        todoInstance.m_SubShellLiftUpAngle = m_SubShellLiftUpAngle;

        return todoInstance;
    }
}
