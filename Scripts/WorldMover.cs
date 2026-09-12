using UnityEngine;

public class WorldMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition =
            rb.position +
            Vector3.up *
            moveSpeed *
            Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
    }
}