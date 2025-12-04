using Godot;
using Game.StateMachines;

namespace Game.Characters
{
    public partial class NinjaFrog : CharacterBody2D
    {
        [Export] private float horizontal_speed;
        [Export] private float jump_speed;
        private bool double_jump_available;
        private AnimatedSprite2D sprite;
        private StateMachine movement_state_machine;

        [Signal] public delegate void JumpedEventHandler();

        public override void _Ready()
        {
            sprite = GetNode<AnimatedSprite2D>("Sprite");
            movement_state_machine = GetNode<StateMachine>("MovementStateMachine");
        }

        public override void _PhysicsProcess(double _delta)
        {
            if (Velocity.X != 0)
                sprite.FlipH = Velocity.X < 0;
        }

        public void SetAnimation(string new_animation)
        {
            if (sprite == null) return;

            sprite.Play(new_animation);
        }

        public float GetHorizontalSpeed()
        {
            return horizontal_speed;
        }

        public float GetJumpSpeed()
        {
            return jump_speed;
        }

        public bool IsDoubleJumpAvailable()
        {
            return double_jump_available;
        }

        public void SetDoubleJumpAvailable(bool b)
        {
            double_jump_available = b;
        }

        public string GetPlayingAnimationName()
        {
            return sprite.Animation;
        }

        public void Kill()
        {
            CollisionMask = 1;
            movement_state_machine.TransitionTo("DieMovementState");
        }

        public float GetSpriteRotation()
        {
            return sprite.RotationDegrees;
        }

        public void SetSpriteRotation(float new_rotation)
        {
            sprite.RotationDegrees = new_rotation;
        }

        public void SetSpriteTranslation(Vector2 translation)
        {
            sprite.Translate(translation);
        }
    }
}