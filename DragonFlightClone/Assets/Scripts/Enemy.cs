using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHP = 5;
    public float hp = 5;
    public float speed = 2;
    public GameObject hpbar;
    public GameObject coinPrefab;
    public GameObject MagnetPrefab;

    Rigidbody2D rigid2D;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP;
        transform.Translate(Time.deltaTime * speed * Vector2.down);
        if (transform.position.y < -5) {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp--;

            if (hp == 0)
            {
                //内牢 积己
                Instantiate(coinPrefab, rigid2D.position, Quaternion.identity);

                //老沥 犬伏肺 磊籍 积己
                if(Random.Range(0,10) == 1) Instantiate(MagnetPrefab, rigid2D.position, Quaternion.identity);

                //各 颇鲍
                Destroy(gameObject);
            }
        }
    }
}
