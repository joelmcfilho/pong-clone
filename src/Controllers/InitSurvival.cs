using Godot;

public partial class InitSurvival : Node2D
{
	private HudSurvival _hudSurvival;
	private PlayerMovement _paddle;
	private BallBehavior _ball;

	private GameManager _gm;

	public override void _Ready()
	{
		_hudSurvival = GetNode<HudSurvival>("Hud_Survival");
		_paddle = GetNode<PlayerMovement>("/root/Game/PlayerBar");
		_ball = GetNode<BallBehavior>("/root/Game/Ball");

		_gm = GetNode<GameManager>("/root/GameManager");

		_gm.InitializeSurvival(_ball,_paddle,_hudSurvival);
	}


	public override void _Process(double delta)
	{
	}
}
