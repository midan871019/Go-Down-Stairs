using UnityEngine;

[System.Serializable]
public class PlatformData
{
    public PlatformType type;

    public GameObject prefab;

    public int poolSize = 10;
}