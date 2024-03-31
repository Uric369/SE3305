using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("结束面板")]
    public Canvas gameOverCanvas;

    [Header("监听事件")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public SceneManager sceneManager;

    public Vector2 inputDirection;
    public PlayerInputController inputController;
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D capsuleCollider;
    private ScoreManager scoreManager;
    private Character character;
    private PlayerAudio playerAudio;

    [Header("基本参数")]
    public float scale;
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public float deadBound;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D suspense;

    // Start is called before the first frame update

    private void Awake()
    {
        inputController = new PlayerInputController();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        scoreManager = GetComponent<ScoreManager>();
        character = GetComponent<Character>();
        playerAudio = GetComponent<PlayerAudio>();

        inputController.Gameplay.Jump.started += Jump;
        inputController.Gameplay.Attack.started += Attack;
        inputController.UI.Quit.started += QuitGame;
    }

    private void QuitGame(InputAction.CallbackContext context)
    {
        gameOverCanvas.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        inputController.Enable();
        sceneLoadEvent.LoadRequestEvent += OnSceneLoad;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        inputController.Disable();
        sceneLoadEvent.LoadRequestEvent -= OnSceneLoad;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        inputController.Gameplay.Enable();
    }

    private void OnSceneLoad(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        isDead = false;
        spriteRenderer.enabled = true;
        inputController.Gameplay.Disable();
        playerAnimation.Restart();
    }

    private void Update()
    {
        // 读取input的值
        inputDirection = inputController.Gameplay.Move.ReadValue<Vector2>();
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt) Move();
        // Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        // 翻转人物
        float faceDir = transform.localScale.x;

        if (inputDirection.x > 0)
        {
            faceDir = - scale;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = scale;
        }


        transform.localScale = new Vector3(faceDir, scale, transform.localScale.z);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP");
        if (physicsCheck.isGround)
        {
            playerAudio.Jump();
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("ATTACK");
        playerAnimation.Attack();
    }

    public void getHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void die()
    {
        Debug.Log("寄咯！");
        spriteRenderer.enabled = false;
        inputController.Gameplay.Disable();
        gameOverCanvas.gameObject.SetActive(true);
        isDead = true;
    }

    private void CheckState()
    {
        capsuleCollider.sharedMaterial = physicsCheck.isGround ? normal : suspense;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadLine")
        {
                // 获取Cinemachine Virtual Camera组件
                Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

                // 将Follow目标设置为空
                virtualCamera.Follow = null;
                virtualCamera.LookAt = null;
                die();
            return;
        }

        if (collision.tag == "Teleport")
        {
            collision.GetComponent<Teleport>()?.TriggerAction();
            Debug.Log("撞到了");
        }

            if (collision.tag == "Crystal")
        {
            collision.gameObject.SetActive(false);
            scoreManager.collectCrystal();
            playerAudio.Crystal();
            return;
        }
        if (collision.tag == "Water")
        {
            die();
            return;
        }
        if (collision.tag == "Healer")
        {
            collision.gameObject.SetActive(false);
            scoreManager.collectHealer();
            character.Heal();
            playerAudio.Heal();
            return;
        }
        if (collision.tag == "Stopper")
        {
            collision.gameObject.SetActive(false);
            scoreManager.collectStopper();
            playerAudio.Stopper();
            return;
        }
        if (collision.tag == "Reverser")
        {
            collision.gameObject.SetActive(false);
            scoreManager.collectReverser();
            playerAudio.Reverser();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            die();
        }
    }

}
