using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform player;

    public float rayDistance = 20f;

    public float minSize = 0.3f;
    public float maxSize = 1.5f;


    public float minAlpha = 0.1f;
    public float maxAlpha = 0.7f;


    MeshRenderer mesh;


    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }


    void Update()
    {
        FindGround();
    }


    void FindGround()
    {
        Ray ray =
            new Ray(
                player.position,
                Vector3.down
            );


        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            rayDistance
        ))
        {
            Vector3 pos =
                hit.point;


            // 放到平台表面
            transform.position =
                pos + Vector3.up * 0.02f;



            float distance =
                hit.distance;


            float t =
                Mathf.InverseLerp(
                    rayDistance,
                    0,
                    distance
                );


            // 越近越大
            float size =
                Mathf.Lerp(
                    minSize,
                    maxSize,
                    t
                );


            transform.localScale =
                new Vector3(
                    size,
                    size,
                    size
                );


            SetAlpha(t);
        }
    }



    void SetAlpha(float value)
    {
        Color c =
            mesh.material.color;


        c.a =
            Mathf.Lerp(
                minAlpha,
                maxAlpha,
                value
            );


        mesh.material.color = c;
    }
}
