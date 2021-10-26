using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHP = 5;
    public float hp = 5;
    public float speed = 2;
    public GameObject hpbar;
 
    void Update()
    {
        hpbar.GetComponent<Image>().fillAmount = hp / maxHP;
        transform.Translate(Time.deltaTime * speed * Vector2.down);
        if (transform.position.y < -5) {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp--;
            if (hp == 0) {
                Destroy(gameObject);
            } 
        }
    }
}
