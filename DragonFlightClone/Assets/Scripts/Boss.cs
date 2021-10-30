using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossPattern { Appear = 0, Phase01, Phase02, }
public class Boss : MonoBehaviour
{
    public float maxHP = 100;
    public float hp = 100;
    public float speed = 2;
    public GameObject hpbar;
    public GameObject coinPrefab;
    public GameObject bullet;
    float bulletDamage;
    Rigidbody2D rigid2D;

    private float bossStop = 2.5f;
    private BossPattern bossPattern = BossPattern.Appear;
    private Movement2D movement2D;
    private BossAttack bossAttack;


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossAttack = GetComponent<BossAttack>();
    }
    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        bulletDamage = bullet.GetComponent<Bullet>().getDamage(); // �Ѿ� ������ ������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp -= bulletDamage; // �Ѿ� ��������ŭ ü�� ����

            if (hp == 0)
            {
                //���� ����
                Instantiate(coinPrefab, rigid2D.position, Quaternion.identity);
                //�� �ı�
                Destroy(gameObject);

                GameManager.enemy_count = 0; //���� Ŭ���� �� ������
                // Enemy,Boss HP ���
                // GameManager ��ũ��Ʈ���� spawnEnemy �ڷ�ƾ ȣ��

            }
        }
    }
    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP; // ü�¹�
    }

    // ���� ���� ����
    public void ChangeState(BossPattern newState)
    {
        StopCoroutine(bossPattern.ToString());
        bossPattern = newState;
        StartCoroutine(bossPattern.ToString());
    }

    private IEnumerator Appear()
    {
        while (true)
        {
            movement2D.MoveTo(Vector3.down);
            if (transform.position.y <= bossStop)
            {
                // �̵� ���� ����
                movement2D.MoveTo(Vector3.zero);
                // Phase01�� ����
                ChangeState(BossPattern.Phase01);
            }
            yield return null;
        }
    }    

    // ���� ���� ����
    private IEnumerator Phase01()
    {
        bossAttack.StartFiring(AttackType.SingleFire);
        while (true)
        {
            // ���� ü�� 70% ������ ��
            if (hp < maxHP * 0.7f)
            {
                // 1�� ���� ����
                bossAttack.StopFiring(AttackType.SingleFire);
                // 2�� �������� ����
                ChangeState(BossPattern.Phase02);
            }
            yield return null;
        }
    }
    private IEnumerator Phase02()
    {
        // �� ���� ����
        bossAttack.StartFiring(AttackType.CircleFire);
        while(true)
        {
            // ����
            yield return null;
        }
    }
}