using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MyObject
{
    //public ChickStatus status;
    public Hashtable status = new Hashtable();
    public Hashtable time = new Hashtable();
    public Mom mom;
    public float m = 1.1f;
    public float myMax = 5f;
    public float myMin = 2f;
    public float myRange = 5.0f;
    public float momDistance;


    private Direction old = Direction.top;

    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        this.status.Add("worry", false);
        this.status.Add("hungry", false);
        this.status.Add("ill", false);
        this.status.Add("dead", false);
        this.status.Add("stand", false);
        this.time.Add("worrytime", 0f);
        this.time.Add("hungrytime", 0f);
        this.time.Add("illtime", 0f);
        this.time.Add("deadtime", 0f);
    }

    void SetMom(Mom _mom)
    {
        mom = _mom;
    }
    void OnEnable()
    {
    }
    // Update is called once per frame
    void Update()
    {
        ChangeLayer();
        UpdateDistance();
        //SetVector(new Vector2(0, 8));
        if (momDistance > myMax)
        {
            //Debug.Log("bbb");
            Vector2 maxV = new Vector2(
                -(this.transform.position - mom.transform.position).x / momDistance,
                -(this.transform.position - mom.transform.position).y / momDistance
            ) * m;
            SetSpeed(maxV);
        }
        else if (momDistance < myMin)
        {
            //Debug.Log("aaa");
            SetSpeed(Vector2.zero);
        }
        else
        {
            SetSpeed(
                new Vector2(
                    m*this.transform.position.x, 
                    m*this.transform.position.y
                ), 
                new Vector2(
                    m*mom.transform.position.x, 
                    m*mom.transform.position.y
                )
            );
        }
        ChangeDirection();
        ChangeWorryStatus();
        ChangeStandStatus();
        //Move();
        rig.velocity = this.mySpeed;
    }

    void ChangeWorryStatus()
    {
        if (momDistance > myRange)
        {
            this.status["worry"] = true;
            this.time["worrytime"] = System.Convert.ToDouble(this.time["worrytime"])+Time.deltaTime;
            this.mySpeed = Vector2.zero;
            if ((float)this.time["illtime"] > 5)
            {
                this.status["dead"] = true;
            }
        }
        else
        {
            this.status["worry"] = false;
            this.time["worrytime"] = 0;
        }
    }
    void ChangeIllStatus()
    {
        int r = Random.Range(0, 500);
        if (r == 1)
        {
            this.status["ill"] = true;
        }
        if ((bool)this.status["ill"])
        {
            this.time["illtime"] = (float)this.time["illtime"] + Time.deltaTime;
            if ((float)this.time["illtime"] > 8)
            {
                this.status["dead"] = true;
            }
        }
        else
        {
            this.time["illtime"] = false;
        }
    }
    public void Heal()
    {
        this.status["ill"] = false;
    }
    public void ChangeStandStatus()
    {
        if (this.mySpeed.magnitude <= 0.2)
        {
            this.status["stand"] = true;
        }
        else
        {
            this.status["stand"] = false;
        }
        //Debug.Log(this.mySpeed.magnitude);
        ani.SetBool("ChickenWalk", !System.Convert.ToBoolean(this.status["stand"]));
    }
    void UpdateDistance()
    {
        this.momDistance = (this.transform.position - mom.transform.position).magnitude;
        //Debug.Log(momDistance);
    }

}
