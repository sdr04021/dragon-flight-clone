using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rigid2D;
   //Rigidbody2D playerRigid2D;
    float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //playerRigid2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid2D.position.y < -7) Destroy(gameObject);

        //맵밖으로못나가게설정하기
        
    }
}
