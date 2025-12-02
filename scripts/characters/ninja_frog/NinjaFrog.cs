using Godot;

public partial class NinjaFrog : CharacterBody2D
{
    [Export] private float horizontal_speed;
    private AnimatedSprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("Sprite");
    }

    public override void _PhysicsProcess(double _delta)
    {
        if (Velocity.X != 0)
            sprite.FlipH = Velocity.X < 0;
    }

    public void SetAnimation(string newAnimation)
    {
        if (sprite == null) return;

        sprite.Play(newAnimation);
    }

    public float GetHorizontalSpeed()
    {
        return horizontal_speed;
    }
}
