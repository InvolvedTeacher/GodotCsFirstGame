using Godot;
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
    }
}