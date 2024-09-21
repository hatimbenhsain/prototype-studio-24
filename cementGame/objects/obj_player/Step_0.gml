/// @description Insert description here
// You can write your code in this editor
//show_debug_message(gamepad_axis_value(4, gp_axislh));
//show_debug_message(gamepad_axis_value(4, gp_axislv));

var dt=delta_time/1000000

var targetAngle=angle;
var jx=gamepad_axis_value(4, gp_axislh)+gamepad_axis_value(0, gp_axislh); //joystick x
var jy=gamepad_axis_value(4, gp_axislv)+gamepad_axis_value(0, gp_axislv); //joystick y

speedValue=obj_game.speedValue;

maxImgSpeed=speedValue;

if(maxImgSpeed==0){
	image_speed=0;	
}

if(keyboard_check(vk_left)){
	jx-=1;	
}
if(keyboard_check(vk_right)){
	jx+=1;	
}
if(keyboard_check(vk_up)){
	jy-=1;	
}
if(keyboard_check(vk_down)){
	jy+=1;	
}

jx=clamp(jx,-1,1);
jy=clamp(jy,-1,1);

switch(currentState){
	case STATES.WALKING:
		var walking=false;
		if(abs(jx)>0.2){
			walking=true;
			x+=sign(jx)*dt*10;
			if(jx<0){
				image_xscale=-1;	
			}else{
				image_xscale=1;	
			}
		}
		
		y=384+13*(x-2336)/(2447-2336)
		
		if(walking){
			sprite_index=spr_player_surface_walking;
		}else{
			sprite_index=spr_player_surface_idle;	
		}
		
		if(x>=2443){
			currentState=STATES.FALLING;	
			sprite_index=spr_player_fallStart;
			image_index=0;
		}
		
		x=max(2336,x);
		
		
		break;
	case STATES.FALLING:
		v[1]+=dt*obj_game.grav;
		v[1]=min(v[1],2000);
		v[0]=v[0]*0.9;
		v[0]+=jx*dt*50;
		y+=v[1]*dt;
		x+=v[0]*dt;
		if(image_index>=4 && sprite_index=spr_player_fallStart){
			sprite_index=spr_player_falling;
		}
		var cx=camera_get_view_x(view_camera[0]);
		var cy=camera_get_view_y(view_camera[0]);
		var tcx=cx;
		var tcy=max(y-camera_get_view_height(view_camera[0])/2,64);
		cx=lerp(cx,tcx,cameraSpeed*dt);
		var val=max(0.2,min(1,(1-(2712-y)/(2712-382))));
		cy=lerp(cy,tcy,cameraSpeed*7*val*dt);


		camera_set_view_pos(view_camera[0],cx,cy);
		
		
		if(y>=3012){
			currentState=STATES.SWIMMING;	
			sprite_index=spr_player_fallEnd;
			audio_stop_sound(snd_cementMixer);
			obj_game.audioTrack=audio_play_sound(snd_cement,1,false,obj_game.maxVolume);
		}
		
		break;
	case STATES.SWIMMING:
		if(image_index>=4 && sprite_index=spr_player_fallEnd){
			sprite_index=spr_player_idle;
		}
	
		var turning=false;
		var coasting=false;
		var boosting=false;
	
		pushTimer+=dt;
		pullTimer+=dt;
		
		var vNormalize=Normalize(v);
		
		if(v[1]<gravityMaxSpeed){
			v[1]+=speedValue*dt*obj_game.grav/10;	
		}
		
		//cap speed
		v[0]=sign(v[0])*min(abs(v[0]),abs(vNormalize[0])*maxSpeed*speedValue);
		v[1]=sign(v[1])*min(abs(v[1]),abs(vNormalize[1])*maxSpeed*speedValue);
		
		if(y<=2912){
			v[1]+=obj_game.grav*dt;
		}
		
		var prevV=[v[0],v[1]];
		var prevAV=angularV;
		
		// deccelerate:
		if(abs(v[0])<0.01){
			v[0]=0;
		}else{
			v[0]=v[0]-vNormalize[0]*linearDrag*dt;
			if(sign(v[0])!=sign(prevV[0])){
				v[0]=0;	
			}
		}
		if(abs(v[1])<0.01){
			v[1]=0;
		}else{
			v[1]=v[1]-vNormalize[1]*linearDrag*dt;
			if(sign(v[1])!=sign(prevV[1])){
				v[1]=0;	
			}
		}
		
		//angular drag
		if(abs(angularV)<0.01){
			angularV=0;
		}else{
			angularV-=sign(angularV)*angularDrag*speedValue*dt;
			if(sign(prevAV)!=sign(angularV)){
				angularV=0;	
			}
		}
		
		if(pushTrigger){
			if(pushTimer>=pushDelay && pushTimer-dt<pushDelay){
				boosting=true;
			}
		}
		
		if(pullTrigger){
			if(pullTimer>=pushDelay && pullTimer-dt<pushDelay){
				var bx=speedValue*boostAcceleration*cos(degtorad(180+angle))*0.5;
				var by=speedValue*boostAcceleration*sin(degtorad(180+angle))*0.5;
				v[0]=v[0]+bx;
				v[1]=v[1]+by;
			}
		}
		
		if(y>2912 && (abs(jx)>0.2 || abs(jy)>0.2)){
			turning=true;
			targetAngle=(-darctan2(jx,jy)+90)%360;			
		}
		
		if(turning){
			var difference=targetAngle-angle;
			
			//show_debug_message(targetAngle);
			//show_debug_message(angle);
			//show_debug_message(difference);
			
			if(abs(difference)>5 && 360-abs(difference)>5){
				var dir=sign(difference);
				if(abs(difference)>=180){
					dir=-dir;
				}
				var acc=angularAcceleration
				if(boosting){
					acc=angularAcceleration*10;	
				}
				angularV+=speedValue*acc*dt*dir;
				//show_debug_message("turning");
			}else{
				//show_debug_message("no turning")
			}
		}
		
		angularV=clamp(angularV,-maxAngularV*speedValue,maxAngularV*speedValue);
		
		angle+=angularV*dt;
		
		if(boosting){
			var bx=speedValue*boostAcceleration*cos(degtorad(angle));
			var by=speedValue*boostAcceleration*sin(degtorad(angle));
			v[0]=v[0]+bx;
			v[1]=v[1]+by;
		}
	
		if(y>2912){
			if(speedValue>=0.01 && pushTimer>0.5 && (keyboard_check_pressed(ord("X")) || gamepad_button_check_pressed(4,gp_face2) ||  gamepad_button_check_pressed(0,gp_face2))){
				sprite_index=spr_player_pushing;
				image_index=0;
				pushTrigger=true;
				pushTimer=0;
				image_speed=1*maxImgSpeed;
			}else if(keyboard_check(ord("X")) || gamepad_button_check(4,gp_face2) || gamepad_button_check(0,gp_face2)){
				coasting=true;
				if(Magnitude(v)<maxCoastingSpeed){
					var bx=speedValue*acceleration*cos(degtorad(angle))*dt;
					var by=speedValue*acceleration*sin(degtorad(angle))*dt;
					v[0]=v[0]+bx;
					v[1]=v[1]+by;
				}
			}
		
			if(speedValue>=0.01 && pullTimer>0.5 && (keyboard_check_pressed(ord("Z")) || gamepad_button_check_pressed(4,gp_face1) ||  gamepad_button_check_pressed(0,gp_face1))){
				sprite_index=spr_player_pulling;
				image_index=0;
				pullTrigger=true;
				pullTimer=0;
				image_speed=1*maxImgSpeed;
			}else if(keyboard_check(ord("Z")) || gamepad_button_check(4,gp_face1) ||  gamepad_button_check(0,gp_face1)){
				coasting=true;
				if(Magnitude(v)<maxCoastingSpeed){
					var bx=speedValue*acceleration*cos(degtorad(180+angle))*dt;
					var by=speedValue*acceleration*sin(degtorad(180+angle))*dt;
					v[0]=v[0]+bx;
					v[1]=v[1]+by;
				}
			}
		}


		x+=v[0]*dt;
		y+=v[1]*dt;
		
		
		
		//animation corner
		if(pushTimer>=0.5/image_speed && image_index<1 && !coasting && sprite_index==spr_player_pushing){
			sprite_index=spr_player_idle;
			image_index=0;
			image_speed=maxImgSpeed;
		}else if(pullTimer>=0.5/image_speed && image_index<1 && !coasting && sprite_index==spr_player_pulling){
			sprite_index=spr_player_idle;
			image_index=0;
			image_speed=maxImgSpeed;
		}
		else if(coasting && pushTimer>=0.5 && pullTimer>=0.5 && image_index<1){
			image_speed=maxImgSpeed*(0.4+max((Magnitude(v)-maxCoastingSpeed)/(maxSpeed-maxCoastingSpeed),0)*0.25);
		}
		
		if(sprite_index==spr_player_idle && turning){
			sprite_index=spr_player_turning;
		}else if(sprite_index==spr_player_turning && !turning){
			sprite_index=spr_player_idle;
		}
		
		//camera
		var cx=camera_get_view_x(view_camera[0]);
		var cy=camera_get_view_y(view_camera[0]);
		var tcx=x-camera_get_view_width(view_camera[0])/2;
		var tcy=y-camera_get_view_height(view_camera[0])/2;
		cx=lerp(cx,tcx,cameraSpeed*dt);
		cy=lerp(cy,tcy,cameraSpeed*dt);

		camera_set_view_pos(view_camera[0],cx,cy);
		
		break;
}

angle=(angle+360)%360;
image_angle=-90-angle;

