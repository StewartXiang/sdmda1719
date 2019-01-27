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

    private GameObject Following;
    bool isCatched = false;
    private weasel catcher;

    private int illP = 10;

    bool isInList = false;
    //private Direction old = Direction.top;

    Animator ani;

    public GameObject bubble;
    public GameObject hungry;
    public GameObject ill;
    public GameObject hungryAndIll;

    public AudioClip tweet;

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
        Following = mom.gameObject;
    }
    void OnEnable()
    {
        //illTest();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeLayer();
        if (!isCatched)
        {
            UpdateDistance();
            //SetVector(new Vector2(0, 8));
            if (momDistance > myMax)
            {
                //Debug.Log("bbb");
                Vector2 maxV = new Vector2(
                    -(this.transform.position - Following.transform.position).x / momDistance,
                    -(this.transform.position - Following.transform.position).y / momDistance
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
               // Debug.Log("ccc");
                SetSpeed(
                    new Vector2(
                        m * this.transform.position.x,
                        m * this.transform.position.y
                    ),
                    new Vector2(
                        m * Following.transform.position.x,
                        m * Following.transform.position.y
                    )
                );
            }
            ChangeWorryStatus();
            ChangeStandStatus();
            ChangeIllStatus();
            rig.velocity = this.mySpeed;
        }
        else
        {
            Vector3 addition;
            if (catcher.spr.flipX == true)
            {
                addition = weasel.FacePoseL;
            }
            else
            {
                addition = weasel.FacePoseR;
            }
            transform.position = catcher.transform.position + addition;

            if (catcher.status != WeaselStatus.get)
            {
                isCatched = false;
            }
        }
        ChangeDirection();
        //Move();
        
    }

    void ChangeWorryStatus()
    {
        if (momDistance > myRange)
        {
            if (isInList)
            {
                mom.scoreList.Remove(this.gameObject);
                isInList = false;
            }
            this.status["worry"] = true;
            ani.SetBool("ChickenWorry", true);
            this.time["worrytime"] = System.Convert.ToDouble(this.time["worrytime"])+Time.deltaTime;
            this.mySpeed = Vector2.zero;
            if (System.Convert.ToDouble(this.time["worrytime"]) > 5)
            {
                this.status["dead"] = true;
            }
        }
        else
        {
            if(!isInList)
            {
                mom.scoreList.AddLast(this.gameObject);
                isInList = true;
            }
            this.status["worry"] = false;
            ani.SetBool("ChickenWorry", false);
            this.time["worrytime"] = 0;
        }
        Debug.Log(mom.scoreList.Count);
    }
    /*
    void illTest()
    {
        Debug.Log("Test");
        if (!(bool)this.status["ill"])
        {
            int temp = Random.Range(0, 100);
            if (temp < illP)
            {
                Debug.Log("bingo");
                this.status["ill"] = true;
                ChangeBubbleState(true);
            }
        }
        this.Invoke("illTest", Random.Range(3.0f, 7.0f));
    }*/
    void ChangeIllStatus()
    {
        
        int r = Random.Range(0, 5000);
        if (r == 1)
        {
            this.status["ill"] = true;
            ChangeBubbleState(true);
        }
        if ((bool)this.status["ill"])
        {
            this.time["illtime"] = System.Convert.ToDouble(this.time["illtime"]) + Time.deltaTime;
            if (System.Convert.ToDouble(this.time["illtime"]) > 8)
            {
                this.status["dead"] = true;
            }
        }
        else
        {
            this.time["illtime"] = 0;
            //this.Invoke("illTest", Random.Range(5.0f, 9.0f));
        }
    }

    void ChangeBubbleState(bool on)
    {
        if (on == true)
        {
            bubble.SetActive(true);
            if((bool)this.status["ill"] && (bool)this.status["hungry"])
            {
                hungryAndIll.SetActive(true);
            }
            else if ((bool)this.status["ill"])
            {
                ill.SetActive(true);
            }
            else if ((bool)this.status["hungry"])
            {
                hungry.SetActive(true);
            }
            else
            {
                bubble.SetActive(false);
            }
        }
        else
        {
            bubble.SetActive(false);
            ill.SetActive(false);
            hungryAndIll.SetActive(false);
            hungry.SetActive(false);
        }
    }
    public void Heal()
    {
        this.status["ill"] = false;
        ChangeBubbleState(true);

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
        this.momDistance = (this.transform.position - Following.transform.position).magnitude;
        //Debug.Log(momDistance);
    }

    public void Catched(weasel cter)
    {
        isCatched = true;
        catcher = cter;
        AudioControl.PlayMusic(tweet);
        if (isInList)
        {
            mom.scoreList.Remove(this.gameObject);
            isInList = false;
        }
    }

}
