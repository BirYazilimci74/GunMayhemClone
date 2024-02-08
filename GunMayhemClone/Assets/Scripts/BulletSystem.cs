using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float returnDelay;
    [SerializeField] private List<SpriteRenderer> gunRenders;

    private Queue<GameObject> bulletPool;
    private const int RELOAD_TIME = 5;
    private const int POOL_SIZE = 5;
    
    [HideInInspector] public float lastShootTime;
    [HideInInspector] public bool gunLocked;

    private void Start()
    {
        bulletPool = new Queue<GameObject>();
        InitializePool();
    }
    private void Update() 
    {
        UnlockGun();
    }
    
    private void InitializePool()
    {
        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        return null;
    }

    private void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void Shoot()
    {
        if (!gunLocked)
        {
            GameObject bullet = GetBulletFromPool();
            bullet.transform.position = shootingPosition.position;
            StartCoroutine(ReturnBulletWithDelay(bullet, returnDelay));
            gunRenders.ForEach(gunRender => gunRender.color = Color.white);
        }
        else
        {
            gunRenders.ForEach(gunRender => gunRender.color = Color.red);
        }
    }

    private void UnlockGun()
    {
        if (Time.time - lastShootTime > RELOAD_TIME)
        {
            gunLocked = false;
            lastShootTime = 0;
            gunRenders.ForEach(gunRender => gunRender.color = Color.white);
        }
    }
    
    
    private IEnumerator ReturnBulletWithDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnBulletToPool(bullet);
    }
}

