using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;
    public float damage = 20f;
    public float health = 100f;
    public GameManager gameManager;

    public void Hit(float damage)
    {
        health -= damage;
        if(health == 0)
        {
            gameManager.enemiesAlive--;
            //destroy zombie
            enemyAnimator.SetTrigger("isDead");
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 2.6f);  
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;
        if (GetComponent<NavMeshAgent>().velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 4.65f)
        {
            enemyAnimator.SetTrigger("isAttacking");
            //StopEnemy();
        }
        else if(distance < 2.65f)
        {
            StopEnemy();
        }
        
    }

    private void StopEnemy()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void GoToTarget()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject == player)
        {
            
            player.GetComponent<PlayerManager>().Hit(damage);
        }
    }
}
