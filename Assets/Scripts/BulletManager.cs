using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private List<Bullet> bulletPool = new List<Bullet>();
    private List<Bullet> activeBullets = new List<Bullet>();

    public void Shoot(Vector2 fromPosition, Quaternion rotation)
    {
        Bullet bullet;
        if (bulletPool.Count == 0)
        {
            bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            bullet.OnCollideAction = () => ReturnToPool(bullet);
        }
        else
        {
            bullet = bulletPool.First();
            bulletPool.Remove(bullet);
        }
        activeBullets.Add(bullet);
        bullet.Shoot(fromPosition, rotation);
    }

    private void ReturnToPool(Bullet bullet)
    {
        activeBullets.Remove(bullet);
        bulletPool.Add(bullet);
    }
}
