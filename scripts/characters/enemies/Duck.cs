using Godot;

namespace Game.Characters.Enemies
{
    public partial class Duck : CharacterBody2D
    {
        private AnimatedSprite2D _sprite;
        [Export] public float jump_speed { get; set; }

        public override void _Ready()
        {
            _sprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public override void _PhysicsProcess(double delta)
        {
            if (MoveAndSlide())
            {
                for (int i = 0; i < GetSlideCollisionCount(); ++i)
                {
                    KinematicCollision2D collision = GetSlideCollision(i);
                    if (collision.GetCollider().GetType() == typeof(NinjaFrog))
                        ((NinjaFrog)collision.GetCollider()).Kill();
                }
            }
        }

        public void SetAnimation(string new_animation)
        {
            if (_sprite == null) return;

            _sprite.Play(new_animation);
        }

        public string GetPlayingAnimationName()
        {
            return _sprite.Animation;
        }
    }
}