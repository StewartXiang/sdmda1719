using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public static Vector2 cameraBound = new Vector2(10.2f, 5.6f);
    public GameObject mom;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float tempx = this.gameObject.transform.position.x, tempy = this.gameObject.transform.position.y;
        if(Mathf.Abs(mom.transform.position.x) < cameraBound.x)
        {
            tempx = mom.transform.position.x;
        }
		if(Mathf.Abs(mom.transform.position.y) < cameraBound.y)
        {
            tempy= mom.transform.position.y;
        }
        this.gameObject.transform.position = new Vector3(tempx, tempy, -10);
    }
}
