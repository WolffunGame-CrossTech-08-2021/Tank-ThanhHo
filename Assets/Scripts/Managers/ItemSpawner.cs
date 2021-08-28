using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] List<BaseItemConfig> m_ItemsConfig;
    [SerializeField] float m_FirstItemDelay;
    [SerializeField] float m_SpawnInterval;
    [SerializeField] List<Transform> m_SpawnPositions;
    [SerializeField] float m_SpawnPosHeight;

    const float MAX_TRY_FOR_RANDOM_POSITION = 30;

    private float m_CountdownUntilSpawnTime;
    private bool m_enabled = false;

    private void Update()
    {
        if(m_enabled)
        {
            m_CountdownUntilSpawnTime -= Time.deltaTime;

            if(m_CountdownUntilSpawnTime <= 0)
            {
                m_CountdownUntilSpawnTime = m_SpawnInterval;
                RandomSpawnItem();
            }
        }
    }

    private void RandomSpawnItem()
    {
        BaseItemConfig randomedItemConfig = GetRandomItemConfig();
        Vector3 spawnPosition = GetRandomSpawnPosition();

        SpawnItem(randomedItemConfig, spawnPosition);
    }

    private BaseItemConfig GetRandomItemConfig()
    {
        int randomResult = Random.Range(0, m_ItemsConfig.Count);

        return m_ItemsConfig[randomResult];
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomResult = Random.Range(0, m_SpawnPositions.Count);

        return m_SpawnPositions[randomResult].position;
    }

    private void SpawnItem(BaseItemConfig itemConfig, Vector3 positon)
    {
        var item = itemConfig.GetItem();

        positon.y = m_SpawnPosHeight;

        item.transform.position = positon;
    }

    public void SetActive(bool status)
    {
        if(status == true && m_enabled == false)
        {
            m_enabled = true;
            m_CountdownUntilSpawnTime = m_FirstItemDelay;
        }

        if(status == false && m_enabled == true)
        {
            m_enabled = true;
            m_CountdownUntilSpawnTime = float.PositiveInfinity;
        }
    }
}
