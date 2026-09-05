using Godot;

public partial class InitGame : Node2D
{

	private Hud _hud;
	private BallBehavior _ball;
	private PlayerMovement _playerPaddle;
	private AIBehavior _player2Paddle;

	private GameManager _gm;
	private AudioManager _am;

	public async override void _Ready()
	{
		_hud = GetNode<Hud>("Hud");
		_ball = GetNode<BallBehavior>("Ball");
		_playerPaddle = GetNode<PlayerMovement>("PlayerBar");
		_player2Paddle = GetNode<AIBehavior>("AIBar");

		_gm = GetNode<GameManager>("/root/GameManager");
		_am = GetNode<AudioManager>("/root/AudioManager");

		_gm.InitializeGame(_ball,_playerPaddle,_hud,_player2Paddle);
		

		_hud.UpdateScoreHUD(0,0);

		await _hud.Countdown();

		_am.PlayMusic(GD.Load<AudioStream>("res://assets/sounds/Music/classicmode.mp3"));

		_ball.StartBall();
		_ball.BallAnimationControl();

		
	}

    


}
