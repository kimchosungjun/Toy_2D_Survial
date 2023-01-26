using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    private bool isLive = true;

    Rigidbody2D rb;
    SpriteRenderer spr;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rb.position; // 가야하는 방향
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 가야하는 방향을 normalize하고 속도를 곱함
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        spr.flipX = target.position.x - rb.position.x < 0 ? true : false;    
    }
}
