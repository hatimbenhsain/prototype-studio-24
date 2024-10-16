/// @description Insert description here
// You can write your code in this editor
if(collided){
	if(collidedTimer==0){
		if(collisionObject==obj_person) audio_play_sound(obj_game.wallNotes[progress],1,false,1,0,tone+1);
		else if(collisionObject==obj_bug) audio_play_sound(snd_glass,1,false,1,0,1+progress*0.1+tone);
		else if(collisionObject==obj_love) audio_play_sound(obj_game.wallNotes[progress],1,false,1,0,0.2+tone);
		else if(collisionObject==obj_horse) audio_play_sound(obj_game.pianoNotes[progress],1,false,1,0,1+tone);
		else if(collisionObject==obj_spirit) audio_play_sound(obj_game.gamelanNotes[progress],1,false,1,0,1+tone);
		col=obj_game.wallColors[progress];
		progress++;
		if(progress>=array_length(obj_game.wallColors)){
			instance_destroy(self);
		}
	}else if(collidedTimer>=collidedTime){
		collided=false;
	}
	collidedTimer+=delta_time/1000000
}else{
	collidedTimer=0;	
}

prevCollided=collided;