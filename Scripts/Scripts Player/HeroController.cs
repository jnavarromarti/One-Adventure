using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    /*Definimos las variables del Player o Hero*/
    [Header("Animation Variables")]
    Animator animator; // Hacemos una llamada al animator
    /*Definimos las variables del movimiento del player*/
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rdb2D; // Hacemos una llamada al rigidbody2D 
    [SerializeField] private float speed;
    [SerializeField] private GroundCheck footA;
    [SerializeField] private GroundCheck footB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float longJumpMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    private float runButtonHoldTime;
    // Estados del Player o del Hero
    public bool isWalking;
    public bool isJumping;
    public bool canJump;
    private bool jumpHeld;
    private bool isAttacking;
    private bool isChargedAttack;
    public bool isJumpAttack;
    public bool isInteracting;
    // Llamamos al SwordController y al DamageController
    [SerializeField] GroundCheck[] groundCheck;
    [SerializeField] SwordController swordController;
    [SerializeField] DamageControl damageController;
    // Creamos unas variables booleanas para gestionar los botones del mando para los controles
    [SerializeField] private bool jumpPressed = false;
    [SerializeField] private bool attackPressed = false;
    [SerializeField] private bool chargeAttackPressed = false;
    [SerializeField] public bool interactPressed = false;

    [SerializeField] private Vector3 spawnPoint;



    void Start()
    {
        rdb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponentsInChildren<GroundCheck>();
        spawnPoint = transform.position;
    }
    void Update()
    {
        // Llamamos a los metodos que gestionan el Player
        HandleControls();
        HandleMovement();
        HandleDirection();
        HandleJump();
        HandleAttack();
        HandleChargedAttack();
        HandleJumpAttack();
    }
    public void HandleControls()
    {
        // Iniciamos las variables de los controles del mando junto con los inputs asociados a estas (todas menos el movimiento)
        jumpPressed = Input.GetButtonDown("Fire1");
        attackPressed = Input.GetButtonDown("Fire2");
        chargeAttackPressed = Input.GetButtonDown("Fire3");
        interactPressed = Input.GetButton("Interact");
    }
    void HandleMovement()
    {
        /*Explicacion: las variables horizontal y vertical recogen el movimiento de joystick
        y se crea un vector dirección en base a estas. En base a el vector direccion el componente
        rigid body se mueve de forma horizontal en base a la velocidad (speed), finalmente dependiendo de la velocidad
        en x se ejecuta un estado booleano del animator definido en el motor (iddle o walk)*/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector2(horizontal, vertical);
        rdb2D.velocity = new Vector2(direction.x * speed, rdb2D.velocity.y);
        if ((Mathf.Abs(rdb2D.velocity.x) > 0))
        {
            animator.SetBool("Walk", true);
        }
        else if (Mathf.Abs(rdb2D.velocity.x) == 0)
        {
            animator.SetBool("Walk", false);
        }
    }
    void HandleDirection()
    {
        /*Explicacion: es un metodo simple para voltear el personaje en base a su ultima direccion*/
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void HandleJump()
    {
        /*Explicacion: El metodo HandleJump funciona de la siguiente forma, se hace una llamada a los children foot A/B y 
        se detecta si estan grounded y despues se activa un booleano que indica si se ha mantenido presionado o no el boton de salto ("Fire1"), en caso de se afirmativo
        este proporcionara mas impulso al salto atraves de la operacion jumpmultiplier - 1. */
        bool isFootAgrounded = footA.isGrounded;
        bool isFootBgrounded = footB.isGrounded;
        jumpHeld = (!isFootAgrounded && !isFootBgrounded) && Input.GetButton("Fire1") ? true : false;
        if (jumpPressed && (isFootAgrounded || isFootBgrounded) && !isJumping && canJump)
        {
            isJumping = true;
            rdb2D.velocity = Vector2.up * jumpForce;
            canJump = false;
        }
        if (jumpHeld && rdb2D.velocity.y > 0)
        {
            rdb2D.velocity += Vector2.up * Physics2D.gravity.y * (longJumpMultiplier - 1) * Time.deltaTime;
            canJump = false;
            isJumping = true;
            animator.SetBool("Jump", true);
        }
        else if (!jumpHeld && rdb2D.velocity.y > 0)
        {
            rdb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            canJump = false;
            isJumping = true;
            animator.SetBool("Jump", true);
        }
        // Gestionamos las partes del salto en base a la velocidad, esto tambien es util para ejecutar la animacion jumpout cuando estamos cayendo al vacio
        else if (rdb2D.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("JumpOut", true);
        }
        // Reseteamos las variables una vez volvemos a estar grounded
        if ((isFootAgrounded || isFootBgrounded))
        {
            isJumping = false;
            canJump = true;
            animator.SetBool("JumpOut", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Iddle", true);
        }
    }
    // Metodo de ataque basico
    void HandleAttack()
    {
        if (attackPressed && !isAttacking){
            isAttacking = true;
            StartCoroutine(Attack());
            swordController.SetAttackType(SwordController.AttackType.Attack);
        }
        
    }
    // Metodo de ataque cargado
    void HandleChargedAttack()
    {
        if (chargeAttackPressed && !isChargedAttack)
        {
            isChargedAttack = true;
            StartCoroutine(ChargedAttack());
            swordController.SetAttackType(SwordController.AttackType.ChargedAttack);
        }
    }
    //Metodo de salto + ataque
    void HandleJumpAttack()
    {
        if (isJumping && !isJumpAttack && Input.GetButton("Fire2"))
        {
            isJumpAttack = true;
            StartCoroutine(JumpAttack());
            swordController.SetAttackType(SwordController.AttackType.JumpAttack);
        }
    }
    // Corrutinas para gestionar las animaciones
    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
        isAttacking = false;
    }
    IEnumerator ChargedAttack()
    {
        animator.SetBool("Charged_Attack", true);
        yield return new WaitForSeconds(1.05f);
        animator.SetBool("Charged_Attack", false);
        isChargedAttack = false;
    }
    IEnumerator JumpAttack()
    {
        animator.SetBool("Jump_Attack", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("Jump_Attack", false);
        isJumpAttack = false;
    }
    // Metodo respawn que nos relocaliza en el punto seleccionado.
    public void Respawn()
    {
        transform.position = spawnPoint;
    }
}
