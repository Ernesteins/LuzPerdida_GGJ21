using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] DialogEvent focosCompletos = null;
    bool canWin = false;
    private void OnEnable()
    {
        focosCompletos.AddDelegateToTrigger(CanWin);

    }
    private void OnDisable()
    {
        focosCompletos.RemoveDelegateToTrigger(CanWin);
    }
    void CanWin()
    {
        Debug.Log("YouCan win now");
        canWin = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canWin && other.tag == "Player")
        {
            GameManager.instance.Win();
        }
    }
}
