class_name Breakable
extends Node2D

func destroy() -> void:
    on_destroy()
    queue_free()

func on_destroy() -> void:
    pass
