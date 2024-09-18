/// @description Insert description here
// You can write your code in this editor
//show_debug_message(gamepad_axis_value(4, gp_axislh));
//show_debug_message(gamepad_axis_value(4, gp_axislv));

var dt=delta_time/1000000

swimTimer+=dt;

var targetAngle=angle;


jx=clamp(jx,-1,1);
jy=clamp(jy,-1,1);

switch(currentState){
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
			v[1]+=dt*obj_game.grav/10;	
		}
		
		//cap speed
		v[0]=sign(v[0])*min(abs(v[0]),abs(vNormalize[0])*maxSpeed);
		v[1]=sign(v[1])*min(abs(v[1]),abs(vNormalize[1])*maxSpeed);
		
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
				var bx=boostAcceleration*cos(degtorad(180+angle))*0.5;
				var by=boostAcceleration*sin(degtorad(180+angle))*0.5;
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
				angularV+=acc*dt*dir;
				//show_debug_message("turning");
			}else{
				//show_debug_message("no turning")
			}
		}
		
		angle+=angularV*dt;
		
		if(boosting){
			var bx=boostAcceleration*cos(degtorad(angle));
			var by=boostAcceleration*sin(degtorad(angle));
			v[0]=v[0]+bx;
			v[1]=v[1]+by;
		}
	
		if(y>2912){
			if(swimTimer>=swimFrequency && random(1)<=swimProbability){
				jx=random(2)-1;
				jy=random(2)-1;
				sprite_index=spr_player_pushing;
				image_index=0;
				pushTrigger=true;
				pushTimer=0;
				image_speed=1*maxImgSpeed;
				swimTimer=0;
			}else if(swimTimer<=1){
				coasting=true;
				if(Magnitude(v)<maxCoastingSpeed){
					var bx=acceleration*cos(degtorad(angle))*dt;
					var by=acceleration*sin(degtorad(angle))*dt;
					v[0]=v[0]+bx;
					v[1]=v[1]+by;
				}
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
image_angle=-90-angle;

