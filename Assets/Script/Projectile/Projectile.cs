using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8;

    private bool hit;
    private float direction;
    private BoxCollider2D boxCollider;
    private Animator anime;
    public float lifetime;
    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed =  speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5)
            Disappear();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anime.SetTrigger("explode");
    }

    public void AttackDirection(float _direction)
    {   
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }

    public void Disappear()
    {
        lifetime = 0;
        gameObject.SetActive(false);
    }
}
