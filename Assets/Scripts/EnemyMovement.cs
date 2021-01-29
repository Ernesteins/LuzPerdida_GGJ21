using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [Header("Detect Settings")]
    [SerializeField] Transform head =null;
    [SerializeField] LayerMask playerLayer = 1;
    [SerializeField] float viewRadius = 3; 
    

    [SerializeField] float patrolRange = 30f;
    [SerializeField] float searchRange = 5f;
    [SerializeField] Transform chaseTarget = null;
    NavMeshAgent agent;
    bool isChasing = false;
    float timeToChange = 0;
    Collider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        isChasing = IsPlayerVisible();
      
        if(isChasing){
            Chase();
        }
        else{
            //Patrol(patrolRange);
        }
    }
    bool IsPlayerVisible(){
        Vector3 pos = head.position + head.forward * viewRadius;
        if(Physics.CheckSphere(pos,viewRadius,playerLayer)){
            RaycastHit hit;
            boxCollider.enabled = false;
            if (Physics.Raycast(head.position,chaseTarget.position-head.position,out hit, viewRadius*2f)){
                Debug.DrawLine(head.position,hit.point,Color.red,.3f);
                boxCollider.enabled = true;
                Debug.Log(hit.collider.gameObject.name);
                if(hit.transform == chaseTarget){
                    return true;
                }
            }
            boxCollider.enabled = true;
        }
        return false;
    }
    void Chase(){
        agent.SetDestination(chaseTarget.position);
    }
    void Patrol(float range){
        if(agent.remainingDistance < 1f){
            agent.SetDestination(GetRandomPoint(transform.position,range));
        }
    }
    Vector3 GetRandomPoint(Vector3 current, float range){
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;
        for(int i=0; i<30; i++){
            randomPoint = current + Random.insideUnitSphere * range; 
            if(NavMesh.SamplePosition(randomPoint,out hit,1f,NavMesh.AllAreas)){
                return hit.position;
            }
        }
       return Vector3.zero;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(head.position + head.forward * viewRadius, viewRadius);
    }
}
