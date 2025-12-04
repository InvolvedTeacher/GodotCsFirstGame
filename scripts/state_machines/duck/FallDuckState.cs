using Godot;
using Game.Characters.Enemies;

namespace Game.StateMachines.DuckStateMachine
{
    public partial class FallDuckState : State
    {
        private Duck _duck;

        public override void Ready()
        {
            _duck = GetParent().GetParent<Duck>();
        }

        public override void Enter()
        {
            _duck.SetAnimation("fall");
        }

        public override void UpdatePhysics(double delta)
        {
            Vector2 velocity = _duck.Velocity;

            velocity += _duck.GetGravity() * (float)delta;

            _duck.Velocity = velocity;

            if (_duck.IsOnFloor())
            {
                stateMachine.TransitionTo("IdleDuckState");
            }
        }
    }
}