using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSnowflake : Shell
{
	[SerializeField] private LayerMask m_TargetTankMask;
	[SerializeField] private SphereDetector m_TargetDetector;
	[SerializeField] private ColliderDetector m_HitboxDetector;

	public float m_MovingSpeed;
	public float m_ActiveRadius;
	public float m_BigExplosionRadius;
	public float m_BigExplosionMaxDamage;

	private Vector3 m_CurrentDirection;

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
		if (!m_TargetTankMask.LayerMaskContain(other.layer)) return;

		TankInfo targetInfo = other.GetComponent<TankInfo>();

		if (targetInfo == null) return;

		if (targetInfo != m_Owner)
		{
			MakeBigExplode();
			base.OnExplode();

			m_TargetDetector.enabled = false;
		}
	}

	private void OnHitboxCollide(GameObject other)
	{
		Debug.Log(other.name);
		base.OnExplode();
	}

	private void MakeBigExplode()
    {
		ExplosionShellTodo explosionTodo = ShellTodoPoolFamily.m_Instance.GetObject(ShellTodoEnum.Explosion) as ExplosionShellTodo;
		explosionTodo.m_ExplosionRadius = m_BigExplosionRadius;
		explosionTodo.m_MaxDamage = m_BigExplosionMaxDamage;
		explosionTodo.m_ExplosionForce = 0;

		explosionTodo.Execute(this);
		explosionTodo.Destroy();
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

		MoveFoward();
	}

	private void MoveFoward()
	{
		Vector3 movement = m_CurrentDirection * m_MovingSpeed * Time.deltaTime;
		Vector3 newPosition = m_RigidBody.position + movement;
		m_RigidBody.MovePosition(newPosition);
	}

	public override void Destroy()
	{
		m_TargetDetector.enabled = false;
		m_HitboxDetector.enabled = false;
		base.Destroy();
	}

	public override ShellEnum GetShellType()
	{
		return ShellEnum.Snowflake;
	}

    public override Shell Clone()
    {
        ShellSnowflake shellInstance = base.Clone() as ShellSnowflake;

        shellInstance.m_TargetTankMask = m_TargetTankMask;
        shellInstance.m_MovingSpeed = m_MovingSpeed;
        shellInstance.m_ActiveRadius = m_ActiveRadius;
        shellInstance.m_BigExplosionRadius = m_BigExplosionRadius;
        shellInstance.m_BigExplosionMaxDamage = m_BigExplosionMaxDamage;
        shellInstance.m_CurrentDirection = m_CurrentDirection;

        return shellInstance;
    }
}
