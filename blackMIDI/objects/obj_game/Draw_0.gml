/// @description Insert description here
// You can write your code in this editor


if !surface_exists(surf)
{
    surf = surface_create(w,h);
}

surface_copy(surf, -player.x+w/2, -player.y+h/2, application_surface);

if(combatMode && monster!=-1){
	surface_set_target(surf);
	draw_clear_alpha(c_black,0);
	draw_sprite(monster.sprite_index,monster.image_index,0,0);
	draw_set_color(c_white);
	var hpBarLength=100;
	var hpBarHeight=6;
	var hpBarX=w/2;
	var hpBarY=8;
	draw_rectangle(hpBarX-hpBarLength/2,hpBarY-hpBarHeight/2,hpBarX+hpBarLength/2,hpBarY+hpBarHeight/2,true);
	draw_rectangle(hpBarX-hpBarLength/2+2,hpBarY-hpBarHeight/2+2,
	hpBarX-hpBarLength/2+2+(hpBarLength-4)*monster.hp/monster.maxHP,hpBarY+hpBarHeight/2-2,false);
	
	hpBarX=w/2;
	hpBarY=h-8;
	draw_rectangle(hpBarX-hpBarLength/2,hpBarY-hpBarHeight/2,hpBarX+hpBarLength/2,hpBarY+hpBarHeight/2,true);
	draw_rectangle(hpBarX-hpBarLength/2+2,hpBarY-hpBarHeight/2+2,
	hpBarX-hpBarLength/2+2+(hpBarLength-4)*player.hp/player.maxHP,hpBarY+hpBarHeight/2-2,false);
	
	if(inMenu){
		var boxX=w/2;
		var boxY=50;
		var boxW=64;
		var boxH=32;
		draw_set_halign(fa_center);
		draw_set_valign(fa_center);
	
		draw_set_alpha(.5);
		draw_set_font(font_8bit);	
		if(optionSelected==0){
			draw_set_alpha(1);	
			draw_set_font(font_8bit_big);
			boxW+=2;
			boxH+=2;
		}
		draw_rectangle(boxX-boxW/2,boxY-boxH/2,boxX+boxW/2,boxY+boxH/2,true);
		draw_text(boxX,boxY,"FIGHT!");
		boxX=w/2;
		boxY=90;
		draw_set_alpha(.5);
		draw_set_font(font_8bit);	
		if(optionSelected==1){
			draw_set_alpha(1);	
			draw_set_font(font_8bit_big);
			boxW+=2;
			boxH+=2;
		}
		draw_rectangle(boxX-boxW/2,boxY-boxH/2,boxX+boxW/2,boxY+boxH/2,true);
		draw_text(boxX,boxY,"WAIT!");
		boxX=w/2;
		boxY=130;
		draw_set_alpha(0.5);
		draw_set_font(font_8bit);	
		if(optionSelected==2){
			draw_set_alpha(1);	
			draw_set_font(font_8bit_big);
			boxW+=2;
			boxH+=2;
		}
		draw_rectangle(boxX-boxW/2,boxY-boxH/2,boxX+boxW/2,boxY+boxH/2,true);
		draw_text(boxX,boxY,"RUN!");
	}
	
	draw_set_alpha(1);
	surface_reset_target();
}