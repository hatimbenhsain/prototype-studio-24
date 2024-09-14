/// @description Insert description here
// You can write your code in this editor

var dt=delta_time/1000000

var vNormal=Normal(v);

var prevV=[v[0],v[1]];
var prevAV=angularV;

// deccelerate:
if(abs(v[0])<0.01){
	v[0]=0;
}else{
	v[0]=v[0]-vNormal[0]*linearDrag*dt;
	if(sign(v[0])!=sign(prevV[0])){
		v[0]=0;	
	}
}
if(abs(v[1])<0.01){
	v[1]=0;
}else{
	v[1]=v[1]-vNormal[1]*linearDrag*dt;
	if(sign(v[1])!=sign(prevV[1])){
		v[1]=0;	
	}
}
		
if(abs(angularV)<0.01){
	angularV=0;
}else{
	angularV-=sign(angularV)*angularDrag*dt;
	if(sign(prevAV)!=sign(angularV)){
		angularV=0;	
	}
}

angle+=angularV*dt;

x+=v[0]*dt;
y+=v[1]*dt;

angle=(angle+360)%360;
direction=-90-angle;

if place_meeting(x,y,obj_player){
	image_blend=c_blue;
}else{
	image_blend=c_white;	
}