using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState
{

    private readonly EnemyAIManager enemy;
    public int speed = 10;

    public ChaseState(EnemyAIManager statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
        Debug.Log("state:chase");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
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

        if (enemy.player.GetComponent<Movement>().area == Area.DARK)
        {
            Debug.Log("Going in patrol as player in dark");
            ToPatrolState();
        }

        RaycastHit2D hit = Physics2D.Raycast(enemy.eyes.transform.position, enemy.a, 10, 1 << LayerMask.NameToLayer("Player"));
        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                enemy.chaseTarget = hit.transform;
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
        if(
           ((enemy.player.transform.position.x < enemy.leftBoundary.transform.position.x) || (enemy.player.transform.position.x > enemy.rightBoundary.transform.position.x)))
            ToPatrolState();


    }
    private void Chase()
    {
        if (enemy.player.transform.position.x > enemy.transform.position.x)
        {
            enemy.chaseSpeed = speed;
        }

        else
            enemy.chaseSpeed = -speed;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.chaseSpeed, 0);
    }

}
