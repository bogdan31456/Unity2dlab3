using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float HorizontalMove = 0f;
   // public float speed = 1f;
    private bool FacingRight=true;
    [Header("Player Settings")]
    [Range(0f, 10f)] public float speed = 1f;
    [Range(0f, 15f)  ] public float jumpforce= 8f;

    public Animator animator;
    [Space]
    [Header("Ground Settings")]
    public bool isGrounded=false;
    [Range(-5f, 5f)] public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
        }
        HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));
        if(isGrounded==false)
        {
            animator.SetBool("Jumping",true);
        }
        else
        {
            animator.SetBool("Jumping",false);
        }

        if (HorizontalMove < 0 && FacingRight)
        {
            Flip();
        }
        else if(HorizontalMove > 0 && !FacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity=new Vector2(HorizontalMove*10f,rb.velocity.y);
        rb.velocity = targetVelocity;
        CheckGround();
    }
     private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);
        if(colliders.Length>1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
