using System;
using System.Linq;
using Godot;
using Sokoban.levels;

namespace Sokoban;

public partial class Player : Node2D
{
    private TileMap tilemap;
    private bool canMove = true;

    public override void _Ready()
    {
        tilemap = GetNode<TileMap>("../TileMap");
        GetNode<level_script>("../../Level").Connect(level_script.SignalName.OnWin, new Callable(this, nameof(OnWin)));
    }

    public override void _Process(double delta)
    {
        if(canMove){
            if (Input.IsActionJustPressed("Up"))
            {
                MovePlayer(Vector2I.Up);
            }
            else if (Input.IsActionJustPressed("Down"))
            {
                MovePlayer(Vector2I.Down);
            }
            else if (Input.IsActionJustPressed("Right"))
            {
                MovePlayer(Vector2I.Right);
            }
            else if (Input.IsActionJustPressed("Left"))
            {
                MovePlayer(Vector2I.Left);
            }
        }
    }

    private void MovePlayer(Vector2I direction)
    {
        var nextPosition = tilemap.LocalToMap((Position)) + direction;
        var nextTile = tilemap.GetCellTileData(1, nextPosition);
        if (nextTile.GetCustomData("Walkable").AsBool())
        {
            foreach (var box in GetTree().GetNodesInGroup("Box").Cast<Box>())
            {
                var boxPosition = tilemap.LocalToMap(box.Position);
                if (boxPosition == nextPosition)
                {
                    if (box.move(direction))
                    {
                        Position = tilemap.MapToLocal(nextPosition);
                        return;
                    }
                    return;
                }
            }
            Position = tilemap.MapToLocal(nextPosition);
        }
    }

    public void OnWin()
    {
        canMove = false;
    }
}