using System.Threading.Tasks;
using Godot;

public partial class BallBehavior : CharacterBody2D
{
	[Export] public float Speed = 300.0f;

	public Vector2 _direction = Vector2.Zero;
	private Vector2 _spawnPosition;

	private Hud _hud;

	public async override void _Ready()
	{
		_hud = GetNode<Hud>("../Hud");

		_spawnPosition = GlobalPosition;


	}

	public void StartBall()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();

		float x = GD.Randf() < 0.5f ? -1 : 1;
		float y = rng.RandfRange(-0.5f,0.5f);

		_direction = new Vector2(x,y).Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = _direction*Speed;

		var collision = MoveAndCollide(_direction*Speed*(float)delta);

		if(collision != null)
		{
			HandleCollisions(collision);
		}
	}

	private void HandleCollisions(KinematicCollision2D collision)
	{

		if(collision.GetCollider() is Paddle paddle)
		{
			float offset = (GlobalPosition.Y - paddle.Position.Y)/50f;

			_direction = new Vector2(-_direction.X,offset);
		}
		else
		{
			_direction = _direction.Bounce(collision.GetNormal());
		}
		

	} 

	public async Task ResetBall()
	{	

		GlobalPosition = _spawnPosition;
		_direction = Vector2.Zero;

		await _hud.Countdown();

		StartBall();
		
	}
}
