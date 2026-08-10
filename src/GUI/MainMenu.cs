using Godot;


public partial class MainMenu : Control
{
    private CenterContainer _mainMenu;
    private CenterContainer _modeMenu;
    private GameManager _gm;

    public override void _Ready()
    {
        _mainMenu = GetNode<CenterContainer>("MainMenu");
        _modeMenu = GetNode<CenterContainer>("ModeMenu");

        _mainMenu.Visible = true;
        _modeMenu.Visible = false;

    }

    //Main Menu Interface
    public void PlayButtonPressed()
    {
        _mainMenu.Visible= false;
        _modeMenu.Visible = true;
    }

    public void QuitButtonPressed()
    {
        GetTree().Quit();
    }

    //Mode Menu Interface
    public void PlayerPlayerPressed()
    {
        
    }

    public void PlayerCPUPressed()
    {
        _mainMenu.Visible= false;
        _modeMenu.Visible = false;
        GetTree().ChangeSceneToFile("res://game.tscn");
    }
    public void ReturnModeToMainPressed()
    {
        _mainMenu.Visible= true;
        _modeMenu.Visible = false;
    }


    
}
