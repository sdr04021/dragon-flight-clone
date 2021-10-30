using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextItem : MonoBehaviour
{
    Rigidbody2D playerRigid2D;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid2D = GameObject.Find("player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerRigid2D.position + new Vector2(0,0.7f);
    }
}
