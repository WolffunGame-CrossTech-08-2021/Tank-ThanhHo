using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Twisted Config", menuName = "Scriptable Object/Shells/Shell Twisted Config")]
public class ShellTwistedConfig : BaseShellConfig
{
    public float m_Speed;
    public float m_TwistedRadius;
    public float m_RotateSpeed;

    public override Shell GetShell()
    {
        ShellTwisted shellInstance = base.GetShell() as ShellTwisted;

        shellInstance.m_Speed = m_Speed;
        shellInstance.m_TwistedRadius = m_TwistedRadius;
        shellInstance.m_RotateSpeed = m_RotateSpeed;

        return shellInstance;
    }
}
