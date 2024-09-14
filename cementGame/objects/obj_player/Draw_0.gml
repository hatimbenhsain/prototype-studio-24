/// @description Insert description here
// You can write your code in this editor
if((current_time/1000)%(maxImgSpeed*4)<=maxImgSpeed*2){
	offsetY=1;	
}else{
	offsetY=0;	
}

draw_sprite(spr_screen,0,x,y);
draw_sprite_ext(sprite_index,image_index,x+offsetX,y+offsetY,image_xscale,image_yscale,direction,c_white,1);