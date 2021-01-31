using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerController : MonoBehaviour
{
    [SerializeField] DialogEvent dialogEvent = null;
    bool isTriggerEnabled = false;
    bool isTriggered = false;
    private void OnEnable() {
        dialogEvent.AddDelegateToEnable(SetTrigger);
    }
    private void OnDisable() {
        dialogEvent.RemoveDelegateToEnable(SetTrigger);
    }
    void SetTrigger(bool isEnabled){
        Debug.Log(isEnabled? "Trigger enabled":"Trigger disabled");
        isTriggerEnabled = isEnabled;
    }
    private void OnTriggerEnter(Collider other) {
        if(isTriggerEnabled && other.tag == "Player"&& !isTriggered){
            Debug.Log("Player detected");
            dialogEvent.TriggerEvent();
            isTriggered = true;
        }
    }

}
