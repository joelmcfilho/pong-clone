using Godot;

public partial class AIBehavior : Paddle
{
	[Export] public float Speed = 150.0f;

	private BallBehavior _ball;

	private Vector2 _paddlePos = Vector2.Zero;
	public Vector2 _initialPosAI;

	private GameManager _gm;
	

    public override void _Ready()
    {

		_ball = GetNode<BallBehavior>("../Ball");
		_gm = GetNode<GameManager>("/root/GameManager");

		_initialPosAI = GlobalPosition;
        
    }


	public override void _PhysicsProcess(double delta)
	{
		if(_gm.gameModeSelect == GameMode.Single)
		{
			
			if(_ball._direction.X > 0)
			{
				float paddleDirection = Mathf.Sign(_ball.GlobalPosition.Y - GlobalPosition.Y);
				Velocity = new Vector2(0,paddleDirection*Speed);
				MoveAndSlide();
			}
			else
			{
				Velocity = Vector2.Zero;
				MoveAndSlide();			
			}
		}
		else if(_gm.gameModeSelect == GameMode.Multi)
		{
			Speed = 20f;
			if(_gm.isCountdownActive == true)
			{
				return;
			}
			else
			{
				float direction = Input.GetAxis("up_player_2","down_player_2");

				MoveAndSlide();

				Position = new Vector2(Position.X, Mathf.Clamp((Speed * direction) + Position.Y,-150,260));
			}	
		}

		
		

	}
}
