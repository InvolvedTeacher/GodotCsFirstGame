using Godot;
using Game.Characters;

namespace Game.StateMachines.MovementStateMachine
{
    public partial class DieMovementState : State
    {
        private NinjaFrog _player;
        private float die_direction;
        private Timer _death_timer;

        public override void Ready()
        {
            _player = (NinjaFrog)GetTree().GetFirstNodeInGroup("NinjaFrog");
            _death_timer = GetNode<Timer>("DeathTimer");
        }

        public override void Enter()
        {
            _player.SetDoubleJumpAvailable(true);
            _player.SetAnimation("die");
            _player.SetSpriteTranslation(new Vector2(0, 10));
            _player.Velocity = -_player.Velocity;
            if (_player.Velocity.X < 0)
                die_direction = -1f;
            else
                die_direction = 1f;
        }

        public override void Update(double delta)
        {
            _player.SetSpriteRotation(Mathf.MoveToward(_player.GetSpriteRotation(), 90 * die_direction, 5));
        }

        public override void UpdatePhysics(double delta)
        {
            Vector2 velocity = _player.Velocity;

            velocity += _player.GetGravity() * (float)delta;

            if (_player.IsOnFloor())
            {
                if (velocity.X != 0)
                    velocity = velocity.MoveToward(Vector2.Zero, 10);
                else
                {
                    if (_death_timer.IsStopped())
                        _death_timer.Start();
                }
            }

            _player.Velocity = velocity;
            _player.MoveAndSlide();
        }

        public void _on_death_timer_timeout()
        {
            GetTree().ReloadCurrentScene();
        }
    }
}