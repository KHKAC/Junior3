using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    Animator playerAnim;
    readonly int JUMP_TRIG = Animator.StringToHash("Jump_trig");
    readonly int DEATH_BOOL = Animator.StringToHash("Death_b");
    readonly int DEATH_TYPE = Animator.StringToHash("DeathType_int");
    public GameObject gameOverPanel;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, gravityModifier, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger(JUMP_TRIG);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //isOnGround = true;
        if (collision.gameObject.CompareTag("GROUND"))
        {
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("OBSTACLE"))
        {
            isGameOver = true;
            playerAnim.SetBool(DEATH_BOOL, true);
            playerAnim.SetInteger(DEATH_TYPE, 2);
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over!");
        }
    }
}
