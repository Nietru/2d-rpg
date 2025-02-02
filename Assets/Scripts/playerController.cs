using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Bouncing between new scenes when we restart the game.
using TMPro;    // For all of our Text and Buttons

public class playerComponent : MonoBehaviour
{
    // Variables:
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator playerAnim;

    public int direction;

    public float attackingCoolDown; // So that the player has to click and can't just hold down the button to attack.

    public GameObject sword1;
    public GameObject bow1;
    public int arrowCount; // So that our arrows can run out.
    public GameObject arrowPrefab;
    public int weaponInUse;

    public bool hurting; // Animation for when the player is "hurt" by an enemy. Bool so we can asses if the player is hurt or not.
    public GameObject playerSprite;
    public bool stillInEnemyRange; // To calculate if player is still in enemy range, so we know if theyre getting hurt by the attack.

    public int playerHealth; // So that we can keep track of the player's health.
    public Animator gameOver; // For when we run out of hearts: animation for game over.
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public TextMeshProUGUI inGameCoinText;
    public int coinCount;

    public TextMeshProUGUI inGameHealthPotionText;
    public int healthPotionCount;

    public TextMeshProUGUI inGameArrowText;

    public GameObject shopButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 3; // Starting health of the player.
    }

    // Update is called once per frame
    void Update()
    {
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //attacking cool down if
        if (attackingCoolDown <= 0 && playerHealth > 0)  // If the player is not attacking and has health.
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;  // So that the player doesn't rotate when moving.


            //movement
            moveInput.x = Input.GetAxisRaw("Horizontal"); // calculates the movement of the player on the x axis. {A & D keys}
            moveInput.y = Input.GetAxisRaw("Vertical"); // calculates the movement of the player on the y axis. {W & S keys}
            moveInput.Normalize(); // So that the player doesn't move faster when moving diagonally.

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y)) 
            {
                moveInput.y = 0;
                rb2d.velocity = moveInput * moveSpeed;
            }
            else
            {
                moveInput.x = 0;
                rb2d.velocity = moveInput * moveSpeed;
            }

            //walking anims
            if (moveInput.y < 0)
            {
                playerAnim.Play("playerWalkD");
                direction = 0;
            }
            else if (moveInput.x > 0)
            {
                playerAnim.Play("playerWalkR");
                direction = 1;
            }
            else if (moveInput.x < 0)
            {
                playerAnim.Play("playerWalkL");
                direction = 2;
            }
            else if (moveInput.y > 0)
            {
                playerAnim.Play("playerWalkU");
                direction = 3;
            }

            //idle anims
            if (moveInput.y == 0 && moveInput.x == 0) // if player is not moving in any direction:
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerIdleD");
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerIdleR");
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerIdleL");
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerIdleU");
                }
            }

            //attacking
            if (Input.GetKeyDown(KeyCode.Space)) // Calculates if the attack key is pressed.
            {
                if (direction == 0) // we have 4 different directions for the player to attack in.: 0, 1, 2, 3
                {
                    playerAnim.Play("playerAttackD");
                    attackingCoolDown = 0.4f;
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerAttackR");
                    attackingCoolDown = 0.4f;
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerAttackL");
                    attackingCoolDown = 0.4f;
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerAttackU");
                    attackingCoolDown = 0.4f;
                }

                //shooting arrows
                if (arrowCount > 0 && weaponInUse == 1) // if statement only runs if the player has arrows.
                {
                    PlayerPrefs.SetInt("ArrowCount", arrowCount - 1); // PlayerPrefs is a way to save data between scenes, in Unity. (In this case, the arrow count.)
                    if (direction == 0) // down direction
                    {// Quaternion.Euler is used to rotate the arrow in the direction the player is facing.(may need to adjust the values, depending on the sprite)
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 270))); 
                    }
                    if (direction == 1) // right direction
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    if (direction == 2) // left direction
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
                    if (direction == 3) // up direction
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                }
            }
        }
        else // if the player is attacking or has no health:
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //changing weapons
        if (Input.GetKey(KeyCode.Alpha1)) // Alpha1 is the number 1 on the keyboard. This will change the weapon the player is using.
        {
            sword1.SetActive(true);
            bow1.SetActive(false);
            weaponInUse = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            sword1.SetActive(false);
            bow1.SetActive(true);
            weaponInUse = 1; // we need this to be able to shoot arrows. Line 146 ^ above.
        }
        if (Input.GetKeyDown(KeyCode.H)) // Using health potion, if the player has any, use H key.
        {
            if (healthPotionCount > 0 && playerHealth < 3) // Can use a Health Potion only if the player has health potions AND is not at full health.
            { 
                playerHealth++; // ++ adds 1 to the player's health.
                PlayerPrefs.SetInt("HealthPotionCount", healthPotionCount - 1); // SetInt to save the new health potion count if one is used.
            }
        }

        //attacking cool down timer
        if (attackingCoolDown > 0)
        {
            attackingCoolDown -= Time.deltaTime; // subtracts time from the attackingCoolDown variable (about one second)
        }

        //loosing health
        if (playerHealth == 3) // 3 hearts
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerHealth == 2)
        {
            heart1.SetActive(false);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerHealth == 1)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(true);
        }
        if (playerHealth <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            gameOver.Play("gameOverAnim");
            gameObject.GetComponent<Animator>().speed = 0; // Setting the speed of the animator to "off" with 0 for a game over.
        }

        coinCount = PlayerPrefs.GetInt("ScoreCount");
        inGameCoinText.text = coinCount.ToString(); // Setting the text to the coin count int.

        arrowCount = PlayerPrefs.GetInt("ArrowCount");
        inGameArrowText.text = arrowCount.ToString(); // Setting the text to the arrow count int.

        healthPotionCount = PlayerPrefs.GetInt("HealthPotionCount");
        inGameHealthPotionText.text = healthPotionCount.ToString();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //enemy contact / hurting
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hurting == false && playerHealth > 0) // If the player collides with an enemy and is not already hurting and has health.
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.red; // makes the player sprite red when hurt.
            playerHealth--; // takes one heart.
            StartCoroutine(whitecolor());
            if (playerHealth > 0)
            {  // does a knock-back effect when the player is hurt:
                transform.position = Vector2.MoveTowards(transform.position, collision.gameObject.transform.position, -70 * Time.deltaTime);
            }
            hurting = true;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision) // Anything that collides with the player.
    {
        if (collision.gameObject.CompareTag("Heart") && playerHealth < 3)  // if the player collides with a heart has less than 3 hearts -> pick up the heart.
        {
            playerHealth++; // adds the new heart.
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("ScoreCount", coinCount + 1); // Saves and adds new coin(s) to the coin count.
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Shop"))
        {
            shopButtons.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            shopButtons.SetActive(false);
        }
    }

    IEnumerator whitecolor()
    {

        yield return new WaitForSeconds(2);
        if (playerHealth > 0)
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white; // if player still has health, change back to white sprite (not getting hurt)
        }
        hurting = false;
        GetComponent<BoxCollider2D>().enabled = false; // to reset collision so player can re-engage with collidable objects.
        GetComponent<BoxCollider2D>().enabled = true;

    }

    public void playAgain() // attached to the gameover.
    {
        SceneManager.LoadScene(1); // would have this set to "1", instead of "0", if we had a main menu setup.
    }

    public void mainMenu()  // we dont have a main menu setup, but this is how we would go back to the main menu.
    {
        SceneManager.LoadScene(0);
    }

    public void buyArrow()
    {
        if (coinCount >= 5)
        {
            PlayerPrefs.SetInt("ScoreCount", coinCount - 5);
            PlayerPrefs.SetInt("ArrowCount", arrowCount + 1);
        }
    }
    public void buyHealthPotion()
    {
        if (coinCount >= 10)
        {
            PlayerPrefs.SetInt("ScoreCount", coinCount - 10);
            PlayerPrefs.SetInt("HealthPotionCount", healthPotionCount + 1);
        }
    }
}
