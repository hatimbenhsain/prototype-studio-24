/// @description Insert description here
// You can write your code in this editor
randomise();

acceleration=40;
maxSpeed=4;
vx=0
vy=0;

decceleration=25;

cameraSpeed=10;

camera_set_view_pos(view_camera[0],x-camera_get_view_width(view_camera[0])/2,y-camera_get_view_height(view_camera[0])/2);

prevLocationX=x;
prevLocationY=y;

encounters=[];

function generateEncounters(){
	encounters=[obj_candelabra,obj_npc,obj_lance,obj_npc,obj_graal,obj_npc,obj_npc];
	encounters=array_shuffle(encounters);

	array_push(encounters,obj_button);
	array_push(encounters,obj_nothing);
	
	encounterIndex=0;
}

minDistance=600;
distanceVar=800;
setDistance=minDistance+random(distanceVar);
createDistance=300;

encounterIndex=0;

creations=[];