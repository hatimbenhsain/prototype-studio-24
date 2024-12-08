/// @description Insert description here
// You can write your code in this editor
var res=obj_game.resolution
draw_set_color(c_white);
draw_set_font(font_default)
if(active){
	draw_set_alpha(1)
}else{
	draw_set_alpha(0.5)
}
draw_rectangle(x*res,y*res,(x+obj_game.nodeW)*res,(y+obj_game.nodeH)*res,true)
draw_set_valign(fa_center);
draw_set_halign(fa_center);
draw_text(res*x+obj_game.nodeW/2,res*y+obj_game.nodeH/2,value)