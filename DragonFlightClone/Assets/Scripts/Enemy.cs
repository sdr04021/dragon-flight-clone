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
    public GameObject DualshotPrefab;
    public GameObject bullet;
    float bulletDamage;
    Rigidbody2D rigid2D;

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        bulletDamage = bullet.GetComponent<Bullet>().getDamage(); // �Ѿ� ������ ������
        maxHP += GameManager.gm.wave;
        hp = maxHP;
    }

    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP; // ü�¹�
        transform.Translate(Time.deltaTime * speed * Vector2.down);
        if (transform.position.y < -5) {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp -= bulletDamage; // �Ѿ� ��������ŭ ü�� ����

            if (hp <= 0)
            {
                //���� ����
                Instantiate(coinPrefab, rigid2D.position, Quaternion.identity);

                //���� Ȯ���� �ڼ� ����
                if(Random.Range(0,10) == 1) Instantiate(MagnetPrefab, rigid2D.position, Quaternion.identity);

                //���� Ȯ���� ��� ����
                if (Random.Range(0, 10) == 1) Instantiate(DualshotPrefab, rigid2D.position, Quaternion.identity);

                //�� �ı�
                Destroy(gameObject);
            }
        }
    }
}
