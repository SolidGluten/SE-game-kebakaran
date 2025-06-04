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

var animated_sprite: AnimatedSprite2D

func _ready():
	animated_sprite = $AnimatedSprite2D

func _physics_process(delta):
	var _velocity = self.velocity

	if not is_on_floor():
		cayotee_timer -= delta
		_velocity += get_gravity() * delta
		play_jump_anim()
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
			animated_sprite.flip_h = not is_facing_right
			play_walk_anim()
		else:
			_velocity.x = move_toward(self.velocity.x, 0, speed)
			play_idle_anim()

	self.velocity = velocity
	move_and_slide()

func apply_knockback(source_pos: Vector2, strength: float) -> void:
	var direction = (global_position - source_pos).normalized()
	knock_velocity = direction * strength
	velocity = knock_velocity
	knock_timer = knock_duration

func play_walk_anim():
	if not is_on_floor():
		return

	var item = PlayerInventory.get_current_item()
	match item:
		PlayerInventory.ItemTypes.FireAxe:
			animated_sprite.play("walk_axe")
		PlayerInventory.ItemTypes.WetCloth:
			animated_sprite.play("walk_cloth")
		PlayerInventory.ItemTypes.FireExtinguisher:
			animated_sprite.play("walk_exting")
		_:
			animated_sprite.play("walk")

func play_idle_anim():
	if not is_on_floor():
		return

	var item = PlayerInventory.get_current_item()
	match item:
		PlayerInventory.ItemTypes.FireAxe:
			animated_sprite.play("idle_axe")
		PlayerInventory.ItemTypes.WetCloth:
			animated_sprite.play("idle_cloth")
		PlayerInventory.ItemTypes.FireExtinguisher:
			animated_sprite.play("idle_exting")
		_:
			animated_sprite.play("idle")

func play_jump_anim():
	var item = PlayerInventory.get_current_item()
	match item:
		PlayerInventory.ItemTypes.FireAxe:
			animated_sprite.play("jump_axe")
		PlayerInventory.ItemTypes.WetCloth:
			animated_sprite.play("jump_cloth")
		PlayerInventory.ItemTypes.FireExtinguisher:
			animated_sprite.play("jump_exting")
		_:
			animated_sprite.play("jump")
