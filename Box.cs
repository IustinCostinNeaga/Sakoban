using Godot;
using System;
using System.Linq;

public partial class Box : Node2D
{

	[Signal]
	public delegate Signal HasEnteredTheGoalEventHandler();
	
	private TileMap tileMap;
	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("../TileMap");
	}

	public bool move(Vector2I direction)
	{
		var nextPosition = tileMap.LocalToMap((Position)) + direction;
		var nextTile = tileMap.GetCellTileData(0, nextPosition);
		if (nextTile.GetCustomData("Walkable").AsBool())
		{
			foreach (var box in GetTree().GetNodesInGroup("Box").Cast<Box>())
			{
				var boxPosition = tileMap.LocalToMap(box.Position);
				if (boxPosition == nextPosition)
				{
					return false;
				}
			}
			
			Position = tileMap.MapToLocal(nextPosition);
			if (nextTile.GetCustomData("Boxable").AsBool())
			{
				EmitSignal(SignalName.HasEnteredTheGoal);
			}
			return true;
		}

		return false;
	}
}
