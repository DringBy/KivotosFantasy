using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected Rigidbody2D rb;

    protected float stateTimer;

    protected float xInput;
    protected float yInput;

    protected float currentXSpeed;
    protected float currentYSpeed;

    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void update()
    {
        updateSpeed();

        // 更新计时器
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        //player.anim.SetFloat("yVelocity", rb.velocity.y);
        player.anim.SetFloat("yVelocity", currentYSpeed);
    }

    public virtual void enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        updateSpeed();
    }

    public virtual void exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    ///  获取当前真实速度
    /// </summary>
    public void updateSpeed()
    {
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;
    }

}