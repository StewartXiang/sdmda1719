using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weasel : MyObject
{
    public float width = GameControl.gameBound.x, height = GameControl.gameBound.y;
    public static Vector3 FacePoseL = new Vector3(-1.3f, 0.4f, 0);
    public static Vector3 FacePoseR = new Vector3(1.0f, 0.4f, 0);
    public Vector2 target;
    public int tempn;
    public Mom mom;
    public GameObject getChick;
    public WeaselStatus status = WeaselStatus.prepare;

    public AudioClip bark;
    // Start is called before the first frame update
    void Start()
    {
        //rig = GetComponent<Rigidbody2D>();
        //spr = GetComponent<SpriteRenderer>();
        
    }
    private void OnEnable()
    {
        AudioControl.PlayMusic(bark);
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            this.transform.position = new Vector3(Random.Range(-width, width), height * Mathf.Pow(-1, Random.Range(0, 2)));
        }
        else
        {
            this.transform.position = new Vector3(width * Mathf.Pow(-1, Random.Range(0, 2)), Random.Range(-height, height));
        }
        mySpeed = Vector2.zero;
        IntroTarget();
        SetSpeed();
        //this.Invoke("SetSpeed", 5);
        Debug.Log(this.status.ToString());
        this.status = WeaselStatus.prepare;
        int temp = mom.chickList.Count;
        int n = Random.Range(0, temp);
        //Debug.Log("55555");
    }

    // Update is called once per frame
    void Update()
    {
        IntroTarget();
        if (this.status==WeaselStatus.prepare)
        {
            this.SetSpeed();
        }
        rig.velocity = this.mySpeed;
        //Debug.Log(mySpeed);
        if (CheckOut())
        {
            this.gameObject.SetActive(false);
        }
        ChangeLayer();
        ChangeDirection();
        //Move();
    }
    public void IntroTarget()
    {
        //float xSum=0, ySum=0 ;
        /*
        foreach(GameObject c in mom.chickList)
        {
            xSum += c.transform.position.x;
            ySum += c.transform.position.y;

        }*/
        //Debug.Log(xSum.ToString() + " " +  ySum.ToString());
        //this.target = new Vector2(xSum/mom.chickList.Count,ySum/mom.chickList.Count); 
        LinkedListNode<GameObject> g = mom.chickList.First;
        for (int i=0; i<this.tempn; i++)
        {
            g = g.Next;
        }
        this.target = new Vector2(g.Value.transform.position.x, g.Value.transform.position.y);
    }
    public void SetSpeed()
    {
        SetSpeed(new Vector2(this.transform.position.x, this.transform.position.y), this.target);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject o = collision.gameObject;
        //Debug.Log(233);
        if(this.status == WeaselStatus.prepare || this.status == WeaselStatus.lose)
        {
            if(this.status == WeaselStatus.prepare)
            {
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

            }
            if (o.tag == "chick")
            {
                this.status = WeaselStatus.get;
                getChick = o;
                /*o.transform.parent = this.gameObject.transform;
                o.transform.localScale = new Vector3(1, 1, 1);
                o.transform.localPosition = new Vector3(
                    this.mySpeed.x / Mathf.Abs(this.mySpeed.x) * 1.0f,
                    0.4f,
                    0
                );*/
                o.SendMessage("Catched", this);
            }
        }
        if (o.tag == "mom")
        {

            this.status = WeaselStatus.lose;
            this.getChick = null;
            AudioControl.PlayMusic(bark);
        }
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        GameObject o = collision.gameObject;
        Debug.Log("6666666");
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
    }*/
    public bool CheckOut()
    {
        if (
            Mathf.Abs(this.transform.position.x) > GameControl.gameBound.x+2 || 
            Mathf.Abs(this.transform.position.y) > GameControl.gameBound.y+2
        )
        {
            if (getChick)
            {
                mom.chickList.Remove(getChick);
                GameControl.Esp.Destroy(getChick);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

}
