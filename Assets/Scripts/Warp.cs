using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Vector2 warpPoint;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject.transform.position;
        target.x = warpPoint.x;
        target.y = warpPoint.y;
        other.gameObject.transform.position = target;
    }

}
