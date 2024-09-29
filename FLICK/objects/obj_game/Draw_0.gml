/// @description Insert description here
// You can write your code in this editor

if(showingDialogue){
	var cx=camera_get_view_x(view_camera[0]);
	var cy=camera_get_view_y(view_camera[0]);
	x=cx+186;
	y=cy+284;
	draw_sprite(sprite_index,image_index,x,y);
	draw_set_font(font_1);
	draw_set_color(c_black);
	var xx=cx+55+offsetX;
	var yy=cy+383+offsetY;
	var sep=25;
	var w=290;
	if(is_string(dialogue[textIndex]) || tempText!=""){
		var text;
		if(tempText!=""){
			text=string_upper(string_copy(tempText,0,charIndex));
		}else{
			text=string_upper(string_copy(dialogue[textIndex],0,charIndex));
		}
		draw_text_ext(xx,yy,text,sep,w);
	}else{
		draw_set_font(font_2);
		for(var i=0;i<array_length(dialogue[textIndex]);i++){
			var text=string_upper(dialogue[textIndex][i]);
			var ox=0;
			var oy=0;
			if(flicked && optionSelected==i && flickTime>=0.2){
				ox=floor(random(5))-2;	
				oy=floor(random(3))-2;
			}
			draw_text_ext(xx+ox,yy+sep*i+oy,text,sep,w);	
		}
		draw_sprite(spr_uiFlick,handImageIndex,xx-25,yy+10+optionSelected*sep);
	}
	draw_set_color(c_white);
	
}