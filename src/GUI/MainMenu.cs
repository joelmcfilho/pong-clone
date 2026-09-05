using Godot;


public partial class MainMenu : Control
{
    private CenterContainer _mainMenu;
    private CenterContainer _classicMenu;
    private CenterContainer _survivalMenu;
    private Label _classicMenuText;
    private Label _survivalMenuText;


    private GameManager _gm;
    private AudioManager _am;

    public override void _Ready()
    {
        _mainMenu = GetNode<CenterContainer>("MainMenu");
        _classicMenu = GetNode<CenterContainer>("ClassicModeMenu");
        _survivalMenu = GetNode<CenterContainer>("SurvivalModeMenu");
        _gm = GetNode<GameManager>("/root/GameManager");
        _am = GetNode<AudioManager>("/root/AudioManager");
        _classicMenuText = GetNode<Label>("ClassicMenuText");
        _survivalMenuText = GetNode<Label>("SurvivalMenuText");

        _mainMenu.Visible = true;
        _classicMenu.Visible = false;
        _survivalMenu.Visible = false;
        _classicMenuText.Visible = false;

        _gm.gameModeSelect = GameMode.None;
        _am.PlayMusic(GD.Load<AudioStream>("res://assets/sounds/Music/mainmenu.mp3"));

    }

    //Main Menu Interface
    public void ClassicButtonPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _mainMenu.Visible= false;
        _classicMenu.Visible = true;
        _classicMenuText.Visible = true;
    }

    public void SurvivalButtonPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _mainMenu.Visible = false;
        _survivalMenuText.Visible = true;
        _survivalMenu.Visible = true;
    }

    public void QuitButtonPressed()
    { 
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        GetTree().Quit();
    }

    //Classic Menu Interface
    public void PlayerPlayerPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _gm.gameModeSelect = GameMode.Multi;
        _mainMenu.Visible= false;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
        _am.StopMusic();
        GetTree().ChangeSceneToFile("res://game.tscn");
    }

    public void PlayerCPUPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _gm.gameModeSelect = GameMode.Single;
        _mainMenu.Visible= false;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
        _am.StopMusic();
        GetTree().ChangeSceneToFile("res://game.tscn");
    }
    public void ReturnModeToMainPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _mainMenu.Visible= true;
        _classicMenu.Visible = false;
        _classicMenuText.Visible = false;
    }

    //Survival Menu Interface
    public void SurvivalStartButtonPressed()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _gm.gameModeSelect = GameMode.Survival;
        _survivalMenu.Visible = false;
        _survivalMenuText.Visible = false;
        _am.StopMusic();
        GetTree().ChangeSceneToFile("res://survival.tscn");
    }

    public void HiScoreSurvivalButtonPressed()
    {
        
    }
    
    public void ReturnSurvivalToMain()
    {
        _am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav"));
        _survivalMenuText.Visible = false;
        _survivalMenu.Visible = false;
        _mainMenu.Visible = true;
    }

    
    


    
}
