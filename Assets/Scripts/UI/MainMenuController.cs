using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] RectTransform menuPanel = null;
    [SerializeField, Range(0.1f,5)] float animationDuration = 1;
    float speed;
    private void OnValidate() {
        speed = 1/animationDuration;
    }
    public void Play(){
        SceneManager.LoadScene(1);
    }
    public void OpenCredits(){
        StartCoroutine(AnimatePosition(true));
    }
    public void CloseCredits(){
        StartCoroutine(AnimatePosition(false));
    }
    public void Exit(){
        Application.Quit();
    }
    IEnumerator AnimatePosition(bool toCredits){
        float value = 0;
        float pos;
        while(value < 1){
            pos = toCredits? Mathf.Lerp(0,640,value) : Mathf.Lerp(640,0,value);
            menuPanel.position = new Vector2(menuPanel.position.x,pos);
            value += Time.unscaledDeltaTime * speed;
            yield return null;
        }
        pos = toCredits? 640:0;
        menuPanel.position = new Vector2(menuPanel.position.x,pos);
    }
}
