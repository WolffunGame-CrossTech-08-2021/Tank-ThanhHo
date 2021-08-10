using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSplit : Shell
{
    [SerializeField] float m_MaxLifetime;

    [SerializeField] float m_SubShellMaxDamage;
    [SerializeField] float m_SubShellForce;
    [SerializeField] ShellExplosion m_SubShellPrefab;
    [SerializeField] Explosion m_Explosion;
    [SerializeField] Rigidbody m_RigidBody;
    [SerializeField] float m_SplitAngle;
    [SerializeField] float m_SubShellLiftUpAngle;
    [SerializeField] float m_SubShellInitialDistance;
    [SerializeField] PressHoldingActivator m_PressHoldingActivator;

    private float m_CurrentLifetime;

    private void Start()
    {
        m_CurrentLifetime = m_MaxLifetime;
    }

    private void Update()
    {
        transform.forward = m_RigidBody.velocity.normalized;

        m_CurrentLifetime -= Time.deltaTime;
        
        if(m_CurrentLifetime <= 0)
        {
            Split();
        }
    }

    private void Split()
    {
        m_Explosion.transform.parent = null;
        m_Explosion.transform.position = transform.position;
        m_Explosion.Explode();

        Vector3 fowardDirection = transform.forward;

        SpawnSubShell(fowardDirection, -m_SplitAngle);
        SpawnSubShell(fowardDirection, 0);
        SpawnSubShell(fowardDirection, m_SplitAngle);

        Destroy(gameObject);
    }

    void SpawnSubShell(Vector3 fowardDirection, float angle)
    {
        ShellExplosion subShell = Instantiate(m_SubShellPrefab);
        subShell.m_Owner = m_Owner;
        subShell.m_MaxDamage = m_SubShellMaxDamage;
        var subShellRigidBody = subShell.GetComponent<Rigidbody>();
        Vector3 subShellDirection = Quaternion.Euler(0, angle, 0) * fowardDirection;

        subShellDirection = Vector3.RotateTowards(subShellDirection, new Vector3(0, 1, 0), Mathf.Deg2Rad * m_SubShellLiftUpAngle, 1);
        subShellDirection.Normalize();

        subShell.transform.position = transform.position + m_SubShellInitialDistance * subShellDirection;

        subShellRigidBody.velocity = subShellDirection * m_SubShellForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Explosion.transform.parent = null;
        m_Explosion.transform.position = transform.position;
        m_Explosion.Explode();

        Destroy(gameObject);
    }

    public override BaseShellActivator GetActivator()
    {
        PressHoldingActivator activatorInstance = Instantiate(m_PressHoldingActivator);
        activatorInstance.SetShell(this);

        return activatorInstance;
    }
}
