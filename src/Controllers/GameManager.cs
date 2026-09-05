using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class GameManager : Node
{
	private int _player1Score = 0;
	private int _player2Score = 0;
	private double _time = 0.0f;
	public double saveTime = 0.0f;
	public bool survivalStartPublic = false;
	public int ballCount = 1;

	private BallBehavior _ball;
	private PlayerMovement _playerPaddle;
	private AIBehavior _aiPaddle;
	private Hud _hud;
	private HudSurvival _hudSurvival;
	private Side _side;
	private InitSurvival _initSurvival;
	private Save _save;
	private List<BallBehavior> _ballContainer = new List<BallBehavior>();
	public bool isCountdownActive {get;set;}

	private AudioManager _am;

	public GameMode gameModeSelect;

    public override void _Ready()
    {
		_save = new Save();

		saveTime = _save.LoadTimeRecord();

		gameModeSelect = new GameMode();

        
    }


    public override void _Process(double delta)
    {
		if (gameModeSelect != GameMode.Survival)
        return;

    	if (_initSurvival == null ||
        !GodotObject.IsInstanceValid(_initSurvival))
        return;

    	if (!_initSurvival.survivalStart)
        return;

    	if (_hudSurvival == null ||
        !GodotObject.IsInstanceValid(_hudSurvival))
        return;

		_hudSurvival.UpdateSurvivalTimer(_time += delta);
		RegisterTime(_time);
		_hudSurvival.UpdateSurvivalBallCounter(ballCount);

		if(_initSurvival.survivalStart == false)
		{
			KillTimeCount();
		}		

		if(_initSurvival.survivalStart == true)
		{
			survivalStartPublic = true;
		}

		
        
    }

	
	public async Task GoalScore(Side side)
	{
		if(side == Side.Player)
		{
			_player1Score ++;
			_hud.UpdateScoreHUD(_player1Score,_player2Score);
		}
		if(side == Side.AI || side == Side.Player2)
		{
			_player2Score ++;
			_hud.UpdateScoreHUD(_player1Score,_player2Score);
			
		}

		//End Game conditions

		if(_player1Score == 3)
		{
			await _hud.ShowWinner(Side.Player);
			_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
			_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
			ResetScore();
			await _ball.ResetBall();
			await _hud.Countdown();
			_ball.StartBall();
			return;
		}
		else if(_player2Score == 3)
		{
			if(gameModeSelect == GameMode.Multi)
			{
				await _hud.ShowWinner(Side.Player2);
				_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
				_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
				ResetScore();
				await _ball.ResetBall();
				await _hud.Countdown();
				_ball.StartBall();
				return;
			}
			else if(gameModeSelect == GameMode.Single)
			{
				await _hud.ShowWinner(Side.AI);
				_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
				_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
				ResetScore();
				await _ball.ResetBall();
				await _hud.Countdown();
				_ball.StartBall();
				return;
			}
			
		}	

		// Continue Game, if endgame conditions are not made, the game will reset and continues

		await _hud.ShowPointSign();
		_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
		_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
		await _ball.ResetBall();
		await _hud.Countdown();
		_ball.StartBall();

	}

	public void SurvivalEndGame()
	{
		_initSurvival.survivalStart = false;
		_hudSurvival.ShowSurvivalEndGameText(_time);
		_save.CheckRecord(_time,saveTime);
	}

	public void ResetScore()
	{
		_player1Score = 0;
		_player2Score = 0;
		_hud.UpdateScoreHUD(_player1Score,_player2Score);	
	}

	//INITIALIZATION METHOD FOR CLASSIC GAME
	public void InitializeGame(	
		BallBehavior ball,
		PlayerMovement Paddle,
		Hud hud,
		AIBehavior player2Paddle)
	{
		_ball = ball;
		_hud = hud;
		_playerPaddle = Paddle;
		_aiPaddle = player2Paddle;
	}

		//INITIALIZATION METHOD FOR SURVIVAL GAME
	public void InitializeSurvival(
		InitSurvival initSurvival,
		BallBehavior ball,
		PlayerMovement Paddle,
		HudSurvival hud
		)
	{
		_initSurvival = initSurvival;
		_ball = ball;
		_playerPaddle = Paddle;
		_hudSurvival = hud;
	}

	public double CountTime(double delta)
	{
		_time += delta;

		return _time;
	}

	public string RegisterTime(double time)
	{
		String timeText = time.ToString("F1");

		return timeText;
	}

	//REVISAR NO FIM DA ETAPA
	public void KillTimeCount()
	{
		RegisterTime(0.0f);
		_time = 0.0f;
	}

	public void ClearSurvival()
	{
    _initSurvival = null;
    _hudSurvival = null;
    _ball = null;
    _playerPaddle = null;

    _time = 0.0;
	}

	//Survival Mode ball instantiation
	public void BallSpawnTimerControl(Timer timer)
	{
		timer.Timeout += SpawnBall;
		timer.Start();
	}

	public async void SpawnBall()
	{
		await _hudSurvival.BallHeadsupCountdown();

		PackedScene ballInstance = GD.Load<PackedScene>("res://ball.tscn");

		BallBehavior extraBall = ballInstance.Instantiate<BallBehavior>();

		GetTree().CurrentScene.AddChild(extraBall);
		_am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/newball.wav"));

		_ballContainer.Add(extraBall);
		ballCount = _ballContainer.Count + 1;

		extraBall.StartBall();
	}

	public void ClearBalls()
	{
		foreach(BallBehavior ball in _ballContainer)
		{
			if(IsInstanceValid(ball))
			{
				ball.QueueFree();
			}
		}

		_ballContainer.Clear();
		ballCount = 1;
	}




	

	
	
}
