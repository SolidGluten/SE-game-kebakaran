class_name PlayerHealth
extends Node

@export var max_health: int = 3
@export var current_health: int = 0
@export var is_on_fire: bool = true
@export var is_rolling: bool = false

@export var fire_damage: int = 1
@export var fire_damage_interval: float = 3 # seconds
var fire_damage_timer: float = 0.0

@export var roll_duration: float = 4.0 # seconds
var roll_timer: float = 0.0
var roll_speed_multiplier: float = 0.4

signal health_changed(hp: int)
signal dead

func _ready() -> void:
    current_health = max_health
    fire_damage_timer = fire_damage_interval

func _physics_process(delta: float) -> void:
    print(roll_timer)

    if is_on_fire:
        fire_damage_timer -= delta
    else:
        fire_damage_timer = fire_damage_interval

    # apply fire damage
    if fire_damage_timer <= 0:
        hurt(fire_damage)
        fire_damage_timer = fire_damage_interval

    # handle rolling
    if is_rolling:
        roll_timer -= delta
        if roll_timer <= 0:
            is_rolling = false
            is_on_fire = false

    if Input.is_action_just_pressed("duck_roll") and is_on_fire:
        if is_rolling:
            roll_timer -= roll_speed_multiplier
            return  # already rolling, ignore input
        
        is_rolling = true
        roll_timer = roll_duration
        
func hurt(dmg: int) -> void:
    current_health = max(0, current_health - dmg)
    emit_signal("health_changed", current_health)

    if current_health <= 0:
        emit_signal("dead")
        current_health = max_health

func heal(hp: int) -> void:
    current_health = min(max_health, current_health + hp)
    emit_signal("health_changed", current_health)
