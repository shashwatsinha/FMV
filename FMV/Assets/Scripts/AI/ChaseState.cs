using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState
{

    private readonly EnemyAIManager enemy;


    public ChaseState(EnemyAIManager statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Debug.Log("in chase");
        Look();
        Chase();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //enemy.chaseSpeed = 0;
        //enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (other.gameObject.tag == "Boundary")
        {
            enemy.patrolSpeed *= -1;
            enemy.chaseSpeed *= -1;
            enemy.eyes.transform.position *= -1;
           // enemy.a *= -1;
        }
    }

    public void ToPatrolState()
    {
        //enemy.patrolSpeed *= -1;
        //enemy.patrolSpeed = -6;
        Debug.Log("patrol state");
        enemy.currentState = enemy.patrolState;
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void Look()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.eyes.transform.position, enemy.a, 10, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            // Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                enemy.chaseTarget = hit.transform;
                //ToChaseState();
            }

            
        }

        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        Debug.Log("stopping");
        //enemy.chaseSpeed = enemy.patrolSpeed;
        if(
           ((enemy.player.transform.position.x < enemy.leftBoundary.transform.position.x) || (enemy.player.transform.position.x > enemy.rightBoundary.transform.position.x)))
            ToPatrolState();
       // enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }
    private void Chase()
    {
        if (enemy.player.transform.position.x > enemy.transform.position.x)
        {
            enemy.chaseSpeed = 6;
        }

        else
            enemy.chaseSpeed = -6;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.chaseSpeed, 0);
    }

}
