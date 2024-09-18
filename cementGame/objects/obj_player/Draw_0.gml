/// @description Insert description here
// You can write your code in this editor
if(currentState=STATES.SWIMMING){
	if((current_time/1000)%(maxImgSpeed*4)<=maxImgSpeed*2){
		offsetY=1;	
	}else{
		offsetY=0;	
	}
}

//draw_sprite(spr_screen,0,x,y);
var scHeight=sprite_get_height(spr_screen);
var scCrop=min(max(0,scHeight/2+(y-2912)),scHeight);
draw_sprite_part_ext(spr_screen,0,0,scHeight-scCrop,sprite_get_width(spr_screen),scHeight,x-sprite_get_width(spr_screen)/2,y-sprite_get_height(spr_screen)/2+scHeight-scCrop,1,1,c_white,1);
draw_sprite_ext(sprite_index,image_index,x+offsetX,y+offsetY,image_xscale,image_yscale,image_angle,c_white,1);