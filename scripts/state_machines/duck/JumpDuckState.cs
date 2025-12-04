using Godot;
using Game.Characters.Enemies;

namespace Game.StateMachines.DuckStateMachine
{
    public partial class JumpDuckState : State
    {
        private Duck _duck;

        public override void Ready()
        {
            _duck = GetParent().GetParent<Duck>();
        }

        public override void Enter()
        {
            _duck.SetAnimation("jump");
            Vector2 velocity = _duck.Velocity;
            velocity.Y += _duck.jump_speed;
            _duck.Velocity = velocity;
        }

        public override void UpdatePhysics(double delta)
        {
            Vector2 velocity = _duck.Velocity;

            velocity += _duck.GetGravity() * (float)delta;

            _duck.Velocity = velocity;
            _duck.MoveAndSlide();

            if (_duck.Velocity.Y > 0)
            {
                stateMachine.TransitionTo("FallDuckState");
            }
        }
    }
}