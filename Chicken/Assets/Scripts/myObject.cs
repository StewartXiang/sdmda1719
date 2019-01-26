using UnityEngine;
using System.Collections;

public class MyObject : MonoBehaviour
{
    public Direction direction;
    public Vector2 mySpeed;
    public Rigidbody2D rig;
    public SpriteRenderer spr;
    public void SetPosition(Vector2 v)
    {
        this.transform.position = new Vector3(v.x, v.y, 0);
    }
    public void SetSpeed(Vector2 v)
    {
        mySpeed = v;
    }
    public void SetSpeed(Vector2 myPos, Vector2 aimPos)
    {
        mySpeed = aimPos - myPos;
    }
    public void ChangeDirection()
    {
        if (this.mySpeed.x > System.Math.Abs(this.mySpeed.y))
        {
            spr.flipX = false;
            this.direction = Direction.right;
        }
        else if (-this.mySpeed.x > System.Math.Abs(this.mySpeed.y))
        {
            spr.flipX = true;
            this.direction = Direction.left;
        }
        else if (this.mySpeed.y > System.Math.Abs(this.mySpeed.x))
        {
            this.direction = Direction.top;
        }
        else if (-this.mySpeed.y > System.Math.Abs(this.mySpeed.x))
        {
            this.direction = Direction.buttom;
        }
        //Debug.Log(direction);
    }
    public void ChangeLayer()
    {
        float h = this.transform.position.y + GameControl.gameBound.y;
        int l = 100 - System.Convert.ToInt32(100 * (h / (GameControl.gameBound.y * 2)));
        spr.sortingOrder = l + 1;
    }

    public void Move()
    {
        this.transform.position += new Vector3(this.mySpeed.x * Time.deltaTime, this.mySpeed.y * Time.deltaTime, 0);

    }
}
