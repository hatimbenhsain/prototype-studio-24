

surf=-1;

player=-1;

w=obj_everything.w;
h=obj_everything.h;

combatMode=false;

//player=instance_create_depth()

prevCombatMode=combatMode;

monster=-1;

optionSelected=0;

inMenu=true;

musicInstance=audio_play_sound(snd_ow,1,true,0.1);

battlePlayer=-1;

timer=0;

sh_handle_match1=shader_get_uniform(sh_replaceColor,"colorMatch1");
sh_handle_replace1=shader_get_uniform(sh_replaceColor,"colorReplace1");
sh_handle_match2=shader_get_uniform(sh_replaceColor,"colorMatch2");
sh_handle_replace2=shader_get_uniform(sh_replaceColor,"colorReplace2");
sh_handle_range=shader_get_uniform(sh_replaceColor,"range");

color1R=random(1);
color1B=random(1);
color1G=random(1);
color2R=random(1);
color2B=random(1);
color2G=random(1);


add=false;
subtract=false;

