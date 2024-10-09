// Script assets have changed for v2.3.0 see
// https://help.yoyogames.com/hc/en-us/articles/360005277377 for more information
function getColor(x1,y1,x2,y2){
	var d=point_distance(x1,y1,x2,y2);
	var l=(1-clamp((d-minD)/(maxD-minD),0,0.8))*255;
	//var cs=[255,200,180,160,120,60,30];
	//var l=cs[floor(clamp((d-minD)/(maxD-minD),0,1)*(array_length(cs)-1))];
	var col=make_color_rgb(l,l,l);
	return col;
}