using UnityEngine;

public class BreakPlatform : PlatformBase
{
    public float breakDelay = 1f;

    bool breaking;

    float timer;

    Collider col;

    MeshRenderer mesh;

    void Start()
    {
        col = GetComponent<Collider>();

        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (!breaking)
            return;

        timer += Time.deltaTime;

        // Â²³æ°{Ã{
        mesh.enabled =
            Mathf.FloorToInt(timer * 10) % 2 == 0;

        if (timer >= breakDelay)
        {
            Break();
        }
    }

    public override void OnPlayerEnter(GameObject player)
    {
        base.OnPlayerEnter(player);

        if (breaking)
            return;

        breaking = true;

        timer = 0;
    }

    void Break()
    {
        col.enabled = false;

        mesh.enabled = false;
    }

    public override void Setup(Vector3 size)
    {
        col = GetComponent<Collider>();

        mesh = GetComponent<MeshRenderer>();

        base.Setup(size);

        breaking = false;

        timer = 0;

        col.enabled = true;

        mesh.enabled = true;
    }
}