using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Press Holding Canon Config", menuName = "Scriptable Object/Canons/Press Holding Canon Config")]
public class PressHoldingCanonConfig : BaseCanonConfig
{
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;

    public override BaseCanon GetCanon()
    {
        if (!(m_CanonPrefab is PressHoldingCanon))
        {
            Debug.LogError("Press holding canon config cannot create canon that is not type of PressHoldingCanon");
            return null;
        }

        PressHoldingCanon canonInstance = base.GetCanon() as PressHoldingCanon;

        canonInstance.m_MinLaunchForce = m_MinLaunchForce;
        canonInstance.m_MaxLaunchForce = m_MaxLaunchForce;
        canonInstance.m_MaxChargeTime = m_MaxChargeTime;

        return canonInstance;
    }

    protected override System.Type GetDesiredCanonType()
    {
        return typeof(PressHoldingCanon);
    }
}
