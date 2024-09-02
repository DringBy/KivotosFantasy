using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float maxSpeed = 9f;
    public float jumpForce = 12f;
    public float acceleration = 0.1f;  // 加速度
    public float deceleration = 0.3f;  // 减速度
    public float currentSpeed;
    public float currentLeftSpeed;
    public float currentRightSpeed;

    [Header("Dash Info")]
    [SerializeField] private float dashCoolDown;  // 多久能冲
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;  // 冲多久
    public float dashDir {  get; private set; }

    // 土狼
    [Header("Coyote Info")]
    public float coyoteUsageTimer;
    public float coyoteCountDown;  // 离地后多久能进行土狼
    //[SerializeField] private float coyoteCountDown;
    //private float coyoteUsageTimer;
    //public bool coyotePermitted;

    // 跳跃缓冲
    [Header("Jump Buffer Info")]
    public float jumpBufferUsageTimer;
    public float jumpBufferCountDown;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Component
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAccelerateState accelerateState { get; private set; }
    public PlayerDecelerateState decelerateState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump");
        accelerateState = new PlayerAccelerateState(stateMachine, this, "Move");
        decelerateState = new PlayerDecelerateState(stateMachine, this, "Move");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.update();
        //flipController(rb.velocity.x);
        checkForDashInput();
    }

    /// <summary>
    ///  设置player对象的速度，传入参数为最终的速度，函数内不会做计算。
    /// </summary>
    /// <param name="_xVelocity"></param>
    /// <param name="_yVelocity"></param>
    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        flipController(_xVelocity);
    }

    // 没搞懂
    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
     

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y ));
    }

    public void flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void flipController(float _x)
    {
        // 判断速度方向和面向方向是否一致
        if (_x > 0 && !facingRight)
        {
            flip();
        }else if(_x < 0 && facingRight)
        {
            flip();
        }
    }

    private void checkForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (isWallDetected() && facingDir == Input.GetAxisRaw("Horizontal"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            
            if (dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.changeState(dashState);
        }
    }
}
