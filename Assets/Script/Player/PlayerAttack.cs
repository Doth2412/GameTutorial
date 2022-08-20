using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anime;
    private PlayerMovement playerMovement;
    private float CDtimer;

    // Start is called before the first frame update
    void Awake()
    {
        anime = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        CDtimer = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && playerMovement.canAttack() && CDtimer > attackCD)
        {
            Attack();
        }

        CDtimer += Time.deltaTime;
    }

    private void Attack()
    {
        
        int fb = FindFireBall();
        if (fb != -1)
        {
            CDtimer = 0;
            fireballs[fb].transform.position = firePoint.position;
            fireballs[fb].GetComponent<Projectile>().AttackDirection(Mathf.Sign(transform.localScale.x));
            anime.SetTrigger("attack");
        }
        return;
        
        
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
            if (!fireballs[i].activeInHierarchy)
                return i;
        return -1;
    }
}
