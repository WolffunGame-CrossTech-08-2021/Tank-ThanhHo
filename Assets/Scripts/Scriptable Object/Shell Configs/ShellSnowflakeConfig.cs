using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Snowflake Config", menuName = "Scriptable Object/Shells/Shell Snowflake Config")]
public class ShellSnowflakeConfig : BaseShellConfig
{
    [SerializeField] private float m_ActiveRadius;
    [SerializeField] private float m_BigExplosionRadius;
    [SerializeField] private float m_BigExplosionDamage;
    [SerializeField] private float m_MovingSpeed;

    public override Shell GetShell()
    {
        ShellSnowflake shellInstance = base.GetShell() as ShellSnowflake;

        shellInstance.m_ActiveRadius = m_ActiveRadius;
        shellInstance.m_BigExplosionRadius = m_BigExplosionRadius;
        shellInstance.m_BigExplosionMaxDamage = m_BigExplosionDamage;
        shellInstance.m_MovingSpeed = m_MovingSpeed;

        return shellInstance;
    }
}
