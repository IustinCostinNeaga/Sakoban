extends Button
class_name LevelButton

@export var lvl: String = "0"

func _on_pressed():
	print("change to level " + lvl)
	#var level_scene = preload("res://levels/level" + lvl + ".tscn").instantiate()
	#add_child(level_scene)
