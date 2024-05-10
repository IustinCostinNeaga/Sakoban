extends Node2D

@onready var tile_map = $"../TileMap"
@export var let_move: bool
@export var boxes: Array[Node2D]

func _process(_delta):
	if(let_move):
		if(Input.is_action_just_pressed("Su")): move(Vector2.UP)
		elif(Input.is_action_just_pressed("Giu")): move(Vector2.DOWN)
		elif(Input.is_action_just_pressed("Destra")): move(Vector2.RIGHT)
		elif(Input.is_action_just_pressed("Sinistra")): move(Vector2.LEFT)

func can_move(direction: Vector2):
	var current_postion = tile_map.local_to_map(global_position)
	var next_postion = Vector2(
			current_postion.x + direction.x, 
			current_postion.y + direction.y)
		
	var next_tile: TileData = tile_map.get_cell_tile_data(0, next_postion)
	
	if(next_tile.get_custom_data("Walkable") == false):
		return false
	return true

func move(direction: Vector2):
	var current_postion = tile_map.local_to_map(global_position)
	var next_position = Vector2(
			current_postion.x + direction.x, 
			current_postion.y + direction.y)
		
	var next_tile: TileData = tile_map.get_cell_tile_data(0, next_position)
	
	for box in boxes:
		if(tile_map.local_to_map(box.global_position).x == next_position.x 
		&& tile_map.local_to_map(box.global_position).y == next_position.y):
			if(box.can_move(direction)):
				box.move(direction)
			else:
				return
	
	if(next_tile.get_custom_data("Walkable") == false):
		return
		
	global_position = tile_map.map_to_local(next_position)

	return
