class_name PlayerMovement
extends CharacterBody2D

@export var speed: float = 300.0
@export var jump_velocity: float = -400.0
var is_facing_right: bool = true

@export var cayotee_duration: float = 0.2 # seconds
var cayotee_timer: float = 0.0

var knock_velocity: Vector2 = Vector2.ZERO
@export var knock_duration: float = 0.1 # seconds
var knock_timer: float = 0.0

var player_anim: PlayerAnimation

func _ready():
	player_anim = $AnimatedSprite2D

func _physics_process(delta):
	var _velocity = self.velocity

	if not is_on_floor():
		cayotee_timer -= delta
		_velocity += get_gravity() * delta
		# play_jump_anim()
	else:
		cayotee_timer = cayotee_duration

	if Input.is_action_just_pressed("jump_action") and (is_on_floor() or cayotee_timer > 0):
		_velocity.y = jump_velocity

	var direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")

	if knock_timer > 0:
		knock_timer -= delta
	else:
		if direction != Vector2.ZERO:
			_velocity.x = direction.x * speed
			is_facing_right = direction.x > 0
			player_anim.flip_h = not is_facing_right
			player_anim.set_state(PlayerAnimation.playerState.WALK)
		else:
			_velocity.x = move_toward(self.velocity.x, 0, speed)
			player_anim.set_state(PlayerAnimation.playerState.IDLE)

	self.velocity = _velocity
	move_and_slide()

func apply_knockback(source_pos: Vector2, strength: float) -> void:
	var direction = (global_position - source_pos).normalized()
	knock_velocity = direction * strength
	velocity = knock_velocity
	knock_timer = knock_duration


