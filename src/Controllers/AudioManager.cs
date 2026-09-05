using Godot;

public partial class AudioManager : Node
{
    private AudioStreamPlayer _musicPlayer;
    private AudioStreamPlayer _SFXPlayer;

    public override void _Ready()
    {
        _musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");
        _SFXPlayer = GetNode<AudioStreamPlayer>("SFXPlayer");
    }

    public void PlayMusic(AudioStream music)
    {
        if(music == null) return;
        
        if(_musicPlayer.Stream == music && _musicPlayer.Playing) return;

        _musicPlayer.Stop();

        _musicPlayer.Stream = music;
        _musicPlayer.Play();
    }

    public void PlaySFX(AudioStream sfx)
    {
        _SFXPlayer.Stream = sfx;
        _SFXPlayer.Play();
    }

    public void StopMusic()
    {
        _musicPlayer.Stop();
    }
}

