/// @description Insert description here
// You can write your code in this editor
if(keyboard_check_pressed(ord("X")) && room=room_titleScreen){
	room_goto(room_game);
	audio_sound_gain(snd_smgSlow,0,5000);
	gemissements=audio_play_sound(snd_gemissements,1,true,0.1);
}

var dt=delta_time/1000000

if(showingDialogue){
	if(tempText!="" || (instance_exists(obj_lighter) && obj_lighter.lit)){
		basePitch=2;	
	}else{
		basePitch=1;	
	}
	if(burning){
		if(sprite_index!=spr_txtBox_burning){
			image_index=0;
		}
		sprite_index=spr_txtBox_burning;	
		if(image_index>=sprite_get_number(sprite_index)-1){
			showingDialogue=false;
		}
	}
	if(is_string(dialogue[textIndex]) || tempText!=""){
		optionSelected=0;
		handImageIndex=0;
		flicked=false;
		if(!burning)	sprite_index=spr_txtBox_withTail;
		charIndex+=dt*txtSpeed;
		var txt=dialogue[textIndex];
		if(tempText!=""){
			txt=tempText;	
		}
		charIndex=clamp(charIndex,0,string_length(txt));
		if(keyboard_check_pressed(ord("X"))){
			if(charIndex<string_length(txt)){
				charIndex=string_length(txt);	
			}else{
				charIndex=0;
				textIndex+=1;
			}
		}
		if(charIndex<string_length(txt)){
			timeSinceBlip+=dt;
			if(timeSinceBlip>=blipTime){
				audio_play_sound(snd_blip,1,false,1,0,basePitch-0.05+random(0.1));	
				timeSinceBlip=0;
			}
		}
	}else{
		sprite_index=spr_txtBox;
		
		if(!flicked){
			if(keyboard_check_pressed(ord("X"))){
				flicked=true
				flickTime=0;
			}
			if(keyboard_check_pressed(vk_down)){
				optionSelected+=1;	
			}
			if(keyboard_check_pressed(vk_up)){
				optionSelected-=1;	
			}
		}
		optionSelected=(optionSelected+array_length(dialogue[textIndex]))%array_length(dialogue[textIndex]);
		if(flicked){
			flickTime+=dt;
			//handImageIndex=flickTime*2+1;
			if(flickTime<0.2){
				handImageIndex=1;	
			}else{
				handImageIndex=2;	
				if(flickTime-dt<0.2) audio_play_sound(snd_flick3,1,false);
			}
			if(flickTime>=1){
				charIndex=0;
				textIndex+=1;	
				
			}
		}
	}
	
	if(textIndex>=array_length(dialogue)){
		textIndex=28;	
	}
	
	if(floor(image_index)!=floor(image_index-dt) || 
	(charIndex<string_length(dialogue[textIndex]) && floor(image_index*2)!=floor(image_index*2-dt) 
	&& is_string(dialogue[textIndex]))){	
		offsetX=random(5)
		offsetY=random(5)
	}
	
}

if(!lighterCreated && textIndex>=17){
	instance_create_depth(x,y,depth,obj_lighter);
	lighterCreated=true;
}