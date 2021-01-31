using UnityEngine;

[CreateAssetMenu(fileName = "new DialogEvent", menuName = "Dialogo/DialogEvent", order = 0)]
public class DialogEvent : ScriptableObject {
    bool isActive = true;
    public delegate void Enabled(bool isEnabled);
    event Enabled OnEnable;
    public void AddDelegateToEnable(Enabled callback){
        OnEnable += callback;
    }
    public void RemoveDelegateToEnable(Enabled callback){
        OnEnable -= callback;
    }
    public void FireEnableEvent(bool state){
        isActive = state;
        OnEnable?.Invoke(state);
    }

    // Event Triggered
    public delegate void Trigger();
    event Trigger OnTrigger;
    public void AddDelegateToTrigger(Trigger callback){
        OnTrigger += callback;
    }
    public void RemoveDelegateToTrigger(Trigger callback){
        OnTrigger -= callback;
    }
    public void TriggerEvent(){
        if(!isActive) return;
        OnTrigger?.Invoke();
    }
}