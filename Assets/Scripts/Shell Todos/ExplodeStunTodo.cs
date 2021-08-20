using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeStunTodo : BaseShellTodo
{
	[SerializeField] LayerMask m_TankMask;
	[SerializeField] ParticleSystem m_ExplodePrefab;

	public float m_StunDuration;
    public float m_ExplosionRadius;

	public override void Execute(Shell shell)
    {
		Collider[] colliders = Physics.OverlapSphere(shell.transform.position, m_ExplosionRadius, m_TankMask);

		foreach (var collider in colliders)
		{
			TankInfo targetInfo = collider.GetComponent<TankInfo>();

			if (targetInfo != null)
			{
				StunEffect effectInstance = EffectPoolFamily.m_Instance.GetObject(EffectEnum.Stun) as StunEffect;

				effectInstance.m_StunDuration = m_StunDuration;

				targetInfo.AddEffect(effectInstance);
			}
		}

		ParticleSystem explodeParticleInstance = Instantiate(m_ExplodePrefab);
		explodeParticleInstance.transform.position = shell.transform.position;
		explodeParticleInstance.transform.localScale = new Vector3(m_ExplosionRadius, m_ExplosionRadius, m_ExplosionRadius);
		explodeParticleInstance.Play();
		Destroy(explodeParticleInstance.gameObject, explodeParticleInstance.main.duration);
	}

    public override ShellTodoEnum GetShellTodoType()
    {
        return ShellTodoEnum.ExplodeStun;
    }
}
