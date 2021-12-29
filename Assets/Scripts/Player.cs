using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //変数

    public Rigidbody2D Rigidbody2D;
    [SerializeField] LayerMask blockLayer;

    public float Speed = 0f;
    public int hp = 1;
    public float Jump = 0f;
    public int pointupbig = 2;
    public int pointup = 1;

    [SerializeField] SceneLoad sceneLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hpbig")
        {
            hp += pointupbig;
            other.gameObject.SetActive(false);
            this.foodTest.text = "HP:" + this.hp;
        }
        else if (other.tag == "Hpup")
        {
            hp += pointup;
            other.gameObject.SetActive(false);
            this.foodTest.text = "HP:" + this.hp;
        }
    }

    public Text foodTest;
    public bool Grounded = false;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.foodTest.text = "HP:" + this.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
        Movement();
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        foreach(Collider2D hitEnemy in hitEnemys)
        {
            Debug.Log(hitEnemy.gameObject.name + "に攻撃");
            hitEnemy.GetComponent<Enemy>().OnDamage();
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    void Movement()
    {
        //移動
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetFloat("Speed", Mathf.Abs(x));
        Rigidbody2D.velocity = new Vector2(Speed * x, Rigidbody2D.velocity.y);

        //ジャンプ
        if (IsGround() && Input.GetKeyDown("space"))
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Jump);
        }
        bool IsGround()
        {
            return Physics2D.Linecast(transform.position - transform.right * 0.3f, transform.position - transform.up * 0.3f, blockLayer)
                 || Physics2D.Linecast(transform.position + transform.right * 0.3f, transform.position - transform.up * 0.3f, blockLayer);
        }

    }

    public void OnDamage()
    {
        hp -= 1;
        animator.SetTrigger("isHurt");
        foodTest.text = "HP:" + hp;
        if (hp <= 0)
        {
            Die();
        }

        void Die()
        {
            hp = 0;
            Destroy(gameObject);
            Debug.Log("GameOver");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
        }
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Finish");
            string s = "Second";
            sceneLoad.PressStart(s);
        }
        if (collision.gameObject.tag == "Finish2")
        {
            Debug.Log("Finish");
            string s = "Final";
            sceneLoad.PressStart(s);
        }
    }

}
