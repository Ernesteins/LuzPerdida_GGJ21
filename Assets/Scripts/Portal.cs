using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [HideInInspector]
    public bool canTeleport = true;
    [SerializeField] Portal otherPortal = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"&&canTeleport){
            otherPortal.canTeleport = false;
            other.transform.position = otherPortal.transform.position;
            // other.GetComponent<CharacterController>().enabled = false;
            // other.GetComponent<Rigidbody>().MovePosition(otherPortal.transform.position);
            // // StartCoroutine(Teleport(other));
        }
    }
    IEnumerator Teleport(Collider other){
            yield return new WaitForSeconds(1);
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            canTeleport = true;
        }
    }
}
