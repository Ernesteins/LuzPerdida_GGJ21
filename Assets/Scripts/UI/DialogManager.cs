using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    [SerializeField] RadioTextDisplay display = null;
    // Start is called before the first frame update
    [SerializeField] Dialog[] dialogs = null;

    int currentDialog = 0;
    AudioSource audioSource;
    private void OnEnable() {
        foreach (Dialog d in dialogs)
        {
            d.dialogEvent.AddDelegateToTrigger(OnEventTriggered);
        }
    }
    private void OnDisable() {
        foreach (Dialog d in dialogs)
        {
            d.dialogEvent.RemoveDelegateToTrigger(OnEventTriggered);
        }
    }
    void OnEventTriggered(){
        Debug.Log("Event Triggered");
        if(currentDialog< dialogs.Length){
            display.Display(dialogs[currentDialog].text,dialogs[currentDialog].displayTime);
            AudioClip clip = dialogs[currentDialog].voiceClip;
            if(dialogs != null){
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        currentDialog++;
        SetUpNextDialogEvent(currentDialog);
    }
    private void Start() {
        SetUpNextDialogEvent(currentDialog);
        audioSource = GetComponent<AudioSource>();
    }
    void SetUpNextDialogEvent(int index){
        for(int i=0; i<dialogs.Length;i++)
        {
            dialogs[i].dialogEvent.FireEnableEvent(i==index);
        }
    }

    [System.Serializable]
    public class Dialog{
        [TextArea(3,8)]
        public string text = "";

        [Range(1,10)]
        public float displayTime = 3;
        public AudioClip voiceClip = null;
        public DialogEvent dialogEvent = null;
    }
}
