/// @description Insert description here
// You can write your code in this editor
var col=c_gray;

if(selected){
	var x2=maker.x;
	var y2=maker.y;
	var dist=point_distance(x,y,maker.x,maker.y);
	if(dist>maxD){
		x2=x+(x2-x)*maxD/dist;	
		y2=y+(y2-y)*maxD/dist;	
	}
	col=getColor(x,y,x2,y2);
	draw_line_color(x,y,x2,y2,col,col);	
	col=c_white;
}else{
	col=c_gray;
}

draw_sprite_ext(spr_node,image_index,x,y,image_xscale,image_yscale,0,col,image_alpha);

for(var i=0;i<array_length(links);i++){
	col=getColor(x,y,links[i].x,links[i].y);
	draw_line_color(x,y,links[i].x,links[i].y,col,col);	
}

