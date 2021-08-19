using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitingCanon : PressHoldingCanon
{
    public float m_SplitAngle;
    public int m_BulletCount;

    protected override void Fire()
    {
        float mostLeftAngle = - m_SplitAngle * (int)(m_BulletCount / 2);

        for(int i=0; i<m_BulletCount; i++)
        {
            Fire(mostLeftAngle + m_SplitAngle * i);
        }

        PlayShootingAudio();
    }

    private void Fire(float splitAngle)
    {
        Shell shell = InstantiateShell();

        Vector3 fireDirection = Quaternion.Euler(0, splitAngle, 0) * m_FireTransform.forward;

        shell.SetUp(m_FireTransform.position, fireDirection, m_CurrentLaunchForce);

        //Rigidbody shellRigidBody = shell.m_RigidBody;
        //shell.transform.forward = fireDirection;

        //shell.transform.position = m_FireTransform.position;

        //shellRigidBody.velocity = m_CurrentLaunchForce * shellRigidBody.transform.forward;

        NotifyCreateShell(shell);
    }
}
