/// @description Insert description here
// You can write your code in this editor
randomize();



window_set_fullscreen(true);

display_set_gui_size(1920,1080)

w=240;
h=180;

timer=0;

createGameTrigger=true;

playerNumber=instance_number(obj_player);

d=0;

players=[];

for(var i=0;i<playerNumber;i++){
	players[i]=instance_find(obj_player,i);	
}

players=array_shuffle(players);