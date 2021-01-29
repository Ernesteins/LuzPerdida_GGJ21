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
    [SerializeField] Transform chaseTarget = null;
    
    [Header("Move settings")]
    [SerializeField] float chasingTime = 3; 
    [SerializeField] float searchingTime = 3; 
    [SerializeField] float searchRange = 5f;
    [SerializeField] float patrolRange = 30f;

    NavMeshAgent agent;
    bool isTargetVisible = false;
    bool wasTargetVisible = false;
    float stunnedTime = 0;
    float searchTime = 0;
    float chaseTime = 0;
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
        isTargetVisible = IsPlayerVisible();
        if(isTargetVisible&&!wasTargetVisible){
            chaseTime = chasingTime;
            Debug.Log("Start Chasing");
        }
        wasTargetVisible = isTargetVisible;

        if(stunnedTime>0){
            agent.isStopped = true;
            stunnedTime -= Time.deltaTime;
            chaseTime = 0;
            return;
        }
        else agent.isStopped = false;

        if(chaseTime > 0){
            Chase();
            chaseTime -= Time.deltaTime;
            if(chaseTime <= 0){
                Debug.Log("Start serching");
                searchTime = searchingTime;
            }
        }
        else if(searchTime > 0){
            //search
            Patrol(searchRange);
            searchTime -= Time.deltaTime;
        }
        else{
            Debug.Log("Patrol...");
            ///Patrol
            Patrol(patrolRange);
        }
    }

    public void Stunning(){
        stunnedTime = 10f;
    }
    bool IsPlayerVisible(){
        Vector3 pos = head.position + head.forward * viewRadius;
        if(Physics.CheckSphere(pos,viewRadius,playerLayer)){
            RaycastHit hit;
            boxCollider.enabled = false;
            if (Physics.Raycast(head.position,chaseTarget.position-head.position,out hit, viewRadius*2f)){
                Debug.DrawLine(head.position,hit.point,Color.red,.3f);
                boxCollider.enabled = true;
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
