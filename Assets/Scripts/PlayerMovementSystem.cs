using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementSystem : MonoBehaviour
{
    private const float speed = 2.5f;

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private Transform gunTransform;

    private Coroutine moveCoroutine;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = transform;
    }

    public void Shoot()
    {
        bulletManager.Shoot(gunTransform.position, lookDirection);
        Debug.Log("Shoot");
    }

    public void Move(Vector2 direction)
    {
        //Debug.Log(direction);
        moveDirection = direction;
        if (moveDirection != Vector2.zero)
        {
            lookDirection = direction;
        }
        if (moveCoroutine == null)
        {
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        while (moveDirection != Vector2.zero)
        {
            float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(moveDirection.x, moveDirection.y, 0.0f));
            if (moveDirection.x > 0.0f) { angle = -angle; angle = angle + 360; }
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rigidbody.velocity = moveDirection * speed;
            yield return new WaitForFixedUpdate();
        }
        rigidbody.velocity = moveDirection;
        moveCoroutine = null;
    }
}
