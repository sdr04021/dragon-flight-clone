using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
    // ȭ�� �� ����
    private void LateUpdate()
    {
        if ( transform.position.y < -6.0f ||
             transform.position.y >  6.0f ||
             transform.position.x < -4.0f ||
             transform.position.x >  4.0f)
        {
            Destroy(gameObject);
        }
    }
}
