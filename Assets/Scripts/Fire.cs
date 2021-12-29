using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Fire : MonoBehaviour
{
    public float speed = 0;
    public Rigidbody2D Rigidbody2D;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    [SerializeField] float m_stop = 1f;
    float m_timer;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

                Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
                foreach (Collider2D hitPlayer in hitPlayers)
                {
                    Debug.Log(hitPlayer.gameObject.name + "に攻撃");
                    hitPlayer.GetComponent<Player>().OnDamage();

                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
