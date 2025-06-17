extends Node2D

enum ItemTypes {
	NONE,
	FIRE_AXE,
	WET_CLOTH,
	FIRE_EXTINGUISHER
}

signal item_added(item)
signal item_resetted
# signal item_changed(item)

var item_list = {
	ItemTypes.FIRE_AXE: false,
	ItemTypes.WET_CLOTH: false,
	ItemTypes.FIRE_EXTINGUISHER: false
}

var current_item_type = ItemTypes.NONE
var current_item = null

func _ready():
	on_ready()

func on_ready():
	reset_item()
	print(item_list)
	current_item_type = ItemTypes.NONE

func _process(_delta):
	if Input.is_action_just_pressed("switch_axe"):
		change_item(ItemTypes.FIRE_AXE)
	elif Input.is_action_just_pressed("switch_extinguisher"):
		change_item(ItemTypes.FIRE_EXTINGUISHER)
	elif Input.is_action_just_pressed("switch_cloth"):
		change_item(ItemTypes.WET_CLOTH)

func get_current_item():
	return current_item_type

func add_item(item):
	item_list[item] = true
	emit_signal("item_added", item)
	change_item(item)

func use_item(item):
	if not item_list[item]:
		return
	item_list[item] = false
	if current_item:
		current_item.use()

func change_item(item):
	if not item_list[item]:
		return
	current_item_type = item

func reset_item():
	for key in item_list.keys():
		item_list[key] = false
	emit_signal("item_resetted")
