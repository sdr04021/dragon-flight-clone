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
    public GameObject coinPrefab;
 
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

            if (hp == 0)
            {
                //内牢 积己
                GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                float jumpDirection = 1.0f;
                if (Random.Range(0,2) == 1) jumpDirection = -1.0f;
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpDirection, 3.5f), ForceMode2D.Impulse);
                //各 颇鲍
                Destroy(gameObject);
            }
        }
    }
}
