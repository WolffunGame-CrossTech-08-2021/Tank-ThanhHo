﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExplosionShellTodo : BaseShellTodo
{
	public LayerMask m_TankMask;
	public ParticleSystem m_ExplosionParticlesPrefab;
	public AudioSource m_ExplosionAudio;
	public float m_MaxDamage = 100f;
	public float m_ExplosionForce = 1000f;
	public float m_ExplosionRadius = 5f;

	private void Explode(Vector3 position)
	{
		// Find all the tanks in an area around the shell and damage them.
		Collider[] colliders = Physics.OverlapSphere(position, m_ExplosionRadius, m_TankMask);

		foreach (var collider in colliders)
		{
			Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();

			if (targetRigidBody != null)
			{
				targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
			}

			TankHealth targetHealh = collider.GetComponent<TankHealth>();

			if (targetHealh != null)
			{
				float damage = CalculateDamage(position, targetHealh.transform.position);

				targetHealh.TakeDamage(damage);
			}
		}

		m_ExplosionAudio.Play();
		ParticleSystem explosionParticle = Instantiate(m_ExplosionParticlesPrefab, position, Quaternion.identity);
		explosionParticle.Play();

		Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
	}

    private float CalculateDamage(Vector3 currentPosition, Vector3 targetPosition)
	{
		// Calculate the amount of damage a target should take based on it's position.
		float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);

		float percentDamage = 1 - distanceToTarget / m_ExplosionRadius;

		percentDamage = Mathf.Max(0, percentDamage);

		float damage = m_MaxDamage * percentDamage;

		return damage;
	}

    public override void Execute(Shell shell)
    {
		Explode(shell.transform.position);
	}
}
