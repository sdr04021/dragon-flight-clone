using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���̻� ������ �����ϴ°�
// �������� �÷��̾� ��ũ��Ʈ�� �̹� �־�
// 
public enum AttackType { CircleFire = 0, SingleFire}

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bossAttackPrefab;

    public void StartFiring(AttackType attackType)
    {
        // attackType �������� �̸��� ���� �ڷ�ƾ�� ����
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    /* ȭ�� ������ ������ ����
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
            // �߻�ü ����
            GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
            // �߻�ü �̵� ���� - ����
            Vector3 direction = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,-0.5f), 0).normalized;
            // �߻�ü �̵� ���� ����
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
                // �߻�ü ����
                GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
                // �߻�ü �̵� ����
                float angle = weightAngle + intervalAngle * i;
                // �߻�ü �̵� ����
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // x�� �̵�
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f); // y�� �̵�
                // �߻�ü �̵� ���� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            // �߻�ü ���� ���� 7.5�� ��������
            weightAngle += 0.13f; 
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate);
        }
    }
}
