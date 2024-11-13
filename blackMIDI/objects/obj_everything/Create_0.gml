/// @description Insert description here
// You can write your code in this editor
window_set_fullscreen(true);

w=240;
h=160;

resolution=display_get_gui_width()/1920;
show_debug_message(resolution/1920);

var n=1920/w;

for(var i=0;i<instance_number(obj_player);i++){
	var player=instance_find(obj_player,i);
	var game=instance_create_depth((i%n)*w,floor(i/n)*h,-10000,obj_game);
	game.player=player;
	game.resolution=resolution;
	show_debug_message("created game");
	show_debug_message(game.y);
}