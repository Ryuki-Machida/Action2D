using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 0;
    public GameObject LeafSlash;
    public GameObject Sweat;
    public Rigidbody2D Rigidbody2D;
    public Animator Animator;
    public int Pattern = 0;
    public bool Jumped = false;
    public bool Grounded = false;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    [SerializeField] float m_stop = 1f;
    float m_timer;

    public float ElapsedTime;
    public float BaseTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = new Vector2(speed, Rigidbody2D.velocity.y);

        if (attackPoint)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_stop)
            {
                m_timer = 0f;

                Animator.SetTrigger("Attack");
                Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
                foreach (Collider2D hitPlayer in hitPlayers)
                {
                    Debug.Log(hitPlayer.gameObject.name + "に攻撃");
                    hitPlayer.GetComponent<Player>().OnDamage();

                }
            }
        }

        if (Pattern == 0)
        {
            Pattern1();
        }
        if(Pattern == 2)
        {
            Rigidbody2D.velocity = new Vector2(-10, Rigidbody2D.velocity.y);
        }
        if(Pattern ==3)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if(Pattern == 6)
        {
            ElapsedTime += Time.deltaTime;
            Rigidbody2D.velocity = new Vector2(4, Rigidbody2D.velocity.y);

            if(ElapsedTime > BaseTime && Jumped == false)
            {
                Jumped = true;
                ElapsedTime = 0;
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 12);
            }
        }
        if(Pattern == 7)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Jumped == true && Grounded == false)
        {
            Grounded = true;
            Animator.SetTrigger("Walk");
        }
    }
    //立ちアニメ
    void Pattern1()
    {
        Pattern = 1;
        Invoke("Pattern2", 2);
    }
    //歩きアニメ
    void Pattern2()
    {
        Pattern = 2;
        Animator.SetTrigger("Walk");
        Invoke("Pattern3", 2);
    }
    void Pattern3()
    {
        Pattern = 3;
        Invoke("Pattern4", 0.5f);
    }
    //攻撃アニメ
    void Pattern4()
    {
        Pattern = 4;
        Animator.SetTrigger("Attack");
        Invoke("Pattern5", 0.5f);
    }
    void Pattern5()
    {
        Pattern = 5;
        Animator.SetTrigger("Attack");
        Invoke("Pattern6", 1);
    }
    void Pattern6()
    {
        ElapsedTime = 0;
        Jumped = false;
        Grounded = false;

        Pattern = 6;
        Animator.SetTrigger("Walk");
        Invoke("Pattern7", 5);
    }
    void Pattern7()
    {
        Pattern = 7;
        Invoke("Pattern2", 2);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    
}
