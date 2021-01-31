using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girofaro : MonoBehaviour
{
    public bool TieneFoco;
    public bool girando;

    void Update()
    {
        
        if (TieneFoco && !girando)
        {
            LeanTween.rotateAround(gameObject, new Vector3(0f, 1f, 0f), 360f, 5f);            
            girando = true;
        }
        else if(TieneFoco && girando)
        {
            if (!LeanTween.isTweening(gameObject))
            {
                LeanTween.rotateAround(gameObject, new Vector3(0f, 1f, 0f), 360f, 5f);
            }
        }
        else if(!TieneFoco && girando)
        {
            LeanTween.cancel(gameObject);
            girando = false;
        }
        else
        {

        }

    }
}
