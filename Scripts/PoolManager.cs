using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [Header("平台資料")]
    public List<PlatformData> platformDatas;

    Dictionary<PlatformType, Queue<GameObject>>
        platformPools =
        new Dictionary<PlatformType, Queue<GameObject>>();

    void Awake()
    {
        Instance = this;
        CreatePools();
    }

    void Start()
    {
        
    }

    void CreatePools()
    {
        foreach (PlatformData data in platformDatas)
        {
            Queue<GameObject> pool =
                new Queue<GameObject>();

            for (int i = 0; i < data.poolSize; i++)
            {
                GameObject obj =
                    Instantiate(data.prefab);

                obj.SetActive(false);

                pool.Enqueue(obj);
            }

            platformPools.Add(data.type, pool);
        }
    }

    public GameObject GetPlatform(PlatformType type)
    {
        Queue<GameObject> pool =
            platformPools[type];

        GameObject obj =
            pool.Dequeue();

        obj.SetActive(true);

        pool.Enqueue(obj);

        return obj;
    }
}