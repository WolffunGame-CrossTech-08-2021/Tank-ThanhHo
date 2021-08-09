using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class ShellHoming : Shell
{
	[SerializeField] private LayerMask m_TankMask;
	[SerializeField] private float m_MovingSpeed;
	[SerializeField] private float m_TurnSpeed;
	[SerializeField] private TargetedEffect m_TargetedEffect;
	[SerializeField] private float m_ActiveRadius;
	[SerializeField] private float m_MaxLifetime;
	[SerializeField] private SphereDetector m_TargetDetector;
	[SerializeField] private ColliderDetector m_HitboxDetector;
	[SerializeField] private Rigidbody m_RigidBody;
	[SerializeField] private float m_MaxDamage;
	[SerializeField] private float m_MaxForce;

	[SerializeField] private Explosion m_ExplosionPrefab;
	
	private Vector3 m_CurrentDirection;
	private TankInfo m_CurrentTarget;
	
	private void OnObjectEnter(GameObject other)
	{
		if (m_CurrentTarget != null) return;

		if (!Utilities.LayerMaskContain(m_TankMask, other.layer)) return;
		
		TankInfo targetInfo = other.GetComponent<TankInfo>();

		if (targetInfo == null) return;

		if (targetInfo != m_Owner)
		{
			m_CurrentTarget = targetInfo;
			m_CurrentTarget.AddEffect(m_TargetedEffect);
			
			m_TargetDetector.enabled = false;
		}
	}

	private void OnHitboxCollide(GameObject other)
	{
		Explosion explosionInstance = Instantiate(m_ExplosionPrefab);
		explosionInstance.m_MaxDamage = m_MaxDamage;
		explosionInstance.m_ExplosionForce = m_MaxForce;
		explosionInstance.transform.position = transform.position;
		explosionInstance.Explode();
		
		Destroy(gameObject);
	}

	private void Start()
	{
		m_CurrentDirection = m_RigidBody.velocity.normalized;
		m_CurrentDirection.y = 0;

		m_RigidBody.isKinematic = true;

		transform.forward = m_CurrentDirection;

		Destroy(this.gameObject, m_MaxLifetime);

		m_TargetDetector.SetRadius(m_ActiveRadius);

		m_TargetedEffect.m_Owner = m_Owner;

		m_TargetDetector.OnObjectEnter += OnObjectEnter;
		m_HitboxDetector.OnObjectEnter += OnHitboxCollide;
	}

	private void Update()
	{
		if (m_CurrentTarget != null)
		{
			Turn();
		}
		
		MoveFoward();
	}

	private void MoveFoward()
	{
		Vector3 movement = m_CurrentDirection * m_MovingSpeed * Time.deltaTime;
		//Debug.Log(m_RigidBody.position + " : " + transform.position);
		Vector3 newPosition = m_RigidBody.position + movement;
		m_RigidBody.MovePosition(newPosition);
	}

	private void Turn()
	{
		if (m_CurrentTarget == null) return;

		Vector3 targetDirection = m_TargetedEffect.GetTargetPosition() - transform.position;
		float maxTurnAngleRadian = Mathf.Deg2Rad * m_TurnSpeed * Time.deltaTime;
		m_CurrentDirection = Vector3.RotateTowards(m_CurrentDirection, targetDirection, 
									  maxTurnAngleRadian, 1);
		
		m_CurrentDirection.Normalize();
		transform.forward = m_CurrentDirection;
	}
}
