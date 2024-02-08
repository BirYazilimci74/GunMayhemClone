using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private float waitingTime = 0.5f;

    private BoxCollider2D characterCollider2D;
    
    public GameObject oneWayPlatformGameObject;
    
    private void Awake()
    {
        characterCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            oneWayPlatformGameObject = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            oneWayPlatformGameObject = null;
        }
    }

    public IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider2D = oneWayPlatformGameObject.gameObject.GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(characterCollider2D,platformCollider2D);

        yield return new WaitForSeconds(waitingTime);
        
        Physics2D.IgnoreCollision(characterCollider2D,platformCollider2D,false);
    }
}
