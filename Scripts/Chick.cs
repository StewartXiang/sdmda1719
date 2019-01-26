using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MyObject
{
    //public ChickStatus status;
    public Hashtable status = new Hashtable();
    public Hashtable time = new Hashtable();
    public Mom mom;
    public float m = 0.9f;
    public float myMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        this.status.Add("worry", false);
        this.status.Add("hungry", false);
        this.status.Add("ill", false);
        this.status.Add("dead", false);
        this.status.Add("stand", false);
        this.time.Add("worry", 0f);
        this.time.Add("hungry", 0f);
        this.time.Add("ill", 0f);
        this.time.Add("dead", 0f);
    }
    // Update is called once per frame
    void Update()
    {
        //SetVector(new Vector2(0, 8));
        if((this.transform.position - mom.transform.position).magnitude > myMax)
        {
            Vector2 maxV = new Vector2(
                -(this.transform.position - mom.transform.position).x / (this.transform.position - mom.transform.position).magnitude,
                -(this.transform.position - mom.transform.position).y / (this.transform.position - mom.transform.position).magnitude
            ) * myMax;
            SetSpeed(maxV);
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
        //Move();
        rig.velocity = this.mySpeed;
    }
    void ChangeWorryStatus()
    {
        if ((this.transform.position - mom.transform.position).magnitude > 15)
        {
            this.status["worry"] = true;
            this.time["worry"] = System.Convert.ToDouble(this.time["worry"])+Time.deltaTime;
            this.mySpeed = Vector2.zero;
        }
        else
        {
            this.status["worry"] = false;
            this.time["worry"] = 0;
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
            this.time["ill"] = (float)this.time["ill"] + Time.deltaTime;
        }
        else
        {
            this.time["ill"] = false;
        }
    }
    public void Heal()
    {
        this.status["ill"] = false;
    }

}
