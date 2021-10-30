using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject   meteorLinePrefab;
    public GameObject   enemy_small;        // ��
    public GameObject   enemy_normal;       // ��
    public int wave = 0;
    public int   enemy_count = 0;    // �� ���� Ƚ��
    public GameObject   boss;               // ���� ������Ʈ
    public GameObject   textBossWarning;    // ���� ���� �ؽ�Ʈ
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
        StartCoroutine("spawnEnemy"); // �� ����
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //� �������� �׽�Ʈ
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
            if (enemy_count < 10) // ���� �� ����(10����)
            {
                Instantiate(enemy_small, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(3, 6), Quaternion.identity);
                enemy_count++;
            }
            else if (enemy_count >= 10) // ���� �� ����(���� �� 10�� ���� ����)
            {
                Instantiate(enemy_normal, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(3, 6), Quaternion.identity);
                enemy_count++;
            }

            if (enemy_count == 20) // �� 12�� ���� ���� ���� ����
            {
                StartCoroutine("spawnBoss");
                break;
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator spawnBoss()
    {
        // ���� ���� ����Ʈ Ȱ��ȭ
        textBossWarning.SetActive(true);

        // ���� ���� ����Ʈ ���� ��ġ��
        WarningImageL.transform.localPosition = new Vector3(-600, -370, 0);
        WarningImageR.transform.localPosition = new Vector3(600, 370, 0);

        // 3�� ���
        yield return new WaitForSeconds(3.0f);

        // ���� ���� ����Ʈ ��Ȱ��ȭ
        textBossWarning.SetActive(false);
        // ���� ����
        GameObject boss1 = Instantiate(boss, new Vector2(0, 9), Quaternion.identity);
        boss1.SetActive(true);
        boss1.GetComponent<Boss>().ChangeState(BossPattern.Appear);
    }
}
