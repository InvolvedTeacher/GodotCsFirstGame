using Godot;

public partial class DoubleJumpMovementState : State
{
    private NinjaFrog _player;

    public override void Ready()
    {
        _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
    }

    public override void Enter()
    {
        _player.SetDoubleJumpAvailable(false);
        _player.SetAnimation("double_jump");

        Vector2 velocity = _player.Velocity;
        velocity.Y = _player.GetJumpSpeed();
        _player.Velocity = velocity;
    }
    public override void UpdatePhysics(double delta)
    {
        Vector2 velocity = _player.Velocity;

        velocity += _player.GetGravity() * (float)delta;

        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0f)
        {
            velocity.X = direction * _player.GetHorizontalSpeed();
        }

        _player.Velocity = velocity;
        _player.MoveAndSlide();

        if (_player.IsOnFloor())
        {
            if (_player.Velocity.X != 0)
                stateMachine.TransitionTo("RunMovementState");
            else
                stateMachine.TransitionTo("IdleMovementState");
        }
        else
        {
            if (_player.Velocity.Y > 0)
                stateMachine.TransitionTo("FallMovementState");
        }
    }

    public void _on_sprite_animation_finished()
    {
        if (_player.GetPlayingAnimationName() == "double_jump")
        {
            _player.SetAnimation("jump");
        }
    }
}
