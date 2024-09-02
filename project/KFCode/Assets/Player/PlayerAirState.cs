using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        // 下落途中控制player
        if(xInput != 0)
        {
            player.setVelocity(xInput * player.maxSpeed * 0.8f, rb.velocity.y);
        }

        if(player.isGroundDetected())
        {
            player.coyoteUsageTimer = player.coyoteCountDown;
            stateMachine.changeState(player.idleState);
        }

        if (player.isWallDetected() && xInput == player.facingDir)
        {
            stateMachine.changeState(player.wallSlideState);
        }

        // 土狼
        if (!player.isGroundDetected())
        {
            player.coyoteUsageTimer -= Time.deltaTime;
        }

        // 按下空格键后 将跳跃缓冲的倒计时重新计时
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.jumpBufferUsageTimer = player.jumpBufferCountDown;
        }
        else
        {
            player.jumpBufferUsageTimer -= Time.deltaTime;
        }

        if (player.jumpBufferUsageTimer>0f && player.coyoteUsageTimer > 0f)
        {
            stateMachine.changeState(player.jumpState);
            player.coyoteUsageTimer = 0f;
            player.jumpBufferUsageTimer = 0f;
        }
    }
}
