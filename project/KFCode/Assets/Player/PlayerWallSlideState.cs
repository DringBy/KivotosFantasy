using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        //player.currentXSpeed = 0;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        // 转入PlayerWallJumpState
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.changeState(player.wallJumpState);
            return; // 避免执行下方代码
        }

        // 若有输入且方向非现在方向则退出状态
        if(xInput != 0 && xInput != player.facingDir)
        {
            stateMachine.changeState(player.airState);
        }

        // 新添加：若没有按住方向键退出状态
        if(xInput == 0)
        {
            stateMachine.changeState(player.airState);
        }

        // 在该状态下 player可以被控制后移动得更快
        if (yInput < 0)
        {
            player.setVelocity(0, rb.velocity.y);
            //rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            player.setVelocity(0, rb.velocity.y * player.wallSlideDecay);
            //rb.velocity = new Vector2(0, rb.velocity.y * player.wallSlideDecay);
        }

        // 若接触地面则退出状态
        if (player.isGroundDetected())
        {
            stateMachine.changeState(player.idleState);
        }
    }
}
