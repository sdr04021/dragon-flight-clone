using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Rigidbody2D playerRigid2D;
    float halfWidth;
    float jumpDirection;
    bool isMagnet;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        playerRigid2D = GameObject.Find("player").GetComponent<Rigidbody2D>();
        isMagnet = GameObject.Find("player").GetComponent<Player>().isMagnet;
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        jumpDirection = Random.Range(0, 2);
        if (jumpDirection == 0) jumpDirection = -1.1f;
        else jumpDirection = 1.1f;
        rigid2D.AddForce(new Vector2(jumpDirection, 2.5f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid2D.position.y < -7) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (isMagnet) moveToPlayer();
        LeftRightBound();
    }

    void moveToPlayer()
    {
        if (Vector3.Distance(rigid2D.position, playerRigid2D.position) < 3)
        {
            Vector2 toTarget = playerRigid2D.position - rigid2D.position;
            Vector2 velocity = new Vector2(0, 0);
            float force = 450.0f;

            toTarget.Normalize();
            toTarget *= force;
            velocity += toTarget * Time.deltaTime;
            rigid2D.MovePosition(rigid2D.position + (velocity * Time.deltaTime));

            //방향에 맞춰 회전
            float degree = Mathf.Atan2(playerRigid2D.position.y - rigid2D.position.y, playerRigid2D.position.x - rigid2D.position.x) * Mathf.Rad2Deg;
            rigid2D.MoveRotation(degree + 90f);
        }
    }

    void LeftRightBound()
    {
        if (rigid2D.position.x < -3.63f + halfWidth)
        {
            rigid2D.position = new Vector2(-3.63f + halfWidth, rigid2D.position.y);
        }
        else if (rigid2D.position.x > 3.63f - halfWidth)
        {
            rigid2D.position = new Vector2(3.63f - halfWidth, rigid2D.position.y);
        }
    }
}
