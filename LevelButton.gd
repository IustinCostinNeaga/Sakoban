extends Button
class_name LevelButton

@export var lvl: String = "0"

func _on_pressed():
	var path = "res://levels/level_" + lvl + ".tscn"
	print("change to level " + lvl + ". Path is " + path)
	get_tree().change_scene_to_file(path)

