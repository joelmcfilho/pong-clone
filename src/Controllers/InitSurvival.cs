using Godot;

public partial class InitSurvival : Node2D
{
	private HudSurvival _hudSurvival;
	private PlayerMovement _paddle;
	private BallBehavior _ball;

	private GameManager _gm;

	public override async void _Ready()
	{
		_hudSurvival = GetNode<HudSurvival>("Hud_Survival");
		_paddle = GetNode<PlayerMovement>("PlayerBar");
		_ball = GetNode<BallBehavior>("Ball");

		_gm = GetNode<GameManager>("/root/GameManager");

		_gm.InitializeSurvival(_ball,_paddle,_hudSurvival);

		await _hudSurvival.Countdown();

		_ball.StartBall();
		_ball.BallAnimationControl();
	}


	public override void _Process(double delta)
	{
	}
}
