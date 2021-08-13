using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RapidState
{
    BaseState,
    ShootingState,
}

public class RapidCanon : PressHoldingCanon
{
    RapidState m_CanonState;
    public float m_DelayInterval;
    public int m_MaxBulletCount;

    protected int m_CurrentBulletCount;
    protected float m_CurrentDelay;
    protected float m_CacheLaunchForce;
    protected float m_CacheMinLaunchForce;

    protected virtual void OnEnable()
    {
        m_CanonState = RapidState.BaseState;
    }

    protected override void Start()
    {
        base.Start();
        m_CacheMinLaunchForce = m_CurrentLaunchForce;
    }

    protected override void Update()
    {
        if(m_CanonState == RapidState.BaseState)
        {
            base.Update();
        }
        else if(m_CanonState == RapidState.ShootingState)
        {
            if(m_CurrentBulletCount <= 0)
            {
                m_CanonState = RapidState.BaseState;
                m_CurrentLaunchForce = m_CacheMinLaunchForce;
            }
            else
            {
                m_CurrentDelay -= Time.deltaTime;

                if(m_CurrentDelay <= 0)
                {
                    m_CurrentDelay = m_DelayInterval;
                    m_CurrentBulletCount--;
                    m_CurrentLaunchForce = m_CacheLaunchForce;
                    base.Fire();
                }
            }
        }
        else
        {
            Debug.LogError("Rapid Canon unhandled state");
        }
    }

    protected override void Fire()
    {
        m_CanonState = RapidState.ShootingState;

        m_CurrentBulletCount = m_MaxBulletCount - 1;
        m_CurrentDelay = m_DelayInterval;
        m_CacheLaunchForce = m_CurrentLaunchForce;

        base.Fire();


    }


}
