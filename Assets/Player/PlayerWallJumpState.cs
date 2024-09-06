using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        stateTimer = player.wallJumpTime;
        player.currentXSpeed = player.wallJumpForceX * -player.facingDir;
        player.setVelocity(player.currentXSpeed, player.wallJumpForceY);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
        {
            stateMachine.changeState(player.airState);
        }
        if (player.isGroundDetected())
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
