/// @description Insert description here
// You can write your code in this editor

if surface_exists(surf)
{
	shader_set(sh_replaceColor);
	
	var r=0.2;
	
	if(add || subtract){
		r=0.5	
	}
	
	shader_set_uniform_f(sh_handle_match1,96/255,48/255,32/255);
	shader_set_uniform_f(sh_handle_replace1,color1R,color1G,color1B);
	shader_set_uniform_f(sh_handle_match2,248/255,168/255,40/255);
	shader_set_uniform_f(sh_handle_replace2,color2R,color2G,color2B);
	shader_set_uniform_f(sh_handle_range,r);
	
	if(!add && !subtract)
    draw_surface_stretched(surf,x*resolution,y*resolution,w*resolution,h*resolution);
	else{
		if(add)	gpu_set_blendmode(bm_add);
		else gpu_set_blendmode(bm_subtract);
		draw_surface_stretched(surf,(x-w/2)*resolution,(y-h/2)*resolution,w*2*resolution,h*2*resolution);
	}
	gpu_set_blendmode(bm_normal)
	shader_reset();
	//draw_rectangle_color(x*resolution,y*resolution,x*resolution+240*resolution,y*resolution+160*resolution,c_black,c_black,c_black,c_black,true);
}

