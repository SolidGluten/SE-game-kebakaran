class_name SmokeDetector
extends Area2D


@export var hurt_timer: float = 1.0
var current_time: float = 0.0
var start_timer: bool = false

signal enter_smoke
signal exit_smoke

func _ready():
    current_time = hurt_timer
    connect("body_entered", self._on_body_entered)
    connect("body_exited", self._on_body_exited)

func _process(delta):
    if start_timer and playerInventory.get_current_item() != playerInventory.ItemTypes.WET_CLOTH:
        if current_time > 0:
            current_time -= delta
        else:
            playerHealth.hurt(1)
            current_time = hurt_timer
    else:
        current_time = hurt_timer

func _on_body_entered(node):
    if node is TileMapLayer and node.name == "SmokeLayer":
        emit_signal("enter_smoke")
        print("In Smoke")
        start_timer = true

func _on_body_exited(node):
    if node is TileMapLayer and node.name == "SmokeLayer":
        emit_signal("exit_smoke")
        print("Out Smoke")
        start_timer = false
