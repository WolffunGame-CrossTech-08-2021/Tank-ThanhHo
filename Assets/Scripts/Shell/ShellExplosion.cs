using System.Runtime.CompilerServices;
using UnityEngine;

public class ShellExplosion : Shell
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;
    [SerializeField] private Explosion m_ExplossionPrefab;
    private bool m_Exploded = false;
    [SerializeField] private Rigidbody m_RigidBody;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void Update()
    {
        transform.forward = m_RigidBody.velocity.normalized;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        
        Explosion explosionInstance = Instantiate(m_ExplossionPrefab);
        explosionInstance.transform.position = transform.position;
        explosionInstance.m_TankMask = m_TankMask;
        explosionInstance.m_ExplosionForce = m_ExplosionForce;
        explosionInstance.m_MaxDamage = m_MaxDamage;
        explosionInstance.m_ExplosionRadius = m_ExplosionRadius;
        
        explosionInstance.Explode();
        Destroy(gameObject);
    }
}