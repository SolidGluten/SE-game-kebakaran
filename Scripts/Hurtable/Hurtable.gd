class_name Hurtable
extends Area2D

@export var damage: int = 1
@export var knockback_force: float = 300.0
@export var put_on_fire: bool = false

func _ready() -> void:
    body_entered.connect(self._on_body_entered)

func _on_body_entered(body: Node2D) -> void:
    if body is PlayerMovement:
        playerHealth.hurt(damage)
        body.apply_knockback(global_position, knockback_force)
        if put_on_fire:
            playerHealth.is_on_fire = true
