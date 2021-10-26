using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg : MonoBehaviour
{
    public float speed = 0.5f;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.down);


        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, target.transform.position.y + 10.1f, 0);
        }
    }
}
