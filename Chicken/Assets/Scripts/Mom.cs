using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mom : MyObject
{
    public MomStatus status;
    public float myMax = 3;

    public LinkedList<GameObject> chickList = new LinkedList<GameObject>();
    // Use this for initialization
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        /*
        SetPosition(new Vector2(0, 0));
        */
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.SetSpeed(new Vector2(0, 3));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.SetSpeed(new Vector2(3, 0));

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            this.SetSpeed(new Vector2(0, -3));

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.SetSpeed(new Vector2(-3, 0));

        }
        */
        ChangeLayer();
        ChangeDirection();
        Move();
    }
}
