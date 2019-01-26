using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weasel : MyObject
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        mySpeed = Vector2.zero;
    }
    public void IntroSpeed(Vector2 target)
    {
        this.mySpeed = (target - new Vector2(this.transform.position.x, this.transform.position.y));
    }
}
