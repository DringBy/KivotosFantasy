using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        //player.setVelocity(currentXSpeed, player.jumpForce);
        //Debug.Log(rb.velocity.x + "  " + rb.velocity.y);
        player.setVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        Debug.Log("in jumpState");

        // 根据长按space时间决定跳跃高度
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentYSpeed = currentYSpeed * player.jumpDecay;
        }

        // 起跳过程中控制player
        if (xInput == 0 && currentXSpeed != 0)
        {
            player.decelerate(currentXSpeed, currentYSpeed);
        }
        else if (xInput * player.facingDir > 0)
        {
            player.accelerate(currentXSpeed, currentYSpeed, xInput);
        }
        else if (xInput * player.facingDir < 0)
        {
            player.reverseAccelerate(currentXSpeed, currentYSpeed, xInput);
        }
        else
        {
            player.setVelocity(currentXSpeed, currentYSpeed);
        }

        // 起跳过程中控制player
        //if (xInput != 0)
        //{
        //    player.setVelocity(xInput * player.maxSpeed * 0.8f, rb.velocity.y);
        //}

        if (currentYSpeed < 0)
        {
            stateMachine.changeState(player.airState);
            return;
        }
    }
}
