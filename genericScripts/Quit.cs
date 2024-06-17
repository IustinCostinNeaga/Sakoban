using Godot;

namespace Sokoban.genericScripts;

public partial class Quit : Node
{
	public void OnPressed()
	{
		GetTree().Quit();
	}
}