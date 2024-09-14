/// @description Insert description here
// You can write your code in this editor
//show_debug_message(gamepad_axis_value(4, gp_axislh));
//show_debug_message(gamepad_axis_value(4, gp_axislv));

var dt=delta_time/1000000

var targetAngle=angle;


switch(currentState){
	case STATES.SWIMMING:
		var turning=false;
		var coasting=false;
		var boosting=false;
	
		pushTimer+=dt;
		pullTimer+=dt;
		
		var vNormal=Normal(v);
		
		//cap speed
		v[0]=sign(v[0])*min(abs(v[0]),abs(vNormal[0])*maxSpeed);
		v[1]=sign(v[1])*min(abs(v[1]),abs(vNormal[1])*maxSpeed);
		
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
		
		if(pushTrigger){
			if(pushTimer>=pushDelay && pushTimer-dt<pushDelay){
				boosting=true;
			}
		}
		
		if(pullTrigger){
			if(pullTimer>=pushDelay && pullTimer-dt<pushDelay){
				var bx=boostAcceleration*cos(degtorad(180+angle));
				var by=boostAcceleration*sin(degtorad(180+angle));
				v[0]=v[0]+bx;
				v[1]=v[1]+by;
			}
		}
		
		var jx=gamepad_axis_value(4, gp_axislh); //joystick x
		var jy=gamepad_axis_value(4, gp_axislv); //joystick y
		if(abs(jx)>0.2 || abs(jy)>0.2){
			turning=true;
			targetAngle=(-darctan2(jx,jy)+90)%360;			
		}
		
		if(turning){
			var difference=targetAngle-angle;
			
			show_debug_message(targetAngle);
			show_debug_message(angle);
			show_debug_message(difference);
			
			if(abs(difference)>5 && 360-abs(difference)>5){
				var dir=sign(difference);
				if(abs(difference)>=180){
					dir=-dir;
				}
				var acc=angularAcceleration
				if(boosting){
					acc=angularAcceleration*10;	
				}
				angularV+=acc*dt*dir;
				show_debug_message("turning");
			}else{
				show_debug_message("no turning")
			}
		}
		
		angle+=angularV*dt;
		
		if(boosting){
			var bx=boostAcceleration*cos(degtorad(angle));
			var by=boostAcceleration*sin(degtorad(angle));
			v[0]=v[0]+bx;
			v[1]=v[1]+by;
		}
	
		if(pushTimer>0.5 && (keyboard_check_pressed(ord("X")) || gamepad_button_check_pressed(4,gp_face2))){
			sprite_index=spr_player_pushing;
			image_index=0;
			pushTrigger=true;
			pushTimer=0;
			image_speed=1*maxImgSpeed;
		}else if(keyboard_check(ord("X")) || gamepad_button_check(4,gp_face2)){
			coasting=true;
			if(Magnitude(v)<maxCoastingSpeed){
				var bx=acceleration*cos(degtorad(angle))*dt;
				var by=acceleration*sin(degtorad(angle))*dt;
				v[0]=v[0]+bx;
				v[1]=v[1]+by;
			}
		}
		
		if(pullTimer>0.5 && (keyboard_check_pressed(ord("Z")) || gamepad_button_check_pressed(4,gp_face1))){
			sprite_index=spr_player_pulling;
			image_index=0;
			pullTrigger=true;
			pullTimer=0;
			image_speed=1*maxImgSpeed;
		}else if(keyboard_check(ord("Z")) || gamepad_button_check(4,gp_face1)){
			coasting=true;
			if(Magnitude(v)<maxCoastingSpeed){
				var bx=acceleration*cos(degtorad(180+angle))*dt;
				var by=acceleration*sin(degtorad(180+angle))*dt;
				v[0]=v[0]+bx;
				v[1]=v[1]+by;
			}
		}


		x+=v[0]*dt;
		y+=v[1]*dt;
		
		
		
		//animation corner
		if(pushTimer>=0.5 && image_index<1 && !coasting && sprite_index==spr_player_pushing){
			sprite_index=spr_player_idle;
			image_index=0;
			image_speed=maxImgSpeed;
		}else if(pullTimer>=0.5 && image_index<1 && !coasting && sprite_index==spr_player_pulling){
			sprite_index=spr_player_idle;
			image_index=0;
			image_speed=maxImgSpeed;
		}
		else if(coasting && pushTimer>=0.5 && pullTimer>=0.5 && image_index<1){
			image_speed=maxImgSpeed*(0.4+max((Magnitude(v)-maxCoastingSpeed)/(maxSpeed-maxCoastingSpeed),0)*0.25);
		}
		
	break;
}

angle=(angle+360)%360;
direction=-90-angle;

//camera
var cx=camera_get_view_x(view_camera[0]);
var cy=camera_get_view_y(view_camera[0]);
var tcx=x-camera_get_view_width(view_camera[0])/2;
var tcy=y-camera_get_view_height(view_camera[0])/2;
cx=lerp(cx,tcx,cameraSpeed*dt);
cy=lerp(cy,tcy,cameraSpeed*dt);

camera_set_view_pos(view_camera[0],cx,cy);