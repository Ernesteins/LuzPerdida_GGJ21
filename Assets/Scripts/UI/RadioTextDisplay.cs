using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RadioTextDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay = null;

    void Start()
    {
        Hide();
    }

    public void Display(string text, float screenTime){
        Invoke("Hide",screenTime);
        textDisplay.text = text;
    }
    void Hide(){
        textDisplay.text = "";
    }
}
