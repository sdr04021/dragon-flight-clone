using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.1f;
    public float damage = 1f; // 총알 대미지

    void Update()
    {
        // 총알 이동
        transform.Translate(new Vector3(0, 1 * speed * Time.deltaTime, 0)); 
        if (transform.position.y > 5) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이랑 충돌했을 때
        if (collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    public float getDamage()
    {
        //Debug.Log(PlayerPrefs.GetInt("level"));
        return PlayerPrefs.GetInt("level") + 1;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
}
