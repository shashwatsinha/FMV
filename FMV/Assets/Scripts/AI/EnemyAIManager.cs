using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour {

    public Transform eyes;
    public int patrolSpeed;
    public GameObject player;
    public int chaseSpeed;
    public Transform leftBoundary;
    public Transform rightBoundary;
    public Vector3 a = new Vector3(1, 0, 0);
    [HideInInspector]public Transform chaseTarget;
    [HideInInspector]public IEnemyState currentState;
    [HideInInspector]public ChaseState chaseState;
    [HideInInspector]public AlertState alertState;
    [HideInInspector]public PatrolState patrolState;
    // Use this for initialization

    void Awake()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start ()
    {
        currentState = patrolState;
        patrolSpeed = 3;
        chaseSpeed = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentState.UpdateState();
       // if(transform.position.x > rightBoundary.transform.position.x || transform.position.x < leftBoundary.position.x)
        {
           // patrolSpeed *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter2D(other);
    }
}
