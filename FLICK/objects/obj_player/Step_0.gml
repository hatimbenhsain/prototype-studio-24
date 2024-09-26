/// @description Insert description here
// You can write your code in this editor
event_inherited();

var moving=false;

var dt=delta_time/1000000;

if(vx!=0) vx-=sign(vx)*decceleration*dt;
if(vy!=0) vy-=sign(vy)*decceleration*dt;

if(keyboard_check(vk_left)){
	vx-=acceleration*dt;	
	moving=true;
	image_xscale=-1;
}
if(keyboard_check(vk_right)){
	vx+=acceleration*dt;	
	moving=true;
	image_xscale=1;
}
if(keyboard_check(vk_up)){
	vy-=acceleration*dt;	
	moving=true;
}
if(keyboard_check(vk_down)){
	vy+=acceleration*dt;	
	moving=true;
}

if(moving){
	sprite_index=spr_player_walk;	
}else{
	sprite_index=spr_player;	
}

vx=clamp(vx,-maxSpeed,maxSpeed);
vy=clamp(vy,-maxSpeed,maxSpeed);

if(abs(vx)>0.1){
	x+=vx;
}
if(abs(vy)>0.1){
	y+=vy;
}

if(keyboard_check_pressed(ord("X"))){
	with(obj_npc){
		if(distance_to_object(obj_player)<64){
			flicked=true;
			flickedTimer=3;
		}
	}
}