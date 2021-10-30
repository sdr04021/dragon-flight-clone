using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
    // 화면 밖 삭제
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
