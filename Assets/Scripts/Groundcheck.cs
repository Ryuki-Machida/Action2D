using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Groundcheck : MonoBehaviour
{
    public float m_speed;
    /// <summary>床を検出するための ray のベクトル</summary>
    [SerializeField] Vector2 m_rayForGround = Vector2.zero;
    /// <summary>床のレイヤー</summary>
    [SerializeField] LayerMask m_groundLayer = 0;
    Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        Vector2 origin = this.transform.position;
        Vector2 dir = Vector2.zero; // dir は速度ベクトル
        Debug.DrawLine(origin, origin + m_rayForGround);    // ray を Scene 上に描く
        // Raycast して床の検出を試みる
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForGround, m_rayForGround.magnitude, m_groundLayer);
        if (hit.collider)
        {
            m_speed *= -1;
            m_rayForGround *= new Vector2(-1, 1);
            transform.localScale *= new Vector2(-1, 1);
        }
        else
        {
            dir = Vector2.right * m_speed;
        }
        dir.y = m_rb.velocity.y;    // 落下については現在の値を採用する
        m_rb.velocity = dir;    // 速度ベクトルをセットする
    }    
}
