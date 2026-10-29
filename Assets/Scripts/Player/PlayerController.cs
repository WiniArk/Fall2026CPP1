using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator), typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    #endregion

    #region Ground Check
    public LayerMask groundLayer;
    public float groundCheckRadious = 0.2f;
    private bool isGrounded; 

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    private GroundCheck gndChk;
    #endregion

    #region Properties
    private int currentJumps = 0;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        gndChk = GetComponent<GroundCheck>();
        gndChk.Init(bc, rb);

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    void Update()
    {
        gndChk.CheckGround(); 

        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed && currentJumps < maxJumps)
        {
            currentJumps++;
            gndChk.SetGroundAfterJump();
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (moveX != 0f) SpriteFlip(moveX);
        if (gndChk.isGrounded) currentJumps = 0;

        anim.SetBool("isGrounded", gndChk.isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));
        if (fireInput)
        {
            anim.SetTrigger("attack");
        }
    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
}


