using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public GameObject target;


    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10.0f);
    }
}