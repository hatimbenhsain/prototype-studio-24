/// @description Insert description here
// You can write your code in this editor
timer+=delta_time/1000000;

if(timer>1 && timer-delta_time/1000000<=1){
	var resolutionMultiplier=1;
	resolution=resolutionMultiplier*display_get_gui_width()/1920;

	var n=1920/(w*resolutionMultiplier);

	gamesPlacement=[];
	var gamesNumber=floor(n*1080/(h*resolutionMultiplier));

	for(var i=0;i<gamesNumber;i++){
		var xx=(i%n)*w;
		var yy=floor(i/n)*h;
		array_push(gamesPlacement,[xx,yy]);
	}

	gamesPlacement=array_shuffle(gamesPlacement);

	//for(var i=0;i<instance_number(obj_player);i++){
	//	var player=instance_find(obj_player,i);
	//	var game=instance_create_depth(gamesPlacement[i][0],gamesPlacement[i][1],-10000,obj_game);
	//	game.player=player;
	//	game.resolution=resolution;
	//	game.player.game=game;
	//}
	
	show_debug_message(display_get_gui_height());
	
}

if(timer>1 && createGameTrigger){
	var num=instance_number(obj_game);
	if(num<instance_number(obj_player)){
		var player=instance_find(obj_player,num);
		var game=instance_create_depth(gamesPlacement[num][0],gamesPlacement[num][1],-10000,obj_game);
		game.player=player;
		game.resolution=resolution;
		game.player.game=game;
	}
	createGameTrigger=false;
	num+=1;
}