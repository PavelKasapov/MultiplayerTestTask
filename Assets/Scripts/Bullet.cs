using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    private Vector2 moveDirection;
    public Action OnCollideAction { get; set; }
     
    public void Shoot(Vector2 fromPosition, Quaternion rotation)
    {
        transform.position = fromPosition;
        transform.rotation = rotation;

        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        OnCollideAction.Invoke();
    }
}
