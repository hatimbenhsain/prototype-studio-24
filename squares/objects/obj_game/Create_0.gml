spaceBetweenNodes=12;
width=camera_get_view_width(view_camera[0])-spaceBetweenNodes*2;
height=camera_get_view_height(view_camera[0])-spaceBetweenNodes*2;

x=camera_get_view_width(view_camera[0])/2;
y=camera_get_view_height(view_camera[0])/2;

for(var xx=x-width/2;xx<=x+width/2;xx+=spaceBetweenNodes){
	for(var yy=y-height/2;yy<=y+height/2;yy+=spaceBetweenNodes){
		instance_create_depth(xx,yy,depth,obj_node);
	}
}


selectedNode=noone;

walls=[];