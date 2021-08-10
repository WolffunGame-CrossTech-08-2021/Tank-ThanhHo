using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    [SerializeField] Shell m_Shell;
    [SerializeField] Transform m_FireTransform;
    [SerializeField] TankInfo m_TankInfo;

    private BaseShellActivator m_CurrentActivator;

    private void OnEnable()
    {
    }


    private void Start()
    {
        m_CurrentActivator = m_Shell.GetActivator();

        m_CurrentActivator.SetFireTransform(m_FireTransform);
        m_CurrentActivator.SetOwner(m_TankInfo);
        m_CurrentActivator.SetPlayerNumber(m_PlayerNumber);

        m_CurrentActivator.transform.parent = transform;
        m_CurrentActivator.transform.position = transform.position;
        m_CurrentActivator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        m_CurrentActivator.Activate();
    }

    private void Update()
    {
    }
}