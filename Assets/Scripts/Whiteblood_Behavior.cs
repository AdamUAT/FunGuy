using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteblood_Behavior : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D MyRig;
    CircleCollider2D MyCirc;


    // Start is called before the first frame update
    void Start()
    {

        MyRig = GetComponent<Rigidbody2D>();
        MyCirc = GetComponent<CircleCollider2D>();
    }


    void Update()
    {



        if (IsfacingR())
        {
            MyRig.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            MyRig.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

   
    private bool IsfacingR()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(MyRig.velocity.x)), transform.localScale.y);
    }

}
