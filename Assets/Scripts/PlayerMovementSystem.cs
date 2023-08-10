using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementSystem : NetworkBehaviour
{
    private const float speed = 2.5f;

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spriteTransform;
    public BulletManager bulletManager;

    private Coroutine moveCoroutine;
    private Vector2 moveDirection;

    [ServerRpc]
    public void ShootServerRpc()
    {
        bulletManager.Shoot(gunTransform.position, spriteTransform.rotation);
    }

    public void Move(Vector2 direction)
    {
        Debug.Log("go");
        moveDirection = direction;
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveRoutine());
        }
    }

   /* public void Move(Vector2 direction)
    {
        Debug.Log("go");
        moveDirection = direction;
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveRoutine());
        }
    }*/

    private IEnumerator MoveRoutine()
    {
        while (moveDirection != Vector2.zero)
        {
            float angle = Vector3.Angle(Vector3.up, new Vector3(moveDirection.x, moveDirection.y, 0.0f));
            if (moveDirection.x > 0.0f) { angle = -angle; angle = angle + 360; }
            spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            playerTransform.Translate(Vector2.up * speed * Time.fixedDeltaTime, spriteTransform);
            /*rigidbody.velocity = moveDirection * speed;*/
            yield return new WaitForFixedUpdate();
        }
        rigidbody.velocity = moveDirection;
        moveCoroutine = null;
    }
}
