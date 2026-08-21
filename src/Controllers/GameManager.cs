using Godot;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class GameManager : Node
{
	private int _player1Score = 0;
	private int _player2Score = 0;
	private BallBehavior _ball;
	private PlayerMovement _playerPaddle;
	private AIBehavior _aiPaddle;
	private Hud _hud;
	private HudSurvival _hudSurvival;
	private Commands _cmds;
	private Side _side;
	public bool isCountdownActive {get;set;}

	public GameMode gameModeSelect;
	
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
		
		_hudSurvival.ShowSurvivalEndGameText();
	}

	public void ResetScore()
	{
		_player1Score = 0;
		_player2Score = 0;
		_hud.UpdateScoreHUD(_player1Score,_player2Score);	
	}

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

	public void InitializeSurvival(
		BallBehavior ball,
		PlayerMovement Paddle,
		HudSurvival hud)
	{
		_ball = ball;
		_playerPaddle = Paddle;
		_hudSurvival = hud;
	}


	

	
	
}
