using System.Threading.Tasks;
using Godot;

public partial class Paddle : CharacterBody2D
{

    public Vector2 _direction;

    public AnimatedSprite2D _sprite;

    public bool isHitting = false;

    public override void _Ready()
    {


        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _sprite.Play("idle");

        _sprite.AnimationFinished += OnAnimationFinished;

    }

    public async Task AnimationControl(float direction)
    {
        if(_sprite == null)
        {
            GD.PrintErr($"ERRO: _sprite is NULL on Node {Name}");
            return;
        }
        if(isHitting == true) return;


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

    public void PlayHitAnimation()
    {
        if (_sprite == null)
            return;

        isHitting = true;
        _sprite.Play("hit");
        
    }

    private void OnAnimationFinished()
{
    if (_sprite.Animation == "hit")
    {
  
        isHitting = false;
        _sprite.Play("idle");
    }
}

    public void ResetPaddle(Vector2 initialPosition)
    {
        GlobalPosition = initialPosition;
        _direction = Vector2.Zero;
        
    }
}
