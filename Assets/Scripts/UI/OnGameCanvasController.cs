using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameCanvasController : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen = null;   
    [SerializeField] GameObject gameOverScreen = null;   
    [SerializeField] GameObject winScreen = null;   
    private void OnEnable() {
        GameManager.OnPause += ActivatePauseMenu;
        GameManager.OnGameOver += ActivateGameOverMenu;
    }
    private void OnDisable() {
        GameManager.OnPause -= ActivatePauseMenu;
        GameManager.OnGameOver -= ActivateGameOverMenu;
    }
    void ActivateWinMenu(){
        gameOverScreen.SetActive(true);
    }
    void ActivateGameOverMenu(){
        gameOverScreen.SetActive(true);
    }
    void ActivatePauseMenu(bool isPaused){
        pauseScreen.SetActive(isPaused);
    }
    public void Pause(){
        GameManager.instance.Pause();
    }
}
