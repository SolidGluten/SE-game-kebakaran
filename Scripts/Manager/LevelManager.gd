extends Node

# func _ready():
#     playerHealth.connect("dead", self.reset_current_scene)

func _process(_delta):
    if Input.is_action_just_pressed("dev_reset"):
        reset_current_scene()

func reset_current_scene():
    get_tree().reload_current_scene()

func load_level(level_path):
    var scene = load(level_path)
    if scene and scene is PackedScene:
        get_tree().change_scene_to_packed(scene)
    else:
        printerr("Failed to load level at: %s" % level_path)
