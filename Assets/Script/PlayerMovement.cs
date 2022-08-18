using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anime;
    private BoxCollider2D boxCollider;
    [SerializeField] private float WallJumpCD;
    private float HorizontalInput;
    // Start is called before the first frame update
    private void Start()
    {
        // Grab references
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        
        //flip character
        if (HorizontalInput > 0.01f)
            transform.localScale = new Vector2(1, 1);
        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();
            
        // Set animator parameters
        anime.SetBool("run", HorizontalInput != 0);
        anime.SetBool("grounded", isGrounded());

        //walljump
        if (WallJumpCD > 0.2f)
        {
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2;  

            if (Input.GetKey(KeyCode.Space))
                Jump();    
        }
        else
        {
            WallJumpCD += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anime.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded()) 
        {
            if(HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpPower, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpPower, 5);
            WallJumpCD = 0;
            
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
