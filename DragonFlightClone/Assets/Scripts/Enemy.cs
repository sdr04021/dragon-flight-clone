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
    public GameObject bullet;
    float bulletDamage;
    Rigidbody2D rigid2D;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        bulletDamage = bullet.GetComponent<Bullet>().getDamage(); // 총알 데미지 가져옴
    }

    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP; // 체력바
        transform.Translate(Time.deltaTime * speed * Vector2.down);
        if (transform.position.y < -5) {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp -= bulletDamage; // 총알 데미지만큼 체력 감소

            if (hp == 0)
            {
                //코인 생성
                Instantiate(coinPrefab, rigid2D.position, Quaternion.identity);

                //일정 확률로 자석 생성
                if(Random.Range(0,10) == 1) Instantiate(MagnetPrefab, rigid2D.position, Quaternion.identity);

                //몹 파괴
                Destroy(gameObject);
            }
        }
    }
}
