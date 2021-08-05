using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

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
                float damage = CalculateDamage(targetHealh.transform.position);

                targetHealh.TakeDamage(damage);
            }
        }

        m_ExplosionAudio.Play();
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);


        float percentDamage = 1 - distanceToTarget / m_ExplosionRadius;

        percentDamage = Mathf.Max(0, percentDamage);

        float damage = m_MaxDamage * percentDamage;

        return damage;
    }
}