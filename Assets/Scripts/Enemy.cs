using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    private bool isLive = false;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll; // collider2D는 모든 콜라이더들을 포함한다.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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

        spr.flipX = target.position.x < rb.position.x;    
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
        coll.enabled = true; // 콜라이더는 enabled
        rb.simulated = true; // 리지드바디는 simulated
        spr.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // 두번 실행 방지
            return;
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            // 살아는 있지만, Hit Action
        }
        else
        {
            // 죽음
            isLive = false;
            coll.enabled = false; // 콜라이더는 enabled
            rb.simulated = false; // 리지드바디는 simulated
            spr.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dir = transform.position - playerPos;
        rb.AddForce(dir.normalized * 3, ForceMode2D.Impulse); // impules는 즉시 힘을 가함을 의미한다.
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
