using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class ShellHoming : Shell, DirectionalShell
{
	[SerializeField] private LayerMask m_TargetTankMask;
	[SerializeField] private float m_MovingSpeed;
	[SerializeField] private float m_TurnSpeed;
	[SerializeField] private TargetedEffect m_TargetedEffect;
	[SerializeField] private float m_ActiveRadius;
	[SerializeField] private SphereDetector m_TargetDetector;
	[SerializeField] private ColliderDetector m_HitboxDetector;
	
	private Vector3 m_CurrentDirection;
	private TankInfo m_CurrentTarget;
	
	/// <summary>
	/// Called when something enter target detector's area
	/// </summary>
	private void OnObjectEnter(GameObject other)
	{
		if (m_CurrentTarget != null) return;

		if (!Utilities.LayerMaskContain(m_TargetTankMask, other.layer)) return;
		
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
		base.OnExplode();
	}

    protected override void OnTriggerEnter(Collider other)
    {
        // We do nothing because the shell homing put collider on the child game object,
		// 1 for target detect, another for hitbox detect
		// so the trigger here is call whenever a child's collider is triggered.

		// We need to override because base class do something with OnTriggerEnter.

		// Instead, use OnHitboxCollide and OnObjectEnter for desired functional.
    }

    protected override void Start()
	{
		base.Start();

		m_RigidBody.isKinematic = true;

		transform.forward = m_CurrentDirection;

		m_TargetDetector.SetRadius(m_ActiveRadius);

		m_TargetedEffect.m_Owner = m_Owner;

		m_TargetDetector.OnObjectEnter += OnObjectEnter;
		m_HitboxDetector.OnObjectEnter += OnHitboxCollide;
	}

	protected override void Update()
	{
		base.Update();

		if (m_CurrentTarget != null)
		{
			Turn();
		}
		
		MoveFoward();
	}

	private void MoveFoward()
	{
		Vector3 movement = m_CurrentDirection * m_MovingSpeed * Time.deltaTime;
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

    public void SetDirection(Vector3 direction)
    {
		m_CurrentDirection = direction;
		m_CurrentDirection.y = 0;
    }
}
