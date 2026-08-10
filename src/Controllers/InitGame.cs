using Godot;

public partial class InitGame : Node2D
{

	private Hud _hud;
	private BallBehavior _ball;

	public async override void _Ready()
	{
		_hud = GetNode<Hud>("Hud");
		_ball = GetNode<BallBehavior>("Ball");
		

		_hud.UpdateScoreHUD(0,0);

		await _hud.Countdown();

		_ball.StartBall();

		
	}

    


}
