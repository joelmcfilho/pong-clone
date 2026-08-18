using System.Threading.Tasks;
using Godot;


public partial class Commands : Control
{
    private GameManager gm;

    

    public override void _Ready()
    {
        gm = GetNode<GameManager>("/root/GameManager");

    }

}