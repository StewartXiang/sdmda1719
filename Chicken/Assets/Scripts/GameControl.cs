using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public static Vector2 gameBound = new Vector2(18f, 9.5f);

    int gameState = 0;
    public Mom myMom;

    public GameObject chickenPrefab;
    public GameObject eggPrefab;

    public int eggNum = 20;

    public GameObject test;
    public Animator testani;
    public static Spawner Csp;
    public static Spawner Esp;
    // Use this for initialization
    void Start () {
        Csp = new Spawner(chickenPrefab, eggNum);
        Esp = new Spawner(eggPrefab, eggNum);
        this.Invoke("initEgg", 5);
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
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
        myMom.SetSpeed(myMom.myMax * new Vector2(haxis, vaxis));


        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.J))
        {
            SpawnChick(Random.Range(-1 * gameBound.x, gameBound.x), Random.Range(-1 * gameBound.y, gameBound.y));
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.K)) { }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.L)) { }
        #endregion
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
}
