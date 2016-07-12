using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public GameObject target;
    float downlimit;
    float downPos;
    // Use this for initialization
    void Start()
    {
        downlimit = 2.7f;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        downPos = target.transform.position.y;
        if (downPos < downlimit)
            downPos = downlimit;
        transform.position = new Vector3(target.transform.position.x, downPos, -10.0f);
    }
}