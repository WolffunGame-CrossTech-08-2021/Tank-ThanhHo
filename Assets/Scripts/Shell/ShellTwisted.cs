using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTwisted : Shell
{
    public float m_Speed;
    public float m_TwistedRadius;
    public float m_RotateSpeed;

    private Vector3 m_Center;
    private Vector3 m_Direction;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_RigidBody.isKinematic = false;
    }
    public override void SetUp(Vector3 position, Vector3 direction, float force)
    {
        direction.y = 0;
        direction.Normalize();

        m_Direction = direction;

        Vector3 crossVector = Vector3.Cross(direction, Vector3.up);
        transform.position = position + crossVector * m_TwistedRadius;
        transform.forward = m_Direction;

        m_Center = position;

        m_CurrentTimeToLive = m_MaxTimeToLive;
    }

    protected override void Update()
    {
        UpdateMovement();
        UpdateTimeToLive();
    }

    void UpdateMovement()
    {
        Vector3 currentPosition = m_RigidBody.position;
        float deltaTime = Time.deltaTime;

        Vector3 newPosition = MoveFoward(currentPosition, m_Direction, m_Speed, deltaTime);

        m_Center = MoveFoward(m_Center, m_Direction, m_Speed, deltaTime);

        float rotateAngle = m_RotateSpeed * deltaTime;

        newPosition = RotateAroundCenter(newPosition, m_Center, m_Direction, rotateAngle);

        m_RigidBody.MovePosition(newPosition);
    }

    /// <returns>New position after move foward</returns>
    private Vector3 MoveFoward(Vector3 position, Vector3 direction, float speed, float deltaTime)
    {
        Vector3 newPosition = position;

        Vector3 movement = direction * speed * deltaTime;

        newPosition += movement;

        return newPosition;
    }

    /// <returns>New position after rotate around a center</returns>
    private Vector3 RotateAroundCenter(Vector3 position, Vector3 center, Vector3 direction, float angle)
    {
        Vector3 newPosition = position;

        newPosition = newPosition.RotateAroundPivot(center, direction, angle);

        return newPosition;
    }

    public override ShellEnum GetShellType()
    {
        return ShellEnum.Twisted;
    }

    public override Shell Clone()
    {
        ShellTwisted shellInstance = base.Clone() as ShellTwisted;

        shellInstance.m_Speed = m_Speed;
        shellInstance.m_TwistedRadius = m_TwistedRadius;
        shellInstance.m_RotateSpeed = m_RotateSpeed;

        shellInstance.m_Center = m_Center;
        shellInstance.m_Direction = m_Direction;

        return shellInstance;
    }
}
