using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] int numOffFlashes = 3;
    [SerializeField, Range(0,3)] float flashTime = 0.5f;
    [SerializeField] KeyCode linternaKey = KeyCode.Q;
    [SerializeField] LayerMask enemyLayer = 1;
    [SerializeField] DetectionSphere sphere = null;
    [SerializeField] Light linternaLight = null;

    public delegate void FlashesCount(int count);
    public static event FlashesCount OnFlashesCountChange;
    
    private void Start() {
        OnFlashesCountChange?.Invoke(numOffFlashes);
        TurnOff();
    }
    private void Update() {
        if(Input.GetKeyDown(linternaKey)){
            TurnOn();
        }
    }
    void TurnOn(){
        if(numOffFlashes<=0) return;
        numOffFlashes--;
        OnFlashesCountChange?.Invoke(numOffFlashes);

        Collider[] colliders = Physics.OverlapSphere(sphere.transform.position,sphere.radius, enemyLayer);
        if(colliders == null) return;
        foreach (Collider c in colliders)
        {
            EnemyMovement enemy = c.GetComponent<EnemyMovement>();
            enemy?.Stunning();
        }

        linternaLight.enabled = true;
        Invoke("TurnOff",flashTime);
    }
    void TurnOff(){
        linternaLight.enabled = false;
    }
}