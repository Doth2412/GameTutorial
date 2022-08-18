using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anime;
    private bool grounded;
    // Start is called before the first frame update
    private void Start()
    {
        // Grab references
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);
        
        //flip character
        if (HorizontalInput>0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (HorizontalInput<-0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && grounded == true)
            Jump();
            

        // Set animator parameters
        anime.SetBool("run", HorizontalInput != 0);
        anime.SetBool("grounded", grounded);

    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        anime.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
