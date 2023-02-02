using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; // fixedDeltaTime : ���� ������ �ϳ��� �Һ��� �ð�, DeltaTime�� Update���� ���
        rb.MovePosition(rb.position + nextVec);
    }
    void LateUpdate()
    {
        anim.SetFloat("Speed",inputVec.magnitude);
        if (inputVec.x!=0)
        {
            spr.flipX = inputVec.x < 0 ? true : false;
        }    
    }
}
