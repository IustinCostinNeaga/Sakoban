using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Sokoban.levels;

public partial class level_script : Node
{
	[Signal]
	public delegate void OnWinEventHandler();
	[Export] public PackedScene WinScreenScene { get; set; }

	private TileMap tileMap;
	private IEnumerable<Box> boxes;
	private WinScreen win;

	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("TileMap");
		boxes = GetTree().GetNodesInGroup("Box").Cast<Box>();
		foreach (var box in boxes)
		{
			box.Connect(Box.SignalName.HasEnteredTheGoal, new Callable(this, nameof(OnBoxEnteredGoal)));
		}
	}

	private void OnBoxEnteredGoal()
	{
		GD.Print("Got the signal for the goal");
		foreach (var box in boxes)
		{
			GD.Print($"Box {tileMap.LocalToMap(box.Position)} Tile {tileMap}");
			var pos = tileMap.LocalToMap(box.Position);
			var tile = tileMap.GetCellTileData(1, pos);

			if (!(tile.GetCustomData("Boxable").AsBool())) return;
		}

		EmitSignal(SignalName.OnWin);
		GD.Print("Sent the OnWin signal");
        win = WinScreenScene.Instantiate() as WinScreen;
        win.Position = GetNode<Node2D>("Player").Position - win.Size * 0.10f;
		AddChild(win);

	}
	
}