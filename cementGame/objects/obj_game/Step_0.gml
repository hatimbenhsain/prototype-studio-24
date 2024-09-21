/// @description Insert description here
// You can write your code in this editor

if(audioTrack!=noone && audio_is_playing(audioTrack)){
	speedValue=clamp(1-audio_sound_get_track_position(audioTrack)/audio_sound_length(audioTrack),0,1);
}else if(audioTrack!=noone){
	timeSinceMusicEnd+=delta_time/1000000	
	speedValue=0;
}

if(timeSinceMusicEnd>=15){
	game_end();	
}

//speedValue=1;