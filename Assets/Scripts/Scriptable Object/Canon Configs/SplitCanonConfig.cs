using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Split Canon Config", menuName = "Scriptable Object/Canons/Split Canon Config")]
public class SplitCanonConfig : BaseCanonConfig
{
    public float m_SplitAngle;
    public int m_BulletCount;

    public override BaseCanon GetCanon()
    {
        if(!(m_CanonPrefab is SplitingCanon))
        {
            Debug.LogError("Split canon config cannot create canon that is not type of SplitingCanon");
            return null;
        }

        SplitingCanon canonInstance = base.GetCanon() as SplitingCanon;
        canonInstance.m_SplitAngle = m_SplitAngle;
        canonInstance.m_BulletCount = m_BulletCount;

        return canonInstance;
    }

    protected override System.Type GetDesiredCanonType()
    {
        return typeof(SplitingCanon);
    }
}
