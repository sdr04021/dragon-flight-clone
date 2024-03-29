using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //public GameObject bullet; // 총알
    Rigidbody2D rigid2d;
    public float speed = 0.1f; // 이동속도
    public int playerHP = 1; // 체력
    public int gold = 0; // 돈
    public GameObject magnetEffectPrefab;
    public bool isMagnet = false;
    bool gotMagnetBefore = false;
    public TextMeshProUGUI goldText; // 돈 UI

    public GameObject bullet;   // 공격할 때 생성되는 발사체 프리펩
    public float attackRate = 0.1f;  // 공격 속도
    private int maxAttackLevel = 2;
    private int attackLevel = 1;

    bool isDualshot = false;
    bool gotDualshotBefore = false;

    public GameObject TextMagnet;
    public GameObject TextDualshot;
    
    /*
    void Start()
    {
        Load();
        StartCoroutine("spawnBullet"); // 총알 생성
        //StartCoroutine("spawnEnemy"); // 적 생성
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
        rigid2d = GetComponent<Rigidbody2D>();
        StartCoroutine(MagnetEffect());
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
            yield return new WaitForSeconds(0.75f);
        }
    }
    IEnumerator DualShot()
    {
        yield return new WaitForSeconds(5.0f); //듀얼샷 지속시간

        if (gotDualshotBefore) gotDualshotBefore = false;
        else
        {
            isDualshot = false;
            AttackLevel--;
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
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 1 * speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        }
        */
        // 플레이어 이동이 범위를 벗어나지 않게
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.9f, 2.9f), Mathf.Clamp(transform.position.y, -5, 5), 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            playerHP--;
            //if (playerHP <= 0) Destroy(gameObject);
            if (playerHP <= 0) gameObject.SetActive(false);
            Save();
            GameManager.gm.GameOver();
        }
        // 보스 공격에 피격
        if (collision.CompareTag("bossAttack"))
        {
            playerHP--;
            // if (playerHP <= 0) Destroy(gameObject);
            if (playerHP <= 0) gameObject.SetActive(false);
            Save();
            GameManager.gm.GameOver();
        }
        if (collision.gameObject.name == "Meteor(Clone)")
        {
            playerHP--;
            //if (playerHP <= 0) Destroy(gameObject);
            if (playerHP <= 0) gameObject.SetActive(false);
            Save();
            GameManager.gm.GameOver();
        }

        if (collision.gameObject.name == "Coin(Clone)")
        {
            gold++;
            goldText.text = gold.ToString();
            //GameManager.gm.gold++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Magnet(Clone)")
        {
            Destroy(collision.gameObject);
            if (isMagnet) gotMagnetBefore = true;
            else isMagnet = true;
            StartCoroutine(Magnet());
            StartCoroutine(ShowText(TextMagnet));
        }

        if (collision.gameObject.name == "Dualshot(Clone)")
        {
            Destroy(collision.gameObject);
            // 듀얼샷 10초 지속시간 how?
            if (isDualshot) gotDualshotBefore = true;
            else isDualshot = true;
            AttackLevel++;
            StartCoroutine(DualShot());
            StartCoroutine(ShowText(TextDualshot));
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

    public void Save()
    {
        if (PlayerPrefs.HasKey("gold") == false)
        {
            PlayerPrefs.SetInt("gold", gold);
        }
        else
        {
            int saved_gold = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", saved_gold + gold);
        }
        
    }
    /*
    public void Load()
    {
        gold = PlayerPrefs.GetInt("gold");
        goldText.text = gold.ToString();
    }
    */
    IEnumerator ShowText(GameObject targetText)
    {
        targetText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        targetText.SetActive(false);
    }
}




