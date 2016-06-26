using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

    private readonly EnemyAIManager enemy;
    
    public PatrolState(EnemyAIManager statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Debug.Log("Patrolling");
        Look();
        Patrol();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            ToAlertState();

        if(other.gameObject.tag=="Boundary")
        {
            enemy.patrolSpeed *= -1;
            enemy.chaseSpeed *= -1;
            enemy.a *= -1;
            enemy.eyes.transform.localPosition *= -1;
           // enemy.eyes.transform.position *= -1;
        }
    }

    public void ToPatrolState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToAlertState()
    {
      //  enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.chaseSpeed = enemy.patrolSpeed*2;
        enemy.currentState = enemy.chaseState;
    }

    private void Look()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.eyes.transform.position, enemy.a,10,1<<LayerMask.NameToLayer("Player"));
        if (hit)
        {
           // Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                enemy.chaseTarget = hit.transform;
                ToChaseState();
            }
        }
      
    }

    void Patrol()
    {
        //rb.velocity = new Vector2(5, 0);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.patrolSpeed, 0);
    }

}
