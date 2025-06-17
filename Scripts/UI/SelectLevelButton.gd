class_name SelectLevelButton
extends Button

@export var level_path = ""
@export var locked: bool = false

func _ready() -> void:
    disabled = locked
    button_down.connect(self._on_button_pressed)
    mouse_entered.connect(self._on_mouse_entered)
    mouse_exited.connect(self._on_mouse_exited)

func _on_button_pressed() -> void:
    if level_path: levelManager.load_level(level_path)


func _on_mouse_entered():
    mouse_default_cursor_shape = Control.CURSOR_POINTING_HAND

func _on_mouse_exited():
    mouse_default_cursor_shape = Control.CURSOR_ARROW


