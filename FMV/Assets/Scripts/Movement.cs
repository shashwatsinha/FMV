using UnityEngine;
using System.Collections;

public enum Movable { RUN, SING};
public enum Area { LIGHT, DARK };

public class Movement : MonoBehaviour {

    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    private bool grounded;
    float singMeter;
    float singTimer = 0;
    float TimeToSing = 5.0f;
    float TimeSinging = 5.0f;
    private Rigidbody2D rb2d;
    public Movable move;
    public Area area;
    // Use this for initialization
    void Start()
    {
        grounded = true;
        singMeter = 0;
        rb2d = GetComponent<Rigidbody2D>();
        move = Movable.RUN;
        singTimer = TimeSinging;
    }

    // Update is called once per frame
    void Update()
    {
        switch(move)
        {
            case Movable.RUN:
                PollMovement();
                switch (area)
                {
                    case Area.DARK:
                        singMeter = singMeter + 0.01f;
                        break;

                    case Area.LIGHT:
                        singMeter = singMeter - 0.01f;
                        break;
                }
                break;

            case Movable.SING:
                singTimer -= Time.deltaTime;
                Sing();
                break;
        }

        
        
        if(singMeter<0.0f)
        {
            singMeter = 0.0f;
        }

        if(singMeter > TimeToSing)
        {
            singMeter = 0.0f;
            singTimer = TimeSinging;
            move = Movable.SING;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), singMeter.ToString());
        GUI.Label(new Rect(80, 10, 100, 20), singTimer.ToString());
    }

    void PollMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (grounded)
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            else
                transform.Translate(new Vector3(speed / 5 * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (grounded)
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            else
                transform.Translate(new Vector3(-speed / 5 * Time.deltaTime, 0, 0));
        }

        if ((Input.GetKeyDown(KeyCode.W)) && grounded == true)
        {
            rb2d.velocity = new Vector2(0f, jumpSpeed);
            grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }

       if(col.gameObject.tag=="Enemy")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Light")
        {
            area = Area.LIGHT;
        }

        if (col.gameObject.tag == "Dark")
        {
            area = Area.DARK;
        }

    
    }

    void OnTriggerExit2D(Collider2D col)
    {
      //  if (col.gameObject.tag == "Light")
        {
       //     area = Area.DARK;
        }

        if (col.gameObject.tag == "Dark")
        {
            area = Area.LIGHT;
        }

   
    }

   

    void Sing()
    {
        if(singTimer<0.0f)
        {
            singTimer = 0.0f;
            move = Movable.RUN;
        }
    }

    }







