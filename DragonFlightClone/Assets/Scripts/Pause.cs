using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool pauseActive = false;
    Image button;

    private void Awake()
    {
        button = GameObject.Find("Button").GetComponent<Image>();
    }

    public void pauseBtn()
    {
        if (pauseActive)
        {
            Time.timeScale = 1;
            pauseActive = false;
            button.sprite = GameManager.gm.pauseSprite;
        }
        else
        {
            Time.timeScale = 0;
            pauseActive = true;
            button.sprite = GameManager.gm.startSprite;
        }
    }
}
