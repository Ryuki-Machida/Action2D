using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Wallcheck : MonoBehaviour
{
    public float m_speed;
    /// <summary>壁を検出するための ray のベクトル</summary>
    [SerializeField] Vector2 m_rayForWall = Vector2.zero;
    /// <summary>壁のレイヤー（レイヤーはオブジェクトに設定されている）</summary>
    [SerializeField] LayerMask m_wallLayer = 0;
    Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
    }

    void WallCheck()
    {
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + m_rayForWall);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_rayForWall, m_rayForWall.magnitude, m_wallLayer);

        Vector2 dir = Vector2.zero;
        if (!hit.collider)
        {
        }
        else
        {
            m_rayForWall *= -1;
            m_speed *= -1;
            transform.localScale *= new Vector2(-1, 1);
        }
        dir = Vector2.right * m_speed;
        dir.y = m_rb.velocity.y;
        m_rb.velocity = dir;
    }
    
}
