using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_BaseSpeed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;

    [SerializeField] private float m_CurrentSpeed;
    
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

    private List<IMovementModifier> m_MovementModifiers = new List<IMovementModifier>();


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        RecalculateSpeed();
    }

    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
        m_MovementModifiers.Clear();
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }

    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if(Mathf.Abs(m_MovementInputValue) < 0.1 && Mathf.Abs(m_TurnInputValue) < 0.1)
        {
            if(m_MovementAudio.clip != m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.Play();
            }
        }
        else 
        {
            if(m_MovementAudio.clip != m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_CurrentSpeed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        var turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotate = Quaternion.Euler(0, turn, 0);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotate);
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    public void AddMovementModifier(IMovementModifier movementModifier)
    {
        m_MovementModifiers.Add(movementModifier);
        RecalculateSpeed();
    }

    public void RemoveMovementModifier(IMovementModifier movementModifier)
    {
        m_MovementModifiers.Remove(movementModifier);
        RecalculateSpeed();
    }

    private void RecalculateSpeed()
    {
        MovementModify movementModify = new MovementModify();
        movementModify.m_FlatMovementChange = 0;
        movementModify.m_PercentMovementChange = 1;

        for (int i = 0; i < m_MovementModifiers.Count; i++)
        {
            movementModify = m_MovementModifiers[i].ModifyMovement(movementModify);
        }

        m_CurrentSpeed = (m_BaseSpeed + movementModify.m_FlatMovementChange) * movementModify.m_PercentMovementChange;
    }
}

public struct MovementModify
{
    public float m_FlatMovementChange;
    public float m_PercentMovementChange;
}