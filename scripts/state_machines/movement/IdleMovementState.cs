using Godot;

public partial class IdleMovementState : State
{
    private NinjaFrog _player;

    public override void Ready()
    {
        _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
    }

    public override void Enter()
    {
        _player.SetAnimation("idle");
    }

    public override void Update(double _delta)
    {
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right"))
            stateMachine.TransitionTo("RunMovementState");
    }

    public override void UpdatePhysics(double _delta)
    {
        if (!_player.IsOnFloor())
        {
            if (_player.Velocity.Y < 0)
                stateMachine.TransitionTo("JumpMovementState");
            else
                stateMachine.TransitionTo("FallMovementState");
        }
    }

    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpMovementState");
    }
}