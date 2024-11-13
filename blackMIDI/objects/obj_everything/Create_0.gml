/// @description Insert description here
// You can write your code in this editor
window_set_fullscreen(true);

w=240;
h=180;

resolutionMultiplier=2;
resolution=resolutionMultiplier*display_get_gui_width()/1920;

var n=1920/(w*resolutionMultiplier);

gamesPlacement=[];
gamesNumber=floor(n*1080/(h*resolutionMultiplier));
show_debug_message("Resolution "+string(gamesNumber));

for(var i=0;i<gamesNumber;i++){
	var xx=(i%n)*w;
	var yy=floor(i/n)*h;
	array_push(gamesPlacement,[xx,yy]);
}

gamesPlacement=array_shuffle(gamesPlacement);

for(var i=0;i<instance_number(obj_player);i++){
	var player=instance_find(obj_player,i);
	var game=instance_create_depth(gamesPlacement[i][0],gamesPlacement[i][1],-10000,obj_game);
	game.player=player;
	game.resolution=resolution;
	show_debug_message(game.y);
}

