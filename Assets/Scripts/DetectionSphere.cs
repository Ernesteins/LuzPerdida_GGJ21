using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    [SerializeField] float viewRadius = 3; 

    public bool Detect(Transform player,LayerMask playerLayer){
        return Physics.CheckSphere(transform.position,viewRadius,playerLayer);
    }

     private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
