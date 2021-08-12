using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SplitShellTodo : BaseShellTodo
{
    [SerializeField] float m_SubShellForce;
    [SerializeField] Shell m_SubShellPrefab;
    [SerializeField] float m_SplitAngle;
    [SerializeField] float m_SubShellLiftUpAngle;
    [SerializeField] float m_SubShellInitialDistance;
    [SerializeField] BaseShellTodo m_SubShellExplodeTodoPrefab;
    [SerializeField] float m_SubShellMaxTimeToLive;

    private void Split(Shell shell)
    {
        Vector3 fowardDirection = shell.transform.forward;
        Vector3 spawnPosition = shell.transform.position;

        SpawnSubShell(shell.m_Owner, m_SubShellMaxTimeToLive, spawnPosition, fowardDirection, -m_SplitAngle);
        SpawnSubShell(shell.m_Owner, m_SubShellMaxTimeToLive, spawnPosition, fowardDirection, 0);
        SpawnSubShell(shell.m_Owner, m_SubShellMaxTimeToLive, spawnPosition, fowardDirection, m_SplitAngle);
    }

    void SpawnSubShell(TankInfo owner, float maxTimeToLive, Vector3 spawnPosition, Vector3 fowardDirection, float angle)
    {
        Shell subShell = Instantiate(m_SubShellPrefab);
        subShell.m_Owner = owner;
        subShell.m_MaxTimeToLive = maxTimeToLive;

        BaseShellTodo shellTodoInstance = Instantiate(m_SubShellExplodeTodoPrefab);

        subShell.AddExplodeTodo(shellTodoInstance);

        var subShellRigidBody = subShell.GetComponent<Rigidbody>();
        Vector3 subShellDirection = Quaternion.Euler(0, angle, 0) * fowardDirection;

        subShellDirection = Vector3.RotateTowards(subShellDirection, new Vector3(0, 1, 0), Mathf.Deg2Rad * m_SubShellLiftUpAngle, 1);
        subShellDirection.Normalize();

        subShell.transform.position = spawnPosition + m_SubShellInitialDistance * subShellDirection;

        subShellRigidBody.velocity = subShellDirection * m_SubShellForce;
    }

    public override void Execute(Shell shell)
    {
        Split(shell);
    }
}
