using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    public void Shoot(float facingDirection)
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);

        if (facingDirection < 0)
            newBullet.GetComponent<Bullet>().SetNegativeSpeed();
        
        Destroy(newBullet, .7f);
    }
}
