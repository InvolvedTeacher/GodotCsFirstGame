using Godot;
using Game.Characters;
using Game.Characters.Enemies;

namespace Game.StateMachines.DuckStateMachine
{
    public partial class IdleDuckState : State
    {
        private Duck _duck;

        public override void Ready()
        {
            _duck = GetParent().GetParent<Duck>();
        }

        public override void Enter()
        {
            _duck.SetAnimation("idle");
        }

        public override void UpdatePhysics(double _delta)
        {
            if (!_duck.IsOnFloor())
                stateMachine.TransitionTo("FallDuckState");
        }

        public void _on_observation_area_body_entered(Node2D body)
        {
            if (body.GetType() == typeof(NinjaFrog))
                ((NinjaFrog)body).Jumped += _on_observed_body_jump;
        }

        public void _on_observation_area_body_exited(Node2D body)
        {
            if (body.GetType() == typeof(NinjaFrog))
                ((NinjaFrog)body).Jumped -= _on_observed_body_jump;
        }

        public void _on_observed_body_jump()
        {
            if (_duck.IsOnFloor())
                stateMachine.TransitionTo("JumpDuckState");
        }
    }
}