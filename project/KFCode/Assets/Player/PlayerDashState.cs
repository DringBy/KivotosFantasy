using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        player.coyoteUsageTimer = 0; // 防止在dash后立刻可以进行土狼
        stateTimer = player.dashDuration;
    }

    public override void exit()
    {
        base.exit();
        player.setVelocity(0, rb.velocity.y);
    }

    public override void update()
    {
        base.update();
        player.setVelocity(player.dashSpeed * player.dashDir, 0);

        if(!player.isGroundDetected() && (player.isWallDetected() && xInput == player.facingDir))
        {
            stateMachine.changeState(player.wallSlideState);
        }

        if (stateTimer < 0)
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
