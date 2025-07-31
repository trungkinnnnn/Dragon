using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBall : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * speed;
        Debug.Log(moveDirection);
        Destroy(gameObject, 5f);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;

        Vector3 scale = transform.localScale;
        scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }


}
