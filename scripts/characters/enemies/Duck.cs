using Godot;

namespace Game.Characters.Enemies
{
    public partial class Duck : CharacterBody2D
    {
        private AnimatedSprite2D _sprite;

        public override void _Ready()
        {
            _sprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public void SetAnimation(string new_animation)
        {
            if (_sprite == null) return;

            _sprite.Play(new_animation);
        }
    }
}