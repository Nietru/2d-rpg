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

    }
}
