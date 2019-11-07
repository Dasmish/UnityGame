using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    Transform Transform;
    float EnemyWidth;
    public float EnemySpeed = 0.5f;
    public LayerMask EnemyMask;

    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Transform = this.transform;
        EnemyWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 LineCast = Transform.position + Transform.right * EnemyWidth;
        Debug.DrawLine(LineCast, LineCast + Vector2.down);

        onGround = Physics2D.Linecast(LineCast, LineCast + Vector2.down, EnemyMask);

        if (!onGround)
        {
            Vector3 currentRotate = Transform.eulerAngles;
            currentRotate.y += 180;
            Transform.eulerAngles = currentRotate;
        }

        Vector2 Velocity = Rigidbody.velocity;
        Velocity.x = Transform.right.x * EnemySpeed;
        Rigidbody.velocity = Velocity;
        //Rigidbody.velocity = new Vector2(EnemySpeed, Rigidbody.velocity.y);
    }
}
