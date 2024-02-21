using UnityEngine;

public class EnemyRayCheck : MonoBehaviour
{
    [SerializeField] private Transform downRay;
    [SerializeField] private Transform upRay;

    public bool CanUp()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(upRay.position,Vector2.up ,1.5f);
        
        if (hitUp.collider is not null && hitUp.collider.gameObject.CompareTag("Ground"))
        {
            Debug.DrawRay(upRay.position, Vector2.up * hitUp.distance, Color.black);
            return true;
            
        }else {return false;}
    }

    public bool CanDown()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(downRay.position,-Vector2.up,100);
        
        if (hitDown.collider is not null && hitDown.collider.gameObject.CompareTag("Ground"))
        {
            Debug.DrawRay(downRay.position,-Vector2.up * hitDown.distance,Color.black);
            return true;
            
        }else {return false; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.canPlay)
        {
            AnimationController.Instance.AttackAnimation();
        }
    }
    
}
