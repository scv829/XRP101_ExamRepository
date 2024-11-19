using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public float Score { get; set; }

    private void Awake()
    {
        if(Intance == null)
        {
            SingletonInit();
        }
        Score = 0.1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Debug.Log($"TimeScale : {Time.timeScale}");
    }

    // 다시 게임을 재개할 때 사용하는 함수
    // Pause로 줄여놓은 timeScale을 다시 원복
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
