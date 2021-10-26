using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject meteorLinePrefab;
    GameObject player;

    int coin = 0;

    private void Awake()
    {
        gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");

        StartCoroutine(meteorGenerator());
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
            Instantiate(meteorLinePrefab, new Vector2(player.GetComponent<Rigidbody2D>().position.x, 0), Quaternion.identity);
        }
    }
}
