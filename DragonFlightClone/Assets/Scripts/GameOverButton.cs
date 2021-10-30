using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GameOverClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOverClick() // 게임을 종료하고 로비로 돌아간다
    {
        GameManager.gm.enemy_count = 0;
        GameManager.gm.wave = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }
}
