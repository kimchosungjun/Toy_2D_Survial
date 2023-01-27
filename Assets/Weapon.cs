using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    void Start()
    {
        Init();   
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed* Time.deltaTime); // 업데이트에서 이동할땐 Time.deltaTime 곱해주기
                break;
            default:
                break;
        }
        // Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int idx=0; idx<count; idx++)
        {
            Transform bullet;
           // PoolManager가 아닌 캐릭터 Bullet 박스에 넣기 위함
            if (idx < transform.childCount)
            {
                bullet = transform.GetChild(idx);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.forward * 360 * idx / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); // 이동 방향은 월드를 기준
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1은 무한으로 관통한다는 의미
        }
    }
}
