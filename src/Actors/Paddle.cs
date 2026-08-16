using Godot;

public partial class Paddle : CharacterBody2D
{

    public Vector2 _direction;

    public AnimatedSprite2D _sprite;

    public override void _Ready()
    {


        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    }

    public void AnimationControl(float direction)
    {
        if(_sprite == null)
        {
            GD.PrintErr($"ERRO: _sprite is NULL on Node {Name}");
            return;
        }

        if(direction < 0)
        {
            if (_sprite.Animation != "move_up")
                _sprite.Play("move_up");
        }
        else if(direction > 0)
        {
            if (_sprite.Animation != "move_down")
                _sprite.Play("move_down");
        }
        else
        {
            if (_sprite.Animation != "idle")
                _sprite.Play("idle");
        }
    }

    public void ResetPaddle(Vector2 initialPosition)
    {
        GlobalPosition = initialPosition;
        _direction = Vector2.Zero;
        
    }
}
