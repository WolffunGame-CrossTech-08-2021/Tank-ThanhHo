using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPoison : Shell
{
    [SerializeField] ParticleSystem m_ExplossionParticle;
    [SerializeField] PoisonArea m_PoisonAreaPrefab;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_PoisonRadius;
    [SerializeField] float m_PoisonAreaDuration;
    [SerializeField] float m_PoisonEffectDuration;
    [SerializeField] float m_Dps;
    [SerializeField] PressHoldingActivator m_PressHoldingActivator;

    private void Update()
    {
        transform.forward = m_Rigidbody.velocity.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_ExplossionParticle.transform.parent = null;
        //m_ExplossionParticle.transform.position = transform.position;
        m_ExplossionParticle.Play();

        Vector3 poisonAreaPosition = transform.position;
        poisonAreaPosition.y = 0;
        PoisonArea poisonAreaInstance = Instantiate(m_PoisonAreaPrefab, poisonAreaPosition, Quaternion.identity);
        poisonAreaInstance.SetUp(m_Owner, m_PoisonRadius, m_PoisonAreaDuration, m_PoisonEffectDuration, m_Dps);

        Destroy(gameObject);
    }

    public override BaseShellActivator GetActivator()
    {
        PressHoldingActivator activatorInstance = Instantiate(m_PressHoldingActivator);
        activatorInstance.SetShell(this);

        return activatorInstance;
    }
}
