using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //public GameObject bullet; // 총알
    Rigidbody2D rigid2d;
    public float speed = 0.1f; // 이동속도
    public int playerHP = 3; // 체력
    public int gold = 0; // 돈
    public GameObject magnetEffectPrefab;
    public bool isMagnet = false;
    bool gotMagnetBefore = false;

    public GameObject bullet;   // 공격할 때 생성되는 발사체 프리펩
    public float attackRate = 0.1f;  // 공격 속도
    private int maxAttackLevel = 2;
    private int attackLevel = 1;

    /*
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        StartCoroutine("spawnBullet"); // 총알 생성
        //StartCoroutine("spawnEnemy"); // 적 생성
        StartCoroutine(MagnetEffect());
    }

    IEnumerator spawnBullet() // 0.1초마다 총알 생성
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
        yield return new WaitForSeconds(5.0f); //자석 지속시간

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
        // 플레이어 이동

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

        // 플레이어 이동이 범위를 벗어나지 않게
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4, 4), Mathf.Clamp(transform.position.y, -5, 5), 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            playerHP--;
            if (playerHP <= 0) Destroy(gameObject);
        }
        // 보스 공격에 피격
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
            // 듀얼샷 10초 지속시간 how?
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

    /* 공격 멈춤 - 일시정지
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }
    */

    private IEnumerator TryAttack()
    {
        while (true)
        {
            // 발사체 오브젝트 생성
            //Instantiate(bullet, transform.position, Quaternion.identity);
            // 공격 레벨에 따라 발사체 생성
            AttackByLevel();
            yield return new WaitForSeconds(attackRate);
        }
    }

    // 공격 레벨에 따른 발사체
    private void AttackByLevel()
    {
        switch (attackLevel)
        {
            case 1:          // Level 01 : 기존과 같이 발사체 1개 생성
                Instantiate(bullet, transform.position, Quaternion.identity);
                break;
            case 2:          // Level 02 : 간격을 두고 전방으로 발사체 2개 생성
                Instantiate(bullet, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(bullet, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
        }
    }

}
