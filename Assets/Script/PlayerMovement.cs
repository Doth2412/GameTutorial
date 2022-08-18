using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal")
        body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

        if (HorizontalInput>0.01f)
            


        if (Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);

        
    }
}
