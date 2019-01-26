using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weasel : MyObject
{
    public float width = GameControl.gameBound.x, height = GameControl.gameBound.y;
    public Vector2 target;
    public Mom mom;
    public WeaselStatus status = WeaselStatus.prepare;
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
        if (this.status==WeaselStatus.prepare)
        {
            this.SetSpeed();
        }
        rig.velocity = this.mySpeed;
        if (CheckOut())
        {
            Destroy(this);
        }
    }
    public void IntroTarget(Vector2 target)
    {
        float xSum=0, ySum=0 ;
        foreach(GameObject c in mom.chickList)
        {
            xSum += c.transform.position.x;
            ySum += c.transform.position.y;

        }
        this.target = new Vector2(xSum/mom.chickList.Count,ySum/mom.chickList.Count);
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
        if (o.tag == "chick")
        {
            this.status = WeaselStatus.get;
            o.transform.parent = this.gameObject.transform;
            o.transform.localPosition = new Vector3(
                this.mySpeed.x/Mathf.Abs(this.mySpeed.x)*1.0f,
                0.4f,
                0
            );
        }
        else if(o.tag == "mom")
        {
            this.status = WeaselStatus.lose;
        }
    }
    public bool CheckOut()
    {
        if (
            Mathf.Abs(this.transform.position.x) > GameControl.gameBound.x-2 | 
            Mathf.Abs(this.transform.position.x) > GameControl.gameBound.y-2
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
