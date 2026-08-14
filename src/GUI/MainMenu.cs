using Godot;


public partial class MainMenu : Control
{
    private CenterContainer _mainMenu;
    private CenterContainer _modeMenu;
    private Label _modeMenuText;
    private GameManager _gm;

    public override void _Ready()
    {
        _mainMenu = GetNode<CenterContainer>("MainMenu");
        _modeMenu = GetNode<CenterContainer>("ModeMenu");
        _gm = GetNode<GameManager>("/root/GameManager");
        _modeMenuText = GetNode<Label>("ModeMenuText");

        _mainMenu.Visible = true;
        _modeMenu.Visible = false;
        _modeMenuText.Visible = false;

    }

    //Main Menu Interface
    public void PlayButtonPressed()
    {
        _mainMenu.Visible= false;
        _modeMenu.Visible = true;
        _modeMenuText.Visible = true;
    }

    public void QuitButtonPressed()
    {
        GetTree().Quit();
    }

    //Mode Menu Interface
    public void PlayerPlayerPressed()
    {
        _gm.gameModeSelect = GameMode.Multi;
        _mainMenu.Visible= false;
        _modeMenu.Visible = false;
        _modeMenuText.Visible = false;
        GetTree().ChangeSceneToFile("res://game.tscn");
    }

    public void PlayerCPUPressed()
    {
        _gm.gameModeSelect = GameMode.Single;
        _mainMenu.Visible= false;
        _modeMenu.Visible = false;
        _modeMenuText.Visible = false;
        GetTree().ChangeSceneToFile("res://game.tscn");
    }
    public void ReturnModeToMainPressed()
    {
        _mainMenu.Visible= true;
        _modeMenu.Visible = false;
        _modeMenuText.Visible = false;
    }


    
}
