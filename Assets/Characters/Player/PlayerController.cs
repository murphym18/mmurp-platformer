using UnityEngine;
using System.Collections;

public class  PlayerController : MonoBehaviour
{
    public Animator anim;
    public string groundLayerName = "Ground";
    public Collider2D groundTrigger;
    public float jumpTime = 0.4f;
    public Vector2 jumpVector = new Vector2(0, 2.5f);
    public Vector2 jumpImpluceVector = new Vector2(0, 15f);
    public float runForce = 50.0f;
    public float maxRunSpeed = 8.0f;

    private int groundLayer;
    private int xVelocityParam = Animator.StringToHash("X-Velocity");
    private int yVelocityParam = Animator.StringToHash("Y-Velocity");
    private int groundedParam = Animator.StringToHash("Is-On-Ground");
    private bool jumping = false;
    private Transform tx;
    private Rigidbody2D rb;
    private bool isJumpButtonDown = false;

    // Use this for initialization
    void Start()
    {
        groundLayer = LayerMask.GetMask(groundLayerName);
        rb = GetComponent<Rigidbody2D>();
    }

    private bool IsJumpButtonPressed()
    {
        return Input.GetButton("Jump");
    }

    IEnumerator JumpRoutine()
    {
        float timer = 0;
        rb.AddForce(jumpImpluceVector, ForceMode2D.Impulse);
        bool jumpKeyReleased = false;
        yield return new WaitForFixedUpdate();
        while (isJumpButtonDown && !IsGrounded())//&& timer < jumpTime)
        {
            jumpKeyReleased = !isJumpButtonDown || jumpKeyReleased;
            float proportionCompleted = timer / (2*jumpTime);
            Vector2 updateForceVec = Vector2.Lerp(jumpVector, jumpVector*.25f, proportionCompleted);

            rb.AddForce(updateForceVec, ForceMode2D.Impulse);
            //timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }


        rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y, 0));
        yield return new WaitForFixedUpdate();
        //rigidbody.gravityScale = 1;
        while (!IsGrounded() || rb.velocity.y > 0.1)
        {
            jumpKeyReleased = !isJumpButtonDown || jumpKeyReleased;
            yield return null;
        }
        while(!jumpKeyReleased)
        {
            jumpKeyReleased = !isJumpButtonDown || jumpKeyReleased;
            yield return null;
        }
        jumping = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    void Update()
    {
        isJumpButtonDown = IsJumpButtonPressed();
        if (IsGrounded() && isJumpButtonDown && !jumping)
        {
            //rigidbody.gravityScale = 0;
            jumping = isJumpButtonDown = true;
            StartCoroutine(JumpRoutine());
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim == null) return;
        var x = Input.GetAxis("Horizontal");



        var onGround = IsGrounded();
        anim.SetBool(groundedParam, onGround);
        if (x != 0)
            rb.AddForce(new Vector2(x * runForce, 0));
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
        rb.velocity = new Vector2(Mathf.Sign(x) * Mathf.Max(0, Mathf.Min(maxRunSpeed, Mathf.Abs(rb.velocity.x))), rb.velocity.y);

        anim.SetFloat(xVelocityParam, rb.velocity.x);
        anim.SetFloat(yVelocityParam, rb.velocity.y);

    }

    private bool IsGrounded()
    {
        return Physics2D.IsTouchingLayers(groundTrigger, groundLayer);
    }

}
