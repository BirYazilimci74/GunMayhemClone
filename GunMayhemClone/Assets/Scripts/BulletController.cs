using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletImpuls;
    [SerializeField] private float bulletSpeed;

    private Vector3 bulletVelocity;
    
    private void OnEnable()
    {
        float sigh = PlayerController.Instance.transform.localScale.x;
        bulletVelocity = new Vector3(sigh * bulletSpeed,0f,0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 impulsDiretion = new Vector3(1 * Math.Sign(bulletVelocity.x), 0f, 0f);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(impulsDiretion * bulletImpuls,ForceMode2D.Impulse);
        }
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
