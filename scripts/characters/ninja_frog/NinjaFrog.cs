using Godot;

public partial class NinjaFrog : CharacterBody2D
{
    [Export] private float horizontal_speed;
    [Export] private float jump_speed;
    private bool double_jump_available;
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
}
