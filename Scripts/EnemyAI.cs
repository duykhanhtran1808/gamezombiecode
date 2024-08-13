using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [Tooltip("Player height: To avoid enemy to rotate X axis when it hit player")]
    //[SerializeField] float targetHeight = 1.04f; //To avoid enemy to rotate X axis when it hit player
    [SerializeField] float chaseRange = 10f; //Range of Enemy Chase
    [SerializeField] float turnSpeed = 5f;
    //[SerializeField] float groupProvokeArea = 100f; //Khoang cach ma khi 1 zombie bi ban, nhung zombie ben canh se duoi theo nguoi choi

    NavMeshAgent navMeshAgent;
    EnemyHealth health;
    Transform target;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false; //Is the enemy being provoked or not
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        health = this.GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        if (health.IsDead)
        {
            this.enabled = false;
            isProvoked = false;
            navMeshAgent.enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;

        }
        distanceToTarget = Vector3.Distance(target.position, this.transform.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            

        }
        
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        this.GetComponent<Animator>().SetBool("attack", false);
        this.GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
        //this.transform.LookAt(new Vector3(target.position.x, targetHeight, target.position.z));
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void AttackTarget()
    {
        this.GetComponent<Animator>().SetBool("attack", true);
    }

    public void ProvokeEnemy()
    {
        //if(Vector3.Distance(target.transform.position, this.transform.position) <= groupProvokeArea)
        
            this.isProvoked = true;

        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, chaseRange);
    }
}
