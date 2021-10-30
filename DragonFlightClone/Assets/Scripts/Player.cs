using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //public GameObject bullet; // �Ѿ�
    Rigidbody2D rigid2d;
    public float speed = 0.1f; // �̵��ӵ�
    public int playerHP = 3; // ü��
    public int gold = 0; // ��
    public GameObject magnetEffectPrefab;
    public bool isMagnet = false;
    bool gotMagnetBefore = false;

    public GameObject bullet;   // ������ �� �����Ǵ� �߻�ü ������
    public float attackRate = 0.1f;  // ���� �ӵ�
    private int maxAttackLevel = 2;
    private int attackLevel = 1;

    /*
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        StartCoroutine("spawnBullet"); // �Ѿ� ����
        //StartCoroutine("spawnEnemy"); // �� ����
        StartCoroutine(MagnetEffect());
    }

    IEnumerator spawnBullet() // 0.1�ʸ��� �Ѿ� ����
    {
        while (true)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

    }
    */
    private void Start()
    {
        StartCoroutine("TryAttack");
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
        // ���� ���ݿ� �ǰ�
        if (collision.CompareTag("bossAttack"))
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

        if (collision.gameObject.name == "Dualshot(Clone)")
        {
            Destroy(collision.gameObject);
            // ��� 10�� ���ӽð� how?
            AttackLevel++;
        }
    }

    

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    /* ���� ���� - �Ͻ�����
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }
    */

    private IEnumerator TryAttack()
    {
        while (true)
        {
            // �߻�ü ������Ʈ ����
            //Instantiate(bullet, transform.position, Quaternion.identity);
            // ���� ������ ���� �߻�ü ����
            AttackByLevel();
            yield return new WaitForSeconds(attackRate);
        }
    }

    // ���� ������ ���� �߻�ü
    private void AttackByLevel()
    {
        switch (attackLevel)
        {
            case 1:          // Level 01 : ������ ���� �߻�ü 1�� ����
                Instantiate(bullet, transform.position, Quaternion.identity);
                break;
            case 2:          // Level 02 : ������ �ΰ� �������� �߻�ü 2�� ����
                Instantiate(bullet, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(bullet, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
        }
    }

}
