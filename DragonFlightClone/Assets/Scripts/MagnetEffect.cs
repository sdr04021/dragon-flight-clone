using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    Rigidbody2D playerRigid2D;
    bool isMagnet;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid2D = GameObject.Find("player").GetComponent<Rigidbody2D>();
        isMagnet = GameObject.Find("player").GetComponent<Player>().isMagnet;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerRigid2D.position;
        transform.localScale = transform.localScale * 0.98f;
        if ((transform.localScale.x < 0.05f) || (isMagnet == false))
        {
           Destroy(gameObject);
        }
    }
}
