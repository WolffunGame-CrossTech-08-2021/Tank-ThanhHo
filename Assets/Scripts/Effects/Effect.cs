using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public TankInfo m_Owner;
    private TankInfo _m_Target;

    public virtual TankInfo m_Target
    {
        get { return _m_Target; }
        set
        {
            _m_Target = value;
        }
    }

    public virtual void Init(TankInfo owner)
    {
        m_Owner = owner;
    }
}
