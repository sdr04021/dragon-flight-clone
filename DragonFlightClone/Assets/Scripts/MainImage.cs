using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainImage : MonoBehaviour
{
    Image touchToStart;
    bool oneToZero = true;

    // Start is called before the first frame update
    void Start()
    {
        touchToStart = GameObject.Find("TouchToStart").GetComponent<Image>();
        StartCoroutine(BlinkText()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }


    public IEnumerator BlinkText()
    {
        while (true)
        {
            if (oneToZero)
            {
                touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, touchToStart.color.a - (Time.deltaTime));
                if (touchToStart.color.a <= 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, 0);
                    oneToZero = false;
                }
            }
            else
            {
                touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, touchToStart.color.a + (Time.deltaTime));
                if (touchToStart.color.a >= 1)
                {
                    yield return new WaitForSeconds(0.5f);
                    touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, 1);
                    oneToZero = true;
                }
            }

            yield return null;
        }
    }
}
