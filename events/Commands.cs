using System.Threading.Tasks;
using Godot;


public partial class Commands : Control
{
    private GameManager gm;

    

    public override void _Ready()
    {
        gm = GetNode<GameManager>("/root/GameManager");

    }


    //Metodos de Debug, remover na versão final
    public async Task DebugWin(Side side)
    {
        if(side == Side.Player)
        {
            await gm.EndGame(Side.Player);
        }
        if(side == Side.AI)
        {
            await gm.EndGame(Side.AI);
        }
    }

    
}