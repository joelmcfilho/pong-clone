using Godot;
using System;

public partial class PlayerMovement : Paddle
{
	[Export] public float Speed = 20f;

	public Vector2 _initialPosition;

    public override void _Ready()
    {
        _initialPosition = GlobalPosition;
    }



	public override void _PhysicsProcess(double delta)
	{

		float direction = Input.GetAxis("ui_up","ui_down");

		MoveAndSlide();

		Position = new Vector2(Position.X, Mathf.Clamp((Speed * direction) + Position.Y,-150,260));
		
	}


}
