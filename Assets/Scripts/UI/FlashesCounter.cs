using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashesCounter : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab = null;
    private void OnEnable() => Linterna.OnFlashesCountChange += UpdateFlashesCount;
    private void OnDisable() => Linterna.OnFlashesCountChange -= UpdateFlashesCount;

    void UpdateFlashesCount(int count){
        Debug.Log("Flashes remmaining "+count);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i<count);    
        }
   }
}
