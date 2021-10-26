using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float fallSpeed = 6.0f;
    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        if (rigid2D.position.y < -7) Destroy(gameObject);
        
    }
    private void FixedUpdate()
    {
        rigid2D.MovePosition(rigid2D.position + (Vector2.down * fallSpeed * Time.deltaTime));
    }

    void rotate()
    {
        if (angle >= 360) angle = 0;
        angle += (Time.deltaTime * 100 * (fallSpeed / 2));
        rigid2D.MoveRotation(angle);
    }
}
