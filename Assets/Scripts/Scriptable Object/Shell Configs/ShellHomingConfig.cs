using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Homing Config", menuName = "Scriptable Object/Shells/Shell Homing Config")]
public class ShellHomingConfig : BaseShellConfig
{
	public float m_MovingSpeed;
	public float m_TurnSpeed;
	public float m_ActiveRadius;

    public override Shell GetShell()
    {
        if(!(m_ShellPrefab is ShellHoming))
        {
            Debug.LogError("Shell homing config cannot create shell that is not type of ShellHoming");
            return null;
        }

        ShellHoming shellInstance = base.GetShell() as ShellHoming;
        shellInstance.m_MovingSpeed = m_MovingSpeed;
        shellInstance.m_TurnSpeed = m_TurnSpeed;
        shellInstance.m_ActiveRadius = m_ActiveRadius;

        return shellInstance;
    }

    protected override Type GetDesiredShellType()
    {
        return typeof(ShellHoming);
    }
}
