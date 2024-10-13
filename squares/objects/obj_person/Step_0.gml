/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event
event_inherited();

var inst=instance_place(x,y,obj_person);

if(inst!=noone && distance_to_point(inst.x,inst.y)<=2){
	audio_play_sound(snd_love,1,false);
	instance_create_depth((x+inst.x)/2,(y+inst.y)/2,depth,obj_love);
	instance_destroy(inst);	
	instance_destroy(self);	
}