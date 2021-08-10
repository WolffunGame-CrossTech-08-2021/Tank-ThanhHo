using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShellActivator : MonoBehaviour
{
    protected Transform m_FireTransform;
    protected int m_PlayerNumber;
    protected TankInfo m_Owner;

    public abstract void Activate();

    public abstract void Deactivate();

    public void SetFireTransform(Transform fireTransform)
    {
        m_FireTransform = fireTransform;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        m_PlayerNumber = playerNumber;
    }

    public void SetOwner(TankInfo owner)
    {
        m_Owner = owner;
    }
}
