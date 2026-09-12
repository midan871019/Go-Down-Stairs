using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("跳躍")]
    public float jumpForce = 8f;

    [Header("地板檢測")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("下落")]
    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2f;

    public Rigidbody rb;

    Camera mainCam;

    bool isGrounded;

    float horizontal;
    float vertical;

    public Animator animator;

    void Start()
    {

        mainCam = Camera.main;

        // 防止玩家亂翻轉
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!GameManager.Instance.roundManager.IsPlaying()) return;

        InputHandler();

        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        AnimationCheck();
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.roundManager.IsPlaying()) return;

        Move();
        BetterFall();
    }

    void InputHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (GameManager.Instance.floatingJoystick.input != Vector2.zero)
        {
            horizontal = GameManager.Instance.floatingJoystick.input.x;

            vertical = GameManager.Instance.floatingJoystick.input.y;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );
    }

    void Move()
    {
        Vector3 camForward = mainCam.transform.forward;
        Vector3 camRight = mainCam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection =
            camForward * vertical +
            camRight * horizontal;

        moveDirection.Normalize();

        Vector3 velocity =
            moveDirection * moveSpeed * GameManager.Instance.playerEffectManager.speedMultiplier;

        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        // 平滑轉向
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector3(
                rb.velocity.x,
                0,
                rb.velocity.z
            );

            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }

    void BetterFall()
    {
        // 下墜加速
        if (rb.velocity.y < 0)
        {
            rb.velocity +=
                Vector3.up *
                Physics.gravity.y *
                (fallMultiplier - 1) *
                Time.fixedDeltaTime;
        }

        // 放開跳躍鍵時更快下降
        else if (rb.velocity.y > 0 &&
                 !Input.GetKey(KeyCode.Space))
        {
            rb.velocity +=
                Vector3.up *
                Physics.gravity.y *
                (lowJumpMultiplier - 1) *
                Time.fixedDeltaTime;
        }
    }

    public void Bounce(float power)
    {
        animator.SetTrigger("Jump");
        Vector3 v =
        rb.velocity;


        v.y = power;


        rb.velocity = v;
    }

    void AnimationCheck()
    {
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat(
        "VerticalSpeed",
        rb.velocity.y
    );
    }
}