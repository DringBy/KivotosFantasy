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

        float xDashDir = Input.GetAxisRaw("Horizontal");
        float yDashDir = Input.GetAxisRaw("Vertical");

        // 八向dash
        if(xDashDir == 0 && yDashDir == 0)  // 没有输入方向则把面朝方向作为dash方向
        {
            xDashDir = player.facingDir;
            player.setVelocity(player.dashSpeed * xDashDir, 0);
            player.currentXSpeed = player.dashSpeed * xDashDir;
        }
        else if(xDashDir == 0 || yDashDir == 0)  // 仅输入一个维度的方向
        {
            player.setVelocity(player.dashSpeed * xDashDir, player.dashSpeed * yDashDir);
            player.currentXSpeed = player.dashSpeed * xDashDir;
        }
        else  // 输入两个维度的方向
        {
            player.setVelocity(player.dashSpeed * xDashDir * 0.7071f, player.dashSpeed * yDashDir * 0.7071f);
            player.currentXSpeed = player.dashSpeed * xDashDir;
        }
        
        //player.setVelocity(player.dashSpeed * player.dashDir, 0);

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
