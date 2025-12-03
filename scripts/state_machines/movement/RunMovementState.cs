using Godot;

public partial class RunMovementState : State
{
    private NinjaFrog _player;
    private Timer _coyote_timer;
    private bool coyote_time_available;

    public override void Ready()
    {
        _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
        _coyote_timer = GetNode<Timer>("CoyoteTimer");
    }

    public override void Enter()
    {
        _player.SetDoubleJumpAvailable(true);
        _player.SetAnimation("run");

        coyote_time_available = true;
    }

    public override void UpdatePhysics(double delta)
    {
        Vector2 velocity = _player.Velocity;

        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0f)
        {
            velocity.X = direction * _player.GetHorizontalSpeed();
        }
        else
        {
            velocity.X = (float)Mathf.MoveToward(velocity.X, 0, 20);
        }

        _player.Velocity = velocity;
        _player.MoveAndSlide();

        if (!_player.IsOnFloor())
        {
            if (_player.Velocity.Y < 0)
                stateMachine.TransitionTo("JumpMovementState");
            else
            {
                if (coyote_time_available)
                {
                    if (_coyote_timer.IsStopped())
                        _coyote_timer.Start();
                }
                else
                    stateMachine.TransitionTo("FallMovementState");
            }
        }
        else
        {
            coyote_time_available = true;
            _coyote_timer.Stop();
        }

        if (_player.Velocity.X == 0f)
            stateMachine.TransitionTo("IdleMovementState");
    }

    public override void Exit()
    {
        _coyote_timer.Stop();
    }

    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpMovementState");
    }

    public void _on_coyote_timer_timeout()
    {
        coyote_time_available = false;
    }
}
