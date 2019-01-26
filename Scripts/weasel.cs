using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weasel : MyObject
{
    public int width, height;
    public Vector2 target;
    public 
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        int r = Random.Range(0, 1);
        if (r == 0)
        {
            rig.position.Set(Random.Range(-width, width), Mathf.Pow(-1, (int)Random.Range(0, 1)));
        }
        else
        {
            rig.position.Set(Mathf.Pow(-1, (int)Random.Range(0, 1)), Random.Range(-height, height));
        }
        mySpeed = Vector2.zero;
    }
    private void OnEnable()
    {
        this.Invoke("SetSpeed()", 5000);

    }

    // Update is called once per frame
    void Update()
    {
        this.SetSpeed();
        rig.velocity = this.mySpeed;
    }
    public void IntroTarget(Vector2 target)
    {
        float xSum=0, ySum=0 ;
        foreach(GameObject c in mom.chicks)
        {
            xSum += c.transform.position.x;
            ySum += c.transform.position.y;

        }
        this.target = new Vector2(xSum/mom.chicks.Count,ySum/mom.chicks.Count);
    }
    public void SetSpeed()
    {
        SetSpeed(this.transform.position, this.target);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject o = collision.gameObject;
        switch (this.direction)
        {
            case Direction.buttom:
            case Direction.top:
                this.SetSpeed(new Vector2(this.mySpeed.x, -this.mySpeed.y));
                break;
            case Direction.right:
            case Direction.left:
                this.SetSpeed(new Vector2(-this.mySpeed.x, this.mySpeed.y));
                break;
        }
        if (collision.gameObject.tag == "chick")
        {
            collision.gameObject.transform.parent = this.gameObject.transform;

        }
    }

}
