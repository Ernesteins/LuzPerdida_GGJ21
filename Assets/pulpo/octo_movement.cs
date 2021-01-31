using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class octo_movement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timerForNewPath;
    bool inCoroutine;
    Vector3 target;
    bool validPath;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    void Update()
    {
        if (!inCoroutine)
            StartCoroutine(DoSomething());

    }
    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        Vector3 pos = new Vector3(x, 0, z);
        //Quaternion rotation = Quaternion.LookRotation(target, Vector3.up);//
        //transform.rotation = rotation;
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoroutine = true;
        yield return new WaitForSeconds(timerForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        //if (!validPath) Debug.Log("Found an invalid Path");

        while (!validPath) 
        {
            yield return new WaitForSeconds(0.2f);
            GetNewPath();
        validPath = !navMeshAgent.CalculatePath(target, path);
        }
        inCoroutine = false;
    }
        void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
}
