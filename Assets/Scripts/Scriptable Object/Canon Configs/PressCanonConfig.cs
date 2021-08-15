using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Press Canon Config", menuName = "Scriptable Object/Canons/Press Canon Config")]
public class PressCanonConfig : BaseCanonConfig
{
    public override BaseCanon GetCanon()
    {
        if(!(m_CanonPrefab is PressCanon))
        {
            Debug.Log("Press canon config cannot create canon that is not type of PressCanon");
            return null;
        }

        return base.GetCanon();
    }

    protected override Type GetDesiredCanonType()
    {
        return typeof(PressCanon);
    }
}
