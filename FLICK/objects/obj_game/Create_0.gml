dialogue=["Man what the fuck is your problem?",
"Why do you do this shit? Why? Why? Why?",
"You think this is funny? That we're all here having a laugh with you?",
"Just explain it to me. Please. I'm serious!!!!",
["Yes I think it's funny ","There's nothing else i can do","I have a naturally evil nature"],
"Are you fucking kidding me right now?",
"That's your reason?",
"Look i don't know if anybody has ever taught you about the concept of empathy",
["Yes i've heard about it","No i haven't"],"Stop doing that!!!!",
"Anyway, empathy is when a human being is capable of feeling the feelings of their fellow human being.",
"In this case, you flicking me causes me PHYSICAL PAIN and MILD ANNOYANCE.",
"Can you feel that at all? Do you see it on my face? In the tone if my voice???",
["Yes but it matters not to me.","No i was born without it.","Yes but still i must flick -- I am the flicker",],
"Oooh my god.",
"Okay. No matter. Have you heArd of the social contract?",
["I am not really listening rn","Only a few times","No i refuse to expose myself to philosophy"],
"Stop that!!!",
"The social contract is something that you implicitly sign when you are part of a society",
"For example, the one we are a part of.",
"Its terms are such that you may not do others what you do not want to be done on your person.",
"Would you like it if I and everyone else went around flicking you while you were just tending to your personal matters?? Would you???",
["No. Yet, i continue","I would not mind very much","Yes i would like it it would bring me such great pleasure.",],
"...",
"Look i don't know what to say to you",
"Just go about your business and leave me alone",
"Go flick other people and other people's properties! I don't care anymore!",
"I wash my hands off of this business! Do you hear me!",
"It is not my role nor my desire to educate you! So leave my sight!",
"Please go away!!!! I don't want to look at you anymore!!?",
"Admittedly you have a very handsome face that is just my type but the way you behaved today makes me physically disgusted!",
"Yes, i just said that, I am very comfortable with my sexuality.",
"I see no issue with admitting my attraction to you despite my hatred of you. Everything comes in shades of grey. And??",
"Regardless. Please leave me alone.",
"If you don't leave anytime soon you're going to hit the point where I run out of lines and I start repeating myself.",
"You probably don't want to see that.",
"It's very uncanny.",
"And yes that happens because i was born with a condition such that I cannot formulate more than ~100 possible sentences.",
"It has been very hard especially getting through my creative writing masters degree but i've managed through.",
"I'm sure you admire my efforts very much.",
"I have a few other lines but I don't think those would make any sense in the context.",
"The author's use of the outdated term \"sic\" is problematic and reflects their frankly disturbing views on the role of the teacher in the student's kitchen.",
"See? That was one of them. I shouldn't put out anymore than that. Going back..."
]

npcsFlicked=0;

showingDialogue=false;

depth=-1000000;

charIndex=0;
textIndex=0;



txtSpeed=50;

offsets=[[0,2],[2,0],[4,4]]

offsetX=0;
offsetY=0;

optionSelected=0;

flickTime=0;
flicked=false;
handImageIndex=0;

burning=false;

lighterCreated=false;

tempText="";

smgSlow=audio_play_sound(snd_smgSlow,1,true);
gemissements=-1;

timeSinceBlip=0;
blipTime=0.1;
basePitch=1;