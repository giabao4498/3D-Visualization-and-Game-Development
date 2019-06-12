using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Zero : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpHeight;
    private bool isRight;
    private bool isGrounded;
    private bool isJumped;
    const int airLayer = 1;
    private string[] enemy = new string[] {
        "Sigma_0",
        "Enemy1_5",
        "Enemy2_0"
    };
    void Start()
    {
        isRight = true;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (gameObject)
            Inp();
    }
    void FixedUpdate()
    {
        if (gameObject)
        {
            float horizontal = Input.GetAxis("Horizontal");
            isGrounded = IsOnGround();
            Move(horizontal);
            Flip(horizontal);
            Layer();
            Reset();
        }
    }
    private void Inp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isJumped = true;
    }
    private void Reset()
    {
        isJumped = false;
    }
    private void Move(float horizontal)
    {
        if (rigidbody.velocity.y < 0)
            animator.SetBool("land", true);
        if (isGrounded && isJumped)
        {
            isGrounded = false;
            rigidbody.AddForce(new Vector2(0, jumpHeight));
            animator.SetTrigger("jump");
        }
        rigidbody.AddForce(new Vector2(horizontal * moveSpeed, 0));
        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !isRight || horizontal < 0 && isRight)
        {
            isRight = !isRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private bool IsOnGround()
    {
        if (rigidbody.velocity.y <= 0)
        foreach (Transform point in groundPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, radius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
            {
                animator.ResetTrigger("jump");
                animator.SetBool("land", false);
                return true;
            }
        }
        return false;
    }
    private void Layer()
    {
        if (!isGrounded)
            animator.SetLayerWeight(airLayer, 1);
        else
            animator.SetLayerWeight(airLayer, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy.Contains(collision.gameObject.name))
            Destroy(gameObject);
    }
}