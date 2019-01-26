using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mom : MyObject
{
    public MomStatus status;
    public float myMax = 3;
    public Animator ani;

    public bool isDashing = false;
    public float maxDashLength = 10.5f;
    public float dashSpeed = 40;

    public LinkedList<GameObject> chickList = new LinkedList<GameObject>();

    private float realDashLength = 0;
    private Vector3 oldDashPos = new Vector3(999, 999, 999);

    // Use this for initialization
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
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
        if (isDashing)
        {
            if (oldDashPos.z != 999)
            {
                realDashLength += (transform.position - oldDashPos).magnitude;
            }
            oldDashPos = this.transform.position;
            if (realDashLength > maxDashLength)
            {
                oldDashPos = new Vector3(999, 999, 999);
                realDashLength = 0;
                isDashing = false;
                ani.SetBool("MomRun", false);
            }
        }
        else
        {
            ChangeStandStatus();
        }
        Move();
    }
    public void ChangeStandStatus()
    {
        bool isStand;
        if (this.mySpeed.magnitude <= 0.2)
        {
            isStand = true;
        }
        else
        {
            isStand = false;
        }
        //Debug.Log(this.mySpeed.magnitude);
        ani.SetBool("MomWalk", !isStand);
    }
    public void MomRun()
    {
        ani.SetBool("MomRun", true);
    }

    public void DashSpeed()
    {
        switch (direction)
        {
            case Direction.left:
                SetSpeed(Vector2.left * dashSpeed);
                break;
            case Direction.buttom:
                SetSpeed(Vector2.down * dashSpeed);
                break;
            case Direction.right:
                SetSpeed(Vector2.right * dashSpeed);
                break;
            case Direction.top:
                SetSpeed(Vector2.up * dashSpeed);
                break;
        }
    }

}
