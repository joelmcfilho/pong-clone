using Godot;


public partial class MainMenu : Control
{
    private CenterContainer _mainMenu;
    private CenterContainer _classicMenu;
    private CenterContainer _survivalMenu;
    private Label _classicMenuText;
    private Label _survivalMenuText;


    private GameManager _gm;

    public override void _Ready()
    {
        _mainMenu = GetNode<CenterContainer>("MainMenu");
        _classicMenu = GetNode<CenterContainer>("ClassicModeMenu");
        _survivalMenu = GetNode<CenterContainer>("SurvivalModeMenu");
        _gm = GetNode<GameManager>("/root/GameManager");
        _classicMenuText = GetNode<Label>("ClassicMenuText");
        _survivalMenuText = GetNode<Label>("SurvivalMenuText");

        _mainMenu.Visible = true;
        _classicMenu.Visible = false;
        _survivalMenu.Visible = false;
        _classicMenuText.Visible = false;

    }

    //Main Menu Interface
    public void ClassicButtonPressed()
    {
        _mainMenu.Visible= false;
        _classicMenu.Visible = true;
        _classicMenuText.Visible = true;
    }

    public void SurvivalButtonPressed()
    {
        _mainMenu.Visible = false;
        _survivalMenuText.Visible = true;
        _survivalMenu.Visible = true;
    }

    public void QuitButtonPressed()
    {
        GetTree().Quit();
    }

    //Classic Menu Interface
    public void PlayerPlayerPressed()
    {
        _gm.gameModeSelect = GameMode.Multi;
        _mainMenu.Visible= false;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
        GetTree().ChangeSceneToFile("res://game.tscn");
    }

    public void PlayerCPUPressed()
    {
        _gm.gameModeSelect = GameMode.Single;
        _mainMenu.Visible= false;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
        GetTree().ChangeSceneToFile("res://game.tscn");
    }
    public void ReturnModeToMainPressed()
    {
        _mainMenu.Visible= true;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
    }

    //Survival Menu Interface
    public void SurvivalStartButtonPressed()
    {
        _gm.gameModeSelect = GameMode.Survival;
        _survivalMenu.Visible = false;
        _survivalMenuText.Visible = false;
        GetTree().ChangeSceneToFile("res://survival.tscn");
    }

    public void HiScoreSurvivalButtonPressed()
    {
        
    }
    
    public void ReturnSurvivalToMain()
    {
        _survivalMenuText.Visible = false;
        _survivalMenu.Visible = false;
        _mainMenu.Visible = true;
    }

    
    


    
}
