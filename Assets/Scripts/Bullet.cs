using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] private new Rigidbody2D rigidbody;

    private Vector2 moveDirection;
    private Action onCollideAction;

    public void Shoot(Vector2 fromPosition, Vector2 direction, Action onCollide)
    {
        transform.position = fromPosition;
        moveDirection = direction;
        onCollideAction = onCollide;

        float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(moveDirection.x, moveDirection.y, 0.0f));
        if (moveDirection.x > 0.0f) { angle = -angle; angle = angle + 360; }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        onCollideAction.Invoke();
        onCollideAction = null;
    }
}
