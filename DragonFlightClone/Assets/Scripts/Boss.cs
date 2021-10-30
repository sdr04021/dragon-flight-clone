using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossPattern { Appear = 0, Phase01, Phase02, Phase03 }
public class Boss : MonoBehaviour
{
    public float maxHP = 10;
    public float hp = 10;
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
        bulletDamage = bullet.GetComponent<Bullet>().getDamage(); // 총알 데미지 가져옴
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp -= bulletDamage; // 총알 데미지만큼 체력 감소

            if (hp <= 0)
            {
                //코인 생성
                Instantiate(coinPrefab, rigid2D.position, Quaternion.identity);
                //몹 파괴
                Destroy(gameObject);

                GameManager.gm.enemy_count = 0;     //보스 클리어 후 재진행
                GameManager.gm.wave += 1;           // Enemy,Boss HP 향상
                maxHP = maxHP * 1.1f;
                hp = maxHP;

                GameManager.gm.StartCoroutine("spawnEnemy");
            }
        }
    }
    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP; // 체력바
    }

    IEnumerator WaitForNextWave()
    {
        yield return null; //new WaitForSeconds(3.0f);
        GameManager.gm.StartCoroutine("spawnEnemy");
    }

    // 보스 패턴 변경
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
                // 이동 방향 제거
                movement2D.MoveTo(Vector3.zero);
                // Phase01로 변경
                ChangeState(BossPattern.Phase01);
            }
            yield return null;
        }
    }    

    // 보스 공격 패턴
    private IEnumerator Phase01()
    {
        bossAttack.StartFiring(AttackType.SingleFire);
        while (true)
        {
            // 보스 체력 70% 이하일 때
            if (hp < maxHP * 0.7f)
            {
                // 1번 패턴 중지
                bossAttack.StopFiring(AttackType.SingleFire);
                // 2번 패턴으로 변경
                ChangeState(BossPattern.Phase02);
            }
            yield return null;
        }
    }
    private IEnumerator Phase02()
    {
        // 원 형태 공격
        bossAttack.StartFiring(AttackType.CircleFire);
        while(true)
        {
            // 보스 체력 40% 이하일 때
            if (hp < maxHP * 0.4f)
            {
                // 2번 패턴 중지
                bossAttack.StopFiring(AttackType.CircleFire);
                // 3번 패턴으로 변경
                ChangeState(BossPattern.Phase03);
            }
            yield return null;
        }
    }
    private IEnumerator Phase03()
    {
        // 1,2 번 패턴 모두
        bossAttack.StartFiring(AttackType.SingleFire);
        bossAttack.StartFiring(AttackType.CircleFire);
        yield return null;
    }
}
