using Godot;

namespace Sokoban.genericScripts;

public partial class ChangeSceneButton : Button
{
	[Export] private string _sceneName;
	private void OnPressed()
	{
		GetTree().ChangeSceneToFile($"res://{_sceneName}.tscn");
	}
}