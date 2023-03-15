using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")] // �ν������� �Ӽ����� �̻ڰ� ���н����ִ� Ÿ��Ʋ
    public float gameTime;
    public float maxGameTime= 2*10;
    [Header("# Player Info")]
    public int health;
    public int maxHealth=100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp= { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    void Awake()
    {
        instance = this;
        //health = maxHealth;
    }


    void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
