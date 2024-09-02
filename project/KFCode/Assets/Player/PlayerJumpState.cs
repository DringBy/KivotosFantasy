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
        player.setVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        float vY = rb.velocity.y;

        // 起跳过程中控制player
        if (xInput == 0 && player.currentSpeed != 0)
        {
            player.decelerate();
        }
        else if (xInput * player.facingDir > 0)
        {
            player.accelerate(xInput);
        }
        else if (xInput * player.facingDir < 0)
        {
            player.reverseAccelerate(xInput);
        }

        // 起跳过程中控制player
        //if (xInput != 0)
        //{
        //    player.setVelocity(xInput * player.maxSpeed * 0.8f, rb.velocity.y);
        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            vY = vY * player.jumpDecay;
        }

        player.setVelocity(player.currentSpeed, vY);

        if (rb.velocity.y < 0)
        {
            stateMachine.changeState(player.airState);
        }
    }
}
