/// @description Insert description here
// You can write your code in this editor

if(obj_player.currentState==STATES.SWIMMING){

	var dt=delta_time/1000000

	speedValue=obj_game.speedValue;

	var vNormalize=Normalize(v);

	var prevV=[v[0],v[1]];
	var prevAV=angularV;

	if(y<=2912){
		v[1]+=obj_game.grav*dt*100;
	}else if(v[1]<gravityMaxSpeed){
		v[1]+=speedValue*dt*obj_game.grav/10;	
	}

	// deccelerate:
	if(y<=2912){
		if(abs(v[0])<0.01){
			v[0]=0;
		}else{
			v[0]=v[0]-vNormalize[0]*linearDrag*dt/speedValue;
			if(sign(v[0])!=sign(prevV[0])){
				v[0]=0;	
			}
		}
		if(abs(v[1])<0.01){
			v[1]=0;
		}else{
			v[1]=v[1]-vNormalize[1]*linearDrag*dt/speedValue;
			if(sign(v[1])!=sign(prevV[1])){
				v[1]=0;	
			}
		}
	}
		
	if(abs(angularV)<0.01){
		angularV=0;
	}else{
		angularV-=sign(angularV)*angularDrag*dt/speedValue;
		if(sign(prevAV)!=sign(angularV)){
			angularV=0;	
		}
	}

	angle+=angularV*dt;

	x+=v[0]*speedValue*dt;
	y+=v[1]*speedValue*dt;

	angle=(angle+360)%360;
	image_angle=-90-angle;

	if (place_meeting(x,y,obj_player)){
		if(!ignoreCollision){
		
		
			v[0]=v[0]+obj_player.v[0]/mass;
			v[1]=v[1]+obj_player.v[1]/mass;
	
			var vectorAB=[obj_player.x-x,(obj_player.y-y)];
			vectorAB=Normalize(vectorAB);
			var normalPlayerV=Normalize(obj_player.v);
			normalPlayerV[1]=normalPlayerV[1];
			var dir=[vectorAB[0]+normalPlayerV[0],vectorAB[1]+normalPlayerV[1]];
	
			var ang=darctan2(dir[1],dir[0]);
			show_debug_message(string(vectorAB[0])+" "+string(vectorAB[1]));
			show_debug_message(string(normalPlayerV[0])+" "+string(normalPlayerV[1]));
			show_debug_message(ang);
			show_debug_message(Magnitude(dir));
	
			angularV+=-Magnitude(dir)*sign(dir[0])*Magnitude(obj_player.v)*1/power(mass,0.5);
		
			obj_player.v[0]-=obj_player.v[0]*mass/100;
			obj_player.v[1]-=obj_player.v[1]*mass/100;
	
			//while(place_meeting(x,y,obj_player)){
			//	x+=v[0]*dt;
			//	y+=v[1]*dt;
			//}
			//ignoreCollision=true;
		}
	}else{
		//image_blend=c_white;	
		ignoreCollision=false;
	}

	if(!object_is_ancestor(object_index,obj_fish) && place_meeting(x,y,obj_collidable)){
		if(!ignoreCollision){
			var inst=instance_place(x, y, obj_collidable);
		
			if(!object_is_ancestor(inst.object_index,obj_fish)){
				var prevV=[v[0],v[1]];
	
				v[0]-=v[0]*inst.mass/50;
				v[1]-=v[1]*inst.mass/50;

				inst.v[0]+=v[0]*min(mass*0.01/inst.mass,1);
				inst.v[1]+=v[1]*min(mass*0.01/inst.mass,1);
		
		
	
				//var vectorAB=[obj_player.x-x,(obj_player.y-y)];
				//vectorAB=Normalize(vectorAB);
				//var normalPlayerV=Normalize(obj_player.v);
				//normalPlayerV[1]=normalPlayerV[1];
				//var dir=[vectorAB[0]+normalPlayerV[0],vectorAB[1]+normalPlayerV[1]];
	
				//var ang=darctan2(dir[1],dir[0]);
				//show_debug_message(string(vectorAB[0])+" "+string(vectorAB[1]));
				//show_debug_message(string(normalPlayerV[0])+" "+string(normalPlayerV[1]));
				//show_debug_message(ang);
				//show_debug_message(Magnitude(dir));
	
				//angularV+=-Magnitude(dir)*sign(dir[0])*Magnitude(obj_player.v)*1/power(mass,0.5);
		
		
	
				//while(place_meeting(x,y,obj_player)){
				//	x+=v[0]*dt;
				//	y+=v[1]*dt;
				//}
				//ignoreCollision=true;
			}
		}
	}

	angularV=sign(angularV)*min(maxAngularV,abs(angularV));
}