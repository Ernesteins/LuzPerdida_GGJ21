using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] AudioClip attackClip = null;
    [SerializeField] AudioSource audioSource = null;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            GameManager.instance.GameOver();
            audioSource.PlayOneShot(attackClip,1);
        }
    }
}
