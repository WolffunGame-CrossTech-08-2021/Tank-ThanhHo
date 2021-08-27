using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class ShellHoming : Shell, IDirectionalShell
{
	[SerializeField] private LayerMask m_TargetTankMask;
	[SerializeField] private SphereDetector m_TargetDetector;
	[SerializeField] private ColliderDetector m_HitboxDetector;

	public float m_MovingSpeed;
	public float m_TurnSpeed;
	public float m_ActiveRadius;

	private Vector3 m_CurrentDirection;
	private TargetedEffect m_CurrentTargetedEffect;

    protected override void OnEnable()
    {
        base.OnEnable();
		m_TargetDetector.enabled = true;
		m_HitboxDetector.enabled = true;
        m_RigidBody.isKinematic = false;
	}

    public override void SetLayer(int layerId)
    {
        base.SetLayer(layerId);

		m_HitboxDetector.gameObject.layer = layerId;
	}

    public override void SetUp(Vector3 position, Vector3 direction, float force)
    {
		//base.SetUp(position, direction, force);
		transform.position = position;
		direction.y = 0;
		m_CurrentDirection = direction.normalized;
		transform.forward = m_CurrentDirection;
	}

    /// <summary>
    /// Called when something enter target detector's area
    /// </summary>
    private void OnObjectEnter(GameObject other)
	{
		if (m_CurrentTargetedEffect != null) return;

		if (!Utilities.LayerMaskContain(m_TargetTankMask, other.layer)) return;
		
		TankInfo targetInfo = other.GetComponent<TankInfo>();

		if (targetInfo == null) return;

		if (targetInfo != m_Owner)
		{
			m_CurrentTargetedEffect = EffectPoolFamily.m_Instance.GetObject(EffectEnum.Targeted) as TargetedEffect;
			m_CurrentTargetedEffect.m_Owner = m_Owner;
			m_CurrentTargetedEffect.m_MaxDuration = float.PositiveInfinity;


			targetInfo.m_TankEffectManager.AddEffect(m_CurrentTargetedEffect);
			
			m_TargetDetector.enabled = false;
		}
	}

	private void OnHitboxCollide(GameObject other)
	{
		Debug.Log(other.name);
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

		m_TargetDetector.SetRadius(m_ActiveRadius);

		m_TargetDetector.OnObjectEnter += OnObjectEnter;
		m_HitboxDetector.OnObjectEnter += OnHitboxCollide;
	}

	protected override void Update()
	{
		UpdateTimeToLive();

		if (m_CurrentTargetedEffect != null)
		{
			Turn();
		}
		
		MoveFoward();

	}

	private void MoveFoward()
	{
		Vector3 movement = m_CurrentDirection * m_MovingSpeed * Time.deltaTime;
        Debug.Log(movement);
		Vector3 newPosition = m_RigidBody.position + movement;
		m_RigidBody.MovePosition(newPosition);
	}

	private void Turn()
	{
		if (m_CurrentTargetedEffect == null) return;

		Vector3 targetDirection = m_CurrentTargetedEffect.GetTargetPosition() - transform.position;
		float maxTurnAngleRadian = Mathf.Deg2Rad * m_TurnSpeed * Time.deltaTime;
		m_CurrentDirection = Vector3.RotateTowards(m_CurrentDirection, targetDirection, 
									  maxTurnAngleRadian, 1);


		transform.forward = m_CurrentDirection;
		m_CurrentDirection.Normalize();
	}

    public void SetDirection(Vector3 direction)
    {
		m_CurrentDirection = direction;
		m_CurrentDirection.y = 0;
    }

    public override void Destroy()
    {
		if (m_CurrentTargetedEffect != null)
		{
			m_CurrentTargetedEffect.Destroy();
			m_CurrentTargetedEffect = null;
		}
		m_TargetDetector.enabled = false;
		m_HitboxDetector.enabled = false;
		base.Destroy();
	}

    public override ShellEnum GetShellType()
    {
		return ShellEnum.Homing;
    }

    public override Shell Clone()
    {
        ShellHoming shellInstance = base.Clone() as ShellHoming;

        shellInstance.m_TargetTankMask = m_TargetTankMask;
        shellInstance.m_MovingSpeed = m_MovingSpeed;
        shellInstance.m_TurnSpeed = m_TurnSpeed;
        shellInstance.m_ActiveRadius = m_ActiveRadius;
        shellInstance.m_CurrentDirection = m_CurrentDirection;

        return shellInstance;
    }
}
