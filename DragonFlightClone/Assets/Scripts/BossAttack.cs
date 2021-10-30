using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 범위 얼마이상 나가면 삭제하는거
// 데미지는 플레이어 스크립트에 이미 있어
// 
public enum AttackType { CircleFire = 0, SingleFire}

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bossAttackPrefab;

    public void StartFiring(AttackType attackType)
    {
        // attackType 열거형의 이름과 같은 코루틴을 실행
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    /* 화면 밖으로 나가면 삭제
    private void LateUpdate()
    {
        if (transform.position.y < -6.0f  ||
             transform.position.y > 6.0f ||
             transform.position.x < -2.6f ||
             transform.position.x > 2.6f )
        {
            Destroy(gameObject);
        }
    }
    */
    private IEnumerator SingleFire()
    {
        Vector3 targetPosition = Vector3.zero;
        float attackRate = 0.1f;
        while (true)
        {
            // 발사체 생성
            GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
            // 발사체 이동 방향 - 랜덤
            Vector3 direction = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,-0.5f), 0).normalized;
            // 발사체 이동 방향 설정
            clone.GetComponent<Movement2D>().MoveTo(direction);

            yield return new WaitForSeconds(attackRate);
        }
    }
    private IEnumerator CircleFire()
    {
        float attackRate = 0.8f;
        int count = 24;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                // 발사체 생성
                GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
                // 발사체 이동 각도
                float angle = weightAngle + intervalAngle * i;
                // 발사체 이동 방향
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // x축 이동
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f); // y축 이동
                // 발사체 이동 방향 설정
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            // 발사체 각도 변경 7.5도 라디안으로
            weightAngle += 0.13f; 
            // attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);
        }
    }
}
