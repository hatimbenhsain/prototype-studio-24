/// @description Insert description here
// You can write your code in this editor
event_inherited();

//var inst=instance_place(x,y,obj_love);

//if(inst!=noone && distance_to_point(inst.x,inst.y)<=0){
//	audio_play_sound(snd_hurt,1,false,1,0,0.2);
//	instance_destroy(inst);	
//}

if(spd>5){
	image_blend=merge_color(c_white,$ffccdd,clamp((spd-5)/2.5,0,1));
}