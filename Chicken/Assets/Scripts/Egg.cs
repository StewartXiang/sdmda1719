﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MyObject
{
    private int GrowNum = 0;

    public GameControl gameController;
    public int GrowP = 5;
    public Animator ani;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
	}
    void OnEnable()
    {
        init();
    }
    // Update is called once per frame
    void Update () {
        ChangeLayer();
    }

    void init()
    {
        this.transform.rotation=Quaternion.Euler(0, 0, Random.Range(-30, 30));
        this.Invoke("GrowCheck", 3);
    }
    void GrowCheck()
    {
        int temp = Random.Range(0, 100);
        if (temp < GrowP)
        {
            GrowNum++;
            Grow();
        }
        else
        {
            this.Invoke("GrowCheck", 3);
        }
    }
    void Grow()
    {
        
        if (GrowNum == 1)
        {
            ani.SetInteger("Grow", GrowNum);
            this.Invoke("GrowCheck", 3);
        }
        if (GrowNum == 2)
        {
            ani.SetInteger("Grow", GrowNum);
            this.Invoke("GrowToChicken", 0.8f);

        }
    }
    void GrowToChicken()
    {
        gameController.SpawnChick(this.transform.position.x, this.transform.position.y);
        GameControl.Esp.Destroy(this.gameObject);
    }
    void SetGC(GameControl _gc)
    {
        gameController = _gc;
    }
}
