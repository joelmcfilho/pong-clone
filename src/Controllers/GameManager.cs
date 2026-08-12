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
	private Commands _cmds;
	public bool isCountdownActive {get;set;}

	public GameMode gameModeSelect;


	public override async void _Process(double delta)
    {
        //DEBUG -- Retirar na versão final
		if (Input.IsKeyPressed(Key.K))
		{
			await EndGame(Side.Player);
		}

		if (Input.IsKeyPressed(Key.L))
		{
			await EndGame(Side.AI);
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
			await EndGame(Side.Player);
			_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
			_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
			return;
		}
		else if(_player2Score == 3)
		{
			if(side == Side.Player2)
			{
				await EndGame(Side.Player2);
				_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
				_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
				return;
			}
			else if(side == Side.AI)
			{
				await EndGame(Side.AI);
				_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
				_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
				return;
			}
			
		}	

		// Continue Game, if endgame conditions are not made, the game will reset and continues

		await _hud.ShowPointSign();
		_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
		_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);
		await _ball.ResetBall();

	}

	public async Task EndGame(Side side)
	{
		await _hud.ShowWinner(side);
		
		_player1Score = 0;
		_player2Score = 0;
		_hud.UpdateScoreHUD(_player1Score,_player2Score);		

		await _ball.ResetBall();
		_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
		_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);

		
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


	

	
	
}
