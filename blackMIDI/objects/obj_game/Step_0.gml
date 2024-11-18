/// @description Insert description here
// You can write your code in this editor
if(combatMode && !prevCombatMode){
	monster=instance_create_depth(0,0,depth,obj_monster);
	battlePlayer=instance_create_depth(0,0,depth,obj_battlePlayer);
	monster.game=self;
	inMenu=true;
	
	obj_everything.createGameTrigger=true;
	
	audio_stop_sound(musicInstance)
	musicInstance=audio_play_sound(snd_battle,1,true,0.1);
}else if(combatMode && !prevCombatMode){
	audio_stop_sound(musicInstance)
	musicInstance=audio_play_sound(snd_ow,1,true,0.1);
}

if(combatMode){
	if(inMenu){
		if(keyboard_check_pressed(player.button_up)){
			optionSelected++;
		}
		if(keyboard_check_pressed(player.button_down)){
			optionSelected--;
		}
		optionSelected=(optionSelected+3)%3;
		if(keyboard_check_pressed(player.button_interact)){
			audio_play_sound(snd_blip1,1,false,0.1);
			if(optionSelected==0){
				player.attacking=true;	
				inMenu=false;
			}else if(optionSelected==1){
				monster.attacking=true;	
				inMenu=false;
			}else if(optionSelected==2){
				combatMode=false;	
			}
		}
	}
	
	
	
	if(monster.hp<=0){
		timer+=delta_time/1000000;
		inMenu=false;
		if(timer>=1){
			combatMode=false;
			instance_destroy(monster);
			instance_destroy(battlePlayer);
			monster=-1;
			battlePlayer=-1;
			timer=0;
		}
	}
}else{
	optionSelected=0;
}

prevCombatMode=combatMode;