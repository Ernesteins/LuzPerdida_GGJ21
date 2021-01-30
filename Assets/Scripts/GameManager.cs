using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PauseEvent(bool isPaused);
    public delegate void GameEvent();
    public static event PauseEvent OnPause;
    public static event GameEvent OnGameOver;
    public static event GameEvent OnWin;
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }

    bool paused = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }        
    }
    public void Pause(){
        paused = !paused;
        OnPause?.Invoke(paused);
        Time.timeScale = paused? 0 : 1;
    }
    public void GameOver(){
        OnGameOver?.Invoke();
        Time.timeScale = 0;
    }
    public void Win(){
        OnWin?.Invoke();
    }
}
