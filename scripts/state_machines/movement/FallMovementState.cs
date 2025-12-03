using Godot;
using Game.Characters;

namespace Game.StateMachines.MovementStateMachine
{
    public partial class FallMovementState : State
    {
        private NinjaFrog _player;

        public override void Ready()
        {
            _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
        }

        public override void Enter()
        {
            _player.SetAnimation("fall");
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
        }

        public override void HandleInput(InputEvent @event)
        {
            if (@event.IsActionPressed("jump") && _player.IsDoubleJumpAvailable())
                stateMachine.TransitionTo("DoubleJumpMovementState");
        }
    }
}