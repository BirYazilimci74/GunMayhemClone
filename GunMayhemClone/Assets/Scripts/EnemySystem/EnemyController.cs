using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpAmount;
    
    private GameObject oneWayPlatformGameObject;
    private BoxCollider2D enemyCollider2D;
    private EnemyRayCheck enemyRayCheck;
    private float bottomBorderY = -15;
    private Transform playerTranform;
    private Rigidbody2D enemyRb;
    private Vector3 scale;
    private bool canJump;

    public static EnemyController Instance { get; private set; }
    
    private void OnEnable()
    {
        StartCoroutine(MoveVertical());
    }
    private void Awake()
    {
        playerTranform = PlayerController.Instance.transform;
        if (Instance == null) {Instance = this;}
        
        enemyRb = GetComponent<Rigidbody2D>();
        enemyCollider2D = GetComponent<BoxCollider2D>();
        enemyRayCheck = GetComponent<EnemyRayCheck>();
    }

    private void Update()
    {
        if (!GameManager.Instance.canPlay){return;}
        Movement();
        EndingGame();
    }

    private void Movement()
    {
        MoveHorizontal();
        MoveVertical();
        
        //turnning
        float sign = playerTranform.position.x - transform.position.x > 0.2 ? -1 : 1;
        scale = new Vector3(sign, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }

    private void MoveHorizontal()
    {
        float sign = playerTranform.position.x - transform.position.x >= 0 ? -1 : 1;
        transform.Translate(sign * Vector3.right * Time.deltaTime * movementSpeed);
    }

    private IEnumerator MoveVertical()
    {
        while (true)
        {
            if (playerTranform.position.y - transform.position.y >= 2 )
            {
                Jump();
            }
            else if(playerTranform.position.y - transform.position.y is < 2 and > -2)
            {

            }
            else if (playerTranform.position.y - transform.position.y <= -2 )
            {
                Down();
            }
        
            yield return new WaitForSeconds(1f);
        }
        
    }

    private void EndingGame()
    {
        if (transform.position.y < bottomBorderY)
        {
            GameManager.Instance.EndingGame();
        }
    }
    
    private void Jump()
    {
        if (canJump && enemyRayCheck.CanUp())
        {
            enemyRb.AddForce(Vector2.up * jumpAmount,ForceMode2D.Impulse);
        }
    }

    private void Down()
    {
        if (oneWayPlatformGameObject is not null && enemyRayCheck.CanDown())
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            oneWayPlatformGameObject = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
    
    public IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider2D = oneWayPlatformGameObject.gameObject.GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(enemyCollider2D,platformCollider2D);

        yield return new WaitForSeconds(1);
        
        Physics2D.IgnoreCollision(enemyCollider2D,platformCollider2D,false);
    }
}
