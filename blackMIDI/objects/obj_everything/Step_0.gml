/// @description Insert description here
// You can write your code in this editor
timer+=delta_time/1000000;

if(timer>1 && timer-delta_time/1000000<=1){
	var resolutionMultiplier=1;
	resolution=resolutionMultiplier*display_get_gui_width()/1920;

	var n=1920/(w*resolutionMultiplier);

	gamesPlacement=[];
	var gamesNumber=floor(n*1080/(h*resolutionMultiplier));

	for(var i=0;i<=gamesNumber;i++){
		var xx=(i%n)*w;
		var yy=floor(i/n)*h;
		array_push(gamesPlacement,[xx,yy]);
	}
	
	show_debug_message(1080/(h*resolutionMultiplier));
	show_debug_message("games number");
	show_debug_message(gamesNumber);

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

//if(keyboard_check_pressed(ord("Z"))){
//	createGameTrigger=true;	
//}

if(timer>1 && createGameTrigger){
	var num=instance_number(obj_game);
	var s=1;
	var player;
	if(num>=instance_number(obj_player)){
		var p=instance_find(obj_player,(num%playerNumber));
		player=instance_create_depth(p.depth,p.x,p.y,obj_player);
	}else{
		player=instance_find(obj_player,num);
	}
	var xx, yy;
	if(instance_number(obj_game)<array_length(gamesPlacement)){
		xx=gamesPlacement[num][0];
		yy=gamesPlacement[num][1];
	}else{
		show_debug_message("random placement");
		var k=random(array_length(gamesPlacement));
		xx=gamesPlacement[k][0];
		yy=gamesPlacement[k][1];
		var mx=clamp((instance_number(obj_game)-array_length(gamesPlacement))/10,1,8);
		s=ceil(random(mx));
	}
	var game=instance_create_depth(xx,yy,-10000-d,obj_game);
	d++;
		
	if(instance_number(obj_game)>=array_length(gamesPlacement)){
		if(random(1)<0.5){
			game.add=true;	
		}else{
			game.subtract=true;
		}
	}
	game.player=player;
	game.resolution=resolution;
	game.w=game.w*s;
	game.h=game.h*s;
	game.player.game=game;

	createGameTrigger=false;
	num+=1;
	
	show_debug_message(game.x);
	show_debug_message(game.y);
}