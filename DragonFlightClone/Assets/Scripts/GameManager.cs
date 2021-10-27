using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject meteorLinePrefab;
    public GameObject enemy_small; // 적
    public GameObject enemy_normal; // 적
    int enemy_count = 0; // 적 등장 횟수

    public int gold = 0;

    private void Awake()
    {
        gm = this;
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

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            if (enemy_count < 3) // 작은 적 생성(10번만)
            {
                Instantiate(enemy_small, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_small, new Vector2(3, 6), Quaternion.identity);
                enemy_count++;
            }
            else if (enemy_count >= 3) // 보통 적 생성(작은 적 10번 생성 이후)
            {
                Instantiate(enemy_normal, new Vector2(-3, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(-1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(0, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(1.5f, 6), Quaternion.identity);
                Instantiate(enemy_normal, new Vector2(3, 6), Quaternion.identity);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
