using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject   meteorLinePrefab;
    public GameObject   enemy_small;        // 적
    public GameObject   enemy_normal;       // 적
    public int wave = 0;
    public int   enemy_count = 0;    // 적 등장 횟수
    public GameObject   boss;               // 보스 오브젝트
    public GameObject   textBossWarning;    // 보스 등장 텍스트
    public GameObject   WarningImageL;     //
    public GameObject   WarningImageR;

    public Sprite pauseSprite;
    public Sprite startSprite;

    public int gold = 0;

    private void Awake()
    {
        gm = this;
        textBossWarning.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(meteorGenerator());
        StartCoroutine("spawnEnemy"); // 적 생성
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //운석 랜덤생성 테스트
    public IEnumerator meteorGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 8));
            Instantiate(meteorLinePrefab, new Vector2(GameObject.Find("player").GetComponent<Rigidbody2D>().position.x, 0), Quaternion.identity);
        }
    }

    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            if (enemy_count < 10) // 작은 적 생성(10번만)
            {
                Instantiate(enemy_small, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(3, 6), Quaternion.identity);
                enemy_count++;
            }
            else if (enemy_count >= 10) // 보통 적 생성(작은 적 10번 생성 이후)
            {
                Instantiate(enemy_normal, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(3, 6), Quaternion.identity);
                enemy_count++;
            }

            if (enemy_count == 20) // 적 12번 생성 이후 보스 등장
            {
                StartCoroutine("spawnBoss");
                break;
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator spawnBoss()
    {
        // 보스 등장 이펙트 활성화
        textBossWarning.SetActive(true);

        // 보스 등장 이펙트 원래 위치로
        WarningImageL.transform.localPosition = new Vector3(-600, -370, 0);
        WarningImageR.transform.localPosition = new Vector3(600, 370, 0);

        // 3초 대기
        yield return new WaitForSeconds(3.0f);

        // 보스 등장 이펙트 비활성화
        textBossWarning.SetActive(false);
        // 보스 출현
        GameObject boss1 = Instantiate(boss, new Vector2(0, 9), Quaternion.identity);
        boss1.SetActive(true);
        boss1.GetComponent<Boss>().ChangeState(BossPattern.Appear);
    }
}
