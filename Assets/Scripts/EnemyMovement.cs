using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [Header("Detect Settings")]
    [SerializeField] Transform head =null;
    [SerializeField] float maxViewRadius = 3; 
    [SerializeField,Range(-2,2)] float yOffset = 3; 
    [SerializeField] DetectionSphere[] detectionSpheres = null;

    [SerializeField] LayerMask playerLayer = 1;
    [SerializeField] Transform chaseTarget = null;
    
    [Header("Move settings")]
    [SerializeField] float chasingTime = 3; 
    [SerializeField] float searchingTime = 3; 
    [SerializeField] float searchRange = 5f;
    [SerializeField] float patrolRange = 30f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip inicialClip = null;
    [SerializeField] AudioClip partullaClip = null;
    [SerializeField] AudioClip chaseClip = null;
    [SerializeField] AudioClip searchClip = null;
    [SerializeField] AudioSource audioSource = null;

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
        audioSource.PlayOneShot(inicialClip,1);
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
            PlaySound(searchClip);
            searchTime -= Time.deltaTime;
            if(searchTime <= 0){
                Debug.Log("StartPatrol");
            }
        }
        else{
            ///Patrol
            Patrol(patrolRange);
            PlaySound(partullaClip);
        }
    }
    void PlaySound(AudioClip clip, bool loop = true){
        if(clip!=null &&  clip != audioSource.clip){
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
    public void Stunning(){
        stunnedTime = 10f;
    }
    bool IsPlayerVisible(){
        if(DetectTarget()){
            RaycastHit hit;
            boxCollider.enabled = false;
            if (Physics.Raycast(head.position,chaseTarget.position-(head.position+Vector3.up*yOffset),out hit, maxViewRadius)){
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
    bool DetectTarget(){
        foreach(DetectionSphere ds in detectionSpheres){
            if(ds.Detect(chaseTarget,playerLayer)){
                return true;
            }
        }
        return false;
    }
    void Chase(){
        agent.SetDestination(chaseTarget.position);
        PlaySound(chaseClip);
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
}
