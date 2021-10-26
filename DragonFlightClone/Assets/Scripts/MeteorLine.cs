using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorLine : MonoBehaviour
{
    Rigidbody2D playerRigid2d;
    Image meteorAlert;
    public GameObject meteorPrefab;

    bool trace = true;               //Player�� �����ϴ°�
    float speed = 0.5f;             //���� �ӵ�
    float moving_time = 1.5f;       //���� ���ӽð�

    bool oneToZero = true;          //"!" �̹��� �����ӿ�
    bool blink = true;              //"!" �̹��� ������ ����
    bool tremble = false;             //"!" �̹��� ��鸲 ����
    float blinkSpeed = 3.0f;        //"!" ������ �ӵ�
    Vector2 alertPos;

    // Start is called before the first frame update
    void Start()
    {
        //playerRigid2d = GameObject.Find("player").GetComponent<Rigidbody2D>();
        meteorAlert = GetComponentInChildren<Image>();
        alertPos = meteorAlert.transform.position;
        StartCoroutine(moveCount());
        StartCoroutine(BlinkImage());
    }

    // Update is called once per frame
    void Update()
    {
        //FollowPlayer();

        if (tremble)
        {
            meteorAlert.transform.position = alertPos + (Random.insideUnitCircle * 0.05f);
        }
    }

    void FollowPlayer()
    {
        /*
        if (trace)
        {
            float player_x = playerRigid2d.position.x;

            if (transform.position.x < player_x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x > player_x)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        */
    }

    //���� & ���� �ð� ���� �� � ����
    public IEnumerator moveCount()
    {
        yield return new WaitForSeconds(moving_time);
        trace = false;
        blink = false;
        tremble = true; alertPos = meteorAlert.transform.position;
        yield return new WaitForSeconds(1.0f);

        Instantiate(meteorPrefab, new Vector3(transform.position.x, 5.5f, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    // "!" �̹��� ������ ����
    public IEnumerator BlinkImage()
    {
        while (true)
        {
            if (oneToZero)
            {
                meteorAlert.color = new Color(meteorAlert.color.r, meteorAlert.color.g, meteorAlert.color.b, meteorAlert.color.a - (Time.deltaTime * blinkSpeed));
                if (meteorAlert.color.a <= 0)
                {
                    meteorAlert.color = new Color(meteorAlert.color.r, meteorAlert.color.g, meteorAlert.color.b, 0);
                    oneToZero = false;
                }
            }
            else
            {
                meteorAlert.color = new Color(meteorAlert.color.r, meteorAlert.color.g, meteorAlert.color.b, meteorAlert.color.a + (Time.deltaTime * blinkSpeed));
                if (meteorAlert.color.a >= 1)
                {
                    meteorAlert.color = new Color(meteorAlert.color.r, meteorAlert.color.g, meteorAlert.color.b, 1);
                    oneToZero = true;
                }
            }

            if (!blink)
            {
                meteorAlert.color = new Color(meteorAlert.color.r, meteorAlert.color.g, meteorAlert.color.b, 1);
                break;
            }

            yield return null;
        }
    }
}
