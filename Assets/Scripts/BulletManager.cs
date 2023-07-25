using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private List<Bullet> bulletPool = new List<Bullet>();
    private List<Bullet> activeBullets = new List<Bullet>();

    public void Shoot(Vector2 fromPosition, Vector2 direction)
    {
        Bullet bullet;
        if (bulletPool.Count == 0)
        {
            bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        }
        else
        {
            bullet = bulletPool.First();
            bulletPool.Remove(bullet);
        }
        activeBullets.Add(bullet);
        bullet.Shoot(fromPosition, direction, () => ReturnToPool(bullet));
        Debug.Log($"{fromPosition} {direction} {bullet}");
    }

    private void ReturnToPool(Bullet bullet)
    {
        activeBullets.Remove(bullet);
        bulletPool.Add(bullet);
    }
}