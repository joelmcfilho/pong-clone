using Godot;

public partial class Paddle : CharacterBody2D
{

    public Vector2 _direction;
    public void ResetPaddle(Vector2 initialPosition)
    {
        GlobalPosition = initialPosition;
        _direction = Vector2.Zero;
        
    }
}
