﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect : MonoBehaviour
{
    public TankInfo m_Owner;
    private TankInfo _m_Target;

    [HideInInspector] public float m_MaxDuration;
    [SerializeField] protected float m_CurrentDuration;

    public virtual TankInfo m_Target
    {
        get { return _m_Target; }
        set
        {
            _m_Target = value;
        }
    }
    protected virtual void OnDisable()
    {
        m_Target = null;
        m_Owner = null;
        m_MaxDuration = 0;
        m_CurrentDuration = 0;
    }

    public virtual void StartEffect()
    {
        m_CurrentDuration = m_MaxDuration;
        gameObject.SetActive(true);
    }

    public abstract EffectEnum GetEffectType();

    protected virtual void Update()
    {
        UpdateDuration();
    }

    public void ResetDuration()
    {
        m_CurrentDuration = m_MaxDuration;
    }

    protected virtual void UpdateDuration()
    {
        m_CurrentDuration -= Time.deltaTime;

        if (m_CurrentDuration <= 0)
        {
            if(m_Target != null)
            {
                m_Target.m_TankEffectManager.RemoveEffect(this);
            }

            Destroy();
        }
    }

    public virtual void Destroy()
    {
        if(m_Target != null)
        {
            m_Target.m_TankEffectManager.RemoveEffect(this);
        }
        m_Target = null;

        EffectPoolFamily.m_Instance.ReturnObjectToPool(GetEffectType(), this);
    }
}
