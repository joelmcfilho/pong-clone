using Godot;
using System;

public partial class PlayerMovement : Paddle
{
	[Export] public float Speed = 20f;

	public Vector2 _initialPosition;
	private GameManager _gm;

    public override void _Ready()
    {
		base._Ready();
		_gm = GetNode<GameManager>("/root/GameManager");
        _initialPosition = GlobalPosition;
    }



	public override void _PhysicsProcess(double delta)
	{
		if(_gm.isCountdownActive == true)
		{
			return;
		}
		else
		{
			float direction = Input.GetAxis("ui_up","ui_down");
			AnimationControl(direction);

			MoveAndSlide();

			Position = new Vector2(Position.X, Mathf.Clamp((Speed * direction) + Position.Y,-150,260));
		}		
		
	}
	


}
