using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private enum GunState
    {
        Idle,
        Charging,
        Fire,
    }

    GunState m_Gunstate;
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        if(m_Gunstate == GunState.Idle)
        {
            m_AimSlider.value = m_MinLaunchForce;
            if (Input.GetButtonDown(m_FireButton))
            {
                m_Gunstate = GunState.Charging;
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.loop = true;
                m_ShootingAudio.Play();
            }
        }
        else if(m_Gunstate == GunState.Charging)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;
            

            if(Input.GetButtonUp(m_FireButton))
            {
                m_Gunstate = GunState.Fire;
            }

            if(m_CurrentLaunchForce >= m_MaxLaunchForce)
            {
                m_CurrentLaunchForce = m_MaxLaunchForce;
                m_Gunstate = GunState.Fire;
                
            }
        }
        else if(m_Gunstate == GunState.Fire)
        {
            Fire();

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.loop = false;
            m_ShootingAudio.Play();

            m_CurrentLaunchForce = m_MinLaunchForce;
            m_Gunstate = GunState.Idle;
            m_AimSlider.value = m_MinLaunchForce;
        }
        else
        {
            Debug.LogError("Tank shooting goes into undefined state");
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        Rigidbody shell = Instantiate(m_Shell);
        shell.transform.position = m_FireTransform.position;
        shell.transform.rotation = m_FireTransform.rotation;

        shell.velocity = m_CurrentLaunchForce * shell.transform.forward;
    }
}