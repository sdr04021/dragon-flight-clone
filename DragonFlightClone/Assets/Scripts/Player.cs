using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject bullet; // �Ѿ�
    //public GameObject meteor; // �
    public GameObject enemy_small; // ��
    public GameObject enemy_normal; // ��
    // public GameObject warn; // ����ǥ
    Rigidbody2D rigid2d;
    public float speed = 0.1f; // �̵��ӵ�
    public int playerHP = 3; // ü��
    public int gold = 0; // ��

    public GameObject magnetEffectPrefab;
    public bool isMagnet = false;
    bool gotMagnetBefore = false;


    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        StartCoroutine("spawnBullet"); // �Ѿ� ����
        StartCoroutine("spawnEnemy"); // �� ����
        StartCoroutine(MagnetEffect());
    }

    IEnumerator spawnBullet() // 0.2�ʸ��� �Ѿ� ����
    {
        while (true)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }
    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy_small, new Vector2(-3, 6), Quaternion.identity);
            Instantiate(enemy_small, new Vector2(-1.5f, 6), Quaternion.identity);
            Instantiate(enemy_small, new Vector2(0, 6), Quaternion.identity);
            Instantiate(enemy_small, new Vector2(1.5f, 6), Quaternion.identity);
            Instantiate(enemy_small, new Vector2(3, 6), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator Magnet()
    {
        yield return new WaitForSeconds(5.0f); //�ڼ� ���ӽð�

        if (gotMagnetBefore) gotMagnetBefore = false;
        else isMagnet = false;
    }
    IEnumerator MagnetEffect()
    {
        while (true)
        {
            if (isMagnet) Instantiate(magnetEffectPrefab, rigid2d.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        // �÷��̾� �̵�

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(1 * speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 1 * speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        }

        // �÷��̾� �̵��� ������ ����� �ʰ�
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4, 4), Mathf.Clamp(transform.position.y, -5, 5), 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            playerHP--;
            if (playerHP <= 0) Destroy(gameObject);
        }

        if (collision.gameObject.name == "Coin(Clone)")
        {
            GameManager.gm.gold++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Magnet(Clone)")
        {
            Destroy(collision.gameObject);
            if (isMagnet) gotMagnetBefore = true;
            else isMagnet = true;
            StartCoroutine(Magnet());
        }
    }
}
