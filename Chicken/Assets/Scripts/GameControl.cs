using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public static Vector2 gameBound = new Vector2(18f, 9.5f);

    int gameState = 0;
    public Mom myMom;
    public GameObject myWeasel;

    public GameObject chickenPrefab;
    public GameObject eggPrefab;
    public GameObject herbPrefab;
    
    public int eggNum = 20;

    public GameObject test;
    public Animator testani;
    public static Spawner Csp;
    public static Spawner Esp;
    public static Spawner Hsp;

    public int herbNum = 0;
    public float timeLimit = 180;

    bool startCatch = false;
    int startCatchP = 25;
    int herbP = 45;
    int score = 0;
    float timelasted = 0;
    float timeold = 0;

    public AudioControl ac;
    public Animator start;
    private AnimatorStateInfo asi;
    // Use this for initialization
    void Start () {
        Csp = new Spawner(chickenPrefab, eggNum);
        Esp = new Spawner(eggPrefab, eggNum);
        Hsp = new Spawner(herbPrefab, 4);
        //this.Invoke("initEgg", 5);
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case 0:
                if (Input.anyKeyDown)
                {
                    gameState++;
                    initEgg();
                    start.SetBool("GameStart", true);
                }
                break;
            case 1:
                asi = start.GetCurrentAnimatorStateInfo(0);
                if (asi.IsName("Begin_Fade") && asi.normalizedTime >= 0.97f)
                {
                    gameState++;
                    start.gameObject.SetActive(false);

                    this.Invoke("HerbTest", Random.Range(7.0f, 20.0f));
                }
                break;
            case 2:
                if (myMom.chickList.Count != 0 && !startCatch)
                {
                    startCatch = true;
                    StartCatchTest();
                }
                #region Input
                /*
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    sp.Spawn(Random.Range(0, 18) - 9, Random.Range(0, 18) - 9);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    sp.DestroyOne();
                }*/
                //Debug.Log(Input.GetAxis("Horizontal").ToString()+" "+ Input.GetAxis("Vertical").ToString());//移动

                float haxis = Input.GetAxis("Horizontal");
                float vaxis = Input.GetAxis("Vertical");
                if (myMom.isDashing == false)
                {
                    myMom.SetSpeed(myMom.myMax * new Vector2(haxis, vaxis));
                }


                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.J))
                {
                    //SpawnChick(Random.Range(-1 * gameBound.x, gameBound.x), Random.Range(-1 * gameBound.y, gameBound.y));

                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.K))
                {
                    if (myMom.isDashing == false)
                    {
                        ac.PlayMusic(8);
                        myMom.DashSpeed();
                        myMom.isDashing = true;
                        myMom.ani.SetBool("MomRun", true);

                    }

                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.L))
                {
                    if (herbNum > 0)
                    {
                        Collider2D[] c2d = Physics2D.OverlapCircleAll(myMom.transform.position, 5);
                        foreach (Collider2D x in c2d)
                        {
                            if (x.gameObject.tag == "chick")
                            {
                                x.SendMessage("Heal");
                            }
                        }
                        herbNum--;
                    }
                }
                #endregion
                break;
            case 3:
                break;
        }
    }
    void FixedUpdate()
    {
        if (gameState == 2)
        {
            timelasted += Time.deltaTime;
            if (timelasted - timeold >= 1)
            {
                timeold = timelasted;
                FollowingScore();
            }
            if (timelasted >= timeLimit)
            {
                gameState++;
            }
        }
            
    }
    private void OnGUI()
    {
        if (gameState == 2)
        {
            GUI.Label(new Rect(20, 20, 300, 50), "得分：" + score.ToString());
            GUI.Label(new Rect(20, 45, 300, 50), "药草数量：" + herbNum.ToString());
            GUI.Label(new Rect(20, 70, 300, 50), "剩余时间：" + floatToTime(timeLimit-timelasted));
        }

    }
    void initEgg()
    {
        for (int i = 0; i < eggNum; i++)
        {
            GameObject temp = Esp.Spawn(Random.Range(-1 * GameControl.gameBound.x, GameControl.gameBound.x), Random.Range(-1 * GameControl.gameBound.y, GameControl.gameBound.y));
            temp.SendMessage("SetGC", this);
        }
    }

    public void SpawnChick(float x,float y)
    {
        GameObject temp = Csp.Spawn(x,y);
        temp.SendMessage("SetMom", myMom);
        myMom.chickList.AddLast(temp);
    }

    void StartCatchTest()
    {
        //Debug.Log("Test");
        if (!myWeasel.gameObject.activeSelf && myMom.chickList.Count != 0)
        {
            int temp = Random.Range(0, 100);
            if (temp < startCatchP)
            {
                //Debug.Log("bingo");
                myWeasel.gameObject.SetActive(true);
            }

        }
        this.Invoke("StartCatchTest", Random.Range(3.0f, 7.0f));
    }
    void HerbTest()
    {
        if (Hsp.HasSpare())
        {
            int temp = Random.Range(0, 100);
            if(temp< herbP)
            {
                Hsp.Spawn(Random.Range(-1 * GameControl.gameBound.x, GameControl.gameBound.x), Random.Range(-1 * GameControl.gameBound.y, GameControl.gameBound.y));
            }
        }
        this.Invoke("HerbTest", Random.Range(7.0f, 20.0f));
    }
    void FollowingScore()
    {
        int temp = myMom.scoreList.Count;
        score += temp;
    }
    string floatToTime(float tf)
    {
        string h = ((int)tf / 60).ToString();
        string m = ((int)tf % 60).ToString();
        return h + ":" + m;
    }
}
