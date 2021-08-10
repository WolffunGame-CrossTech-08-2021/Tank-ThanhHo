using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    public float m_Dps;
    public float m_MaxDuration;

    [SerializeField] private float m_CurrentDuration;
    private TankHealth m_TargetHealth;

    public override TankInfo m_Target 
    { 
        get => base.m_Target; 
        set
        {
            if(value != null)
            {
                m_TargetHealth = value.GetComponent<TankHealth>();
            }
            else
            {
                m_TargetHealth = null;
            }
            base.m_Target = value;
        }
    }

    private void Start()
    {
        ResetDuration();
    }

    public void ResetDuration()
    {
        m_CurrentDuration = m_MaxDuration;
    }


    void Update()
    {
        DealDamage();

        UpdateDuration();
    }

    void DealDamage()
    {
        if(m_TargetHealth == null)
        {
            return;
        }
        else
        {
            float damage = m_Dps * Mathf.Min(Time.deltaTime, m_CurrentDuration);

            m_TargetHealth.TakeDamage(damage);
        }
        
    }

    void UpdateDuration()
    {
        m_CurrentDuration -= Time.deltaTime;

        if (m_CurrentDuration <= 0)
        {
            m_Target.RemoveEffect(this);
            Destroy(this.gameObject);
        }
    }
}
