using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform worldRoot;

    [Header("生成設定")]
    public int startPlatformCount = 15;

    public float spawnWidth = 8f;

    public float verticalGap = 3f;

    public GameObject player;

    [Header("平台大小")]
    public Vector2 sizeRange =
        new Vector2(1.5f, 4f);

    [Header("回收")]
    public float recycleHeight = 10f;

    public float respawnY = -40f;

    List<GameObject> activePlatforms =
        new List<GameObject>();

    Queue<PlatformType> currentBatch =
        new Queue<PlatformType>();

    public int currentLayer = 1;
    public int platformCount;

    void Start()
    {
        GameManager.Instance.roundManager.OnGameStart += SpawnStartPlatforms;
    }

    void Update()
    {
        CheckRecycle();
    }

    void SpawnStartPlatforms()
    {
        for (int i = 0; i < startPlatformCount; i++)
        {
            SpawnPlatform(i);
        }
        GameManager.Instance.playerMovementManager.rb.position = activePlatforms[5].transform.position + Vector3.up * 10f;
        Debug.Log(player.transform.position);
    }

    void SpawnPlatform(int num)
    {
        if (currentBatch.Count <= 0)
        {
            GenerateNextBatch();
        }

        PlatformType type =
            currentBatch.Dequeue();
        if (num == 5 && type != PlatformType.Normal) type = PlatformType.Normal;


        GameObject obj =
            PoolManager.Instance.GetPlatform(type);

        obj.transform.SetParent(worldRoot);

        RandomizePlatform(obj, num * -verticalGap);

        activePlatforms.Add(obj);
    }

    void RandomizePlatform(GameObject obj, float y)
    {
        float randomX =
            Random.Range(-spawnWidth, spawnWidth);

        float randomZ =
            Random.Range(-spawnWidth, spawnWidth);

        obj.transform.position =
            new Vector3(randomX, y, randomZ);

        float size =
            Random.Range(sizeRange.x, sizeRange.y);

        PlatformBase platform =
            obj.GetComponent<PlatformBase>();

        platform.Setup(
            new Vector3(size, 1, size)
        );
    }

    void CheckRecycle()
    {
        for (int i = 0; i < activePlatforms.Count; i++)
        {
            GameObject obj = activePlatforms[i];

            if (obj.transform.position.y > recycleHeight)
            {
                activePlatforms[i] =
                    ReplacePlatform(obj);
            }
        }
    }

    GameObject ReplacePlatform(GameObject oldObj)
    {
        platformCount++;
        currentLayer = platformCount / 10 + 1;
        if (currentBatch.Count <= 0)
        {
            GenerateNextBatch();
        }

        PlatformType type =
            currentBatch.Dequeue();

        oldObj.SetActive(false);

        GameObject newObj =
            PoolManager.Instance.GetPlatform(type);

        newObj.transform.SetParent(worldRoot);

        float randomX =
            Random.Range(-spawnWidth, spawnWidth);

        float randomZ =
            Random.Range(-spawnWidth, spawnWidth);

        newObj.transform.position =
            new Vector3(
                randomX,
                respawnY,
                randomZ
            );

        float size =
            Random.Range(sizeRange.x, sizeRange.y);
        float size2 =
            Random.Range(sizeRange.x, sizeRange.y);

        PlatformBase platform =
            newObj.GetComponent<PlatformBase>();

        platform.Setup(
            new Vector3(size, 1, size2)
        );

        return newObj;
    }

    void GenerateNextBatch()
    {

        List<PlatformType> batch =
            new List<PlatformType>();

        // ===== 普通 =====
        for (int i = 0; i < 8; i++)
        {
            batch.Add(PlatformType.Normal);
        }

        // ===== 每X層增加難度 =====
        int difficulty = currentLayer / 2;

        // 回血
        for (int i = 0; i < Mathf.Min(1, 2-difficulty/2); i++)
        {
            batch.Add(PlatformType.Heal);
        }

        // 尖刺
        for (int i = 0; i < Mathf.Min(20, difficulty/2 + 1); i++) 
        {
            batch.Add(PlatformType.Spike);
        }

        // 碎裂
        for (int i = 0; i < Mathf.Min(20, difficulty/2 + 1); i++)
        {
            batch.Add(PlatformType.Break);
        }

        for (int i = 0; i < Mathf.Min(20, difficulty/2 + 1); i++)
        {
            batch.Add(PlatformType.Speed);
        }

        for (int i = 0; i < Mathf.Min(20, difficulty-2 + 1); i++)
        {
            batch.Add(PlatformType.Slow);
        }

        for (int i = 0; i < Mathf.Min(20, difficulty/2 + 1); i++)
        {
            batch.Add(PlatformType.Bounce);
        }

        Shuffle(batch);

        currentBatch =
            new Queue<PlatformType>(batch);

        Debug.Log("生成第 " + currentLayer + " 層");
    }

    void Shuffle(List<PlatformType> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand =
                Random.Range(i, list.Count);

            PlatformType temp =
                list[i];

            list[i] = list[rand];

            list[rand] = temp;
        }
    }
}