using Godot;

public partial class RunMovementState : State
{
    private NinjaFrog _player;

    public override void Ready()
    {
        _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
    }

    public override void Enter()
    {
        _player.SetDoubleJumpAvailable(true);
        _player.SetAnimation("run");
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
                stateMachine.TransitionTo("FallMovementState");
        }
        else
        {
            if (_player.Velocity.X == 0f)
                stateMachine.TransitionTo("IdleMovementState");
        }
    }

    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpMovementState");
    }
}
