/// @description Insert description here
// You can write your code in this editor

	if(speedValue>=0.01 && (current_time/1000)%(maxImgSpeed*4)<=maxImgSpeed*2){
		offsetY=1;	
	}else{
		offsetY=0;	
	}

draw_sprite_ext(sprite_index,image_index,x,y+offsetY,image_xscale,image_yscale,image_angle,c_white,1);