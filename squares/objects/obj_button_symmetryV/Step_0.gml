/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event
event_inherited();

if(active && !prevActive){
	instance_create_depth(0,0,depth,obj_follower_mirrorV);	
}else if(!active && prevActive){
	instance_destroy(obj_follower_mirrorV);	
}