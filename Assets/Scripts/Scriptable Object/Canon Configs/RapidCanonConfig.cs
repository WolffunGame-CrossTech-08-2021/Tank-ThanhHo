using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rapid Canon Config", menuName = "Scriptable Object/Canons/Rapid Canon Config")]
public class RapidCanonConfig : BaseCanonConfig
{
    public float m_DelayInterval;
    public int m_MaxBulletCount;

    public override BaseCanon GetCanon()
    {
        if(!(m_CanonPrefab is RapidCanon))
        {
            Debug.LogError("Rapid canon config cannot create canon that is not type of RapidCanon");
            return null;
        }

        RapidCanon canonInstance = base.GetCanon() as RapidCanon;
        canonInstance.m_DelayInterval = m_DelayInterval;
        canonInstance.m_MaxBulletCount = m_MaxBulletCount;

        return canonInstance;
    }

    protected override System.Type GetDesiredCanonType()
    {
        return typeof(RapidCanon);
    }
}
