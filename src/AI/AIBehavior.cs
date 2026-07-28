using Godot;

public partial class AIBehavior : Paddle
{
	[Export] public float Speed = 150.0f;

	private BallBehavior _ball;

	private Vector2 _paddlePos = Vector2.Zero;
	public Vector2 _initialPosAI;

    public override void _Ready()
    {

		_ball = GetNode<BallBehavior>("../Ball");
		_initialPosAI = GlobalPosition;
        
    }


	public override void _PhysicsProcess(double delta)
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
}
