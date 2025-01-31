using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Bouncing between new scenes when we restart the game.
using TMPro;    // For all of our Text and Buttons

public class playerComponent : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator playerAnim;

    public int direction;

    public float attackingCoolDown;

    public GameObject sword1;
    public GameObject bow1;
    public int arrowCount;
    public GameObject arrowPrefab;
    public int weaponInUse;

    public bool hurting;
    public GameObject playerSprite;
    public bool stillInEnemyRange;

    public int playerHealth;
    public Animator gameOver;
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
