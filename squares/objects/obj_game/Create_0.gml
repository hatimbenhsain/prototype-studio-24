spaceBetweenNodes=10;
width=256-spaceBetweenNodes*2;
height=camera_get_view_height(view_camera[0])-spaceBetweenNodes*2;

x=camera_get_view_width(view_camera[0])/2;
y=camera_get_view_height(view_camera[0])/2;

for(var xx=x-width/2+1;xx<=x+width/2;xx+=spaceBetweenNodes){
	for(var yy=y-height/2+1;yy<=y+height/2;yy+=spaceBetweenNodes){
		instance_create_depth(xx,yy,depth,obj_node);
	}
}

var xx=width/2+spaceBetweenNodes*4;
for(var yy=y-height/2+1;yy<=y+height/2-spaceBetweenNodes;yy+=spaceBetweenNodes){
	instance_create_depth(x-xx,yy+spaceBetweenNodes/2,depth,obj_generator);
	instance_create_depth(x+xx,yy+spaceBetweenNodes/2,depth,obj_generator);
}


selectedNode=noone;

walls=[];


wallColors=[c_white,c_teal,c_maroon,c_green,c_purple,c_yellow];
wallNotes=[snd_crystal1,snd_crystal2,snd_crystal3,snd_crystal4,snd_crystal5,snd_crystal6];
gamelanNotes=[snd_gamelan1,snd_gamelan2,snd_gamelan3,snd_gamelan4,snd_gamelan5,snd_gamelan6];
pianoNotes=[snd_piano1,snd_piano2,snd_piano3,snd_piano4,snd_piano5,snd_piano6];