using Godot;

public partial class InitSurvival : Node2D
{
	private HudSurvival _hudSurvival;
	private PlayerMovement _paddle;
	private BallBehavior _ball;
	private Timer _ballTimer;

	private GameManager _gm;
	private AudioManager _am;

	public bool survivalStart = false;

	public override async void _Ready()
	{
		_hudSurvival = GetNode<HudSurvival>("Hud_Survival");
		_paddle = GetNode<PlayerMovement>("PlayerBar");
		_ball = GetNode<BallBehavior>("Ball");
		_ballTimer = GetNode<Timer>("Timer");


		_gm = GetNode<GameManager>("/root/GameManager");
		_am = GetNode<AudioManager>("/root/AudioManager");

		_gm.InitializeSurvival(this,
								_ball,
								_paddle,
								_hudSurvival
								);

		await _hudSurvival.Countdown();
		_am.PlayMusic(GD.Load<AudioStream>("res://assets/sounds/Music/survivalmode.mp3"));

		_ball.StartBall();
		_ball.BallAnimationControl();
		survivalStart = true;

		_gm.BallSpawnTimerControl(_ballTimer);

	}


}
