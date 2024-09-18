/// @description Insert description here
// You can write your code in this editor

currentState=STATES.SWIMMING;


v=[0,0]; //velocity

angle=270; //in degrees
angularV=0;



maxSpeed=50;
maxCoastingSpeed=20;
acceleration=15;
boostAcceleration=30;
angularAcceleration=180;
linearDrag=10;
angularDrag=135;

gravityMaxSpeed=10;


pushTrigger=false;
pushTimer=0;
pushDelay=0.2;

pullTrigger=false;
pullTimer=0;

var _maxpads = gamepad_get_device_count();

for (var i = 0; i < _maxpads; i++)
{
    if (gamepad_is_connected(i))
    {
        show_debug_message("gamepad connected");
    }
	else{
		show_debug_message("gamepad not connected");
	}
}

if(_maxpads==0){
	show_debug_message("no gamepads");
}

cameraSpeed=1;

maxImgSpeed=1;

offsetX=0;
offsetY=0;

swimFrequency=2;
swimTimer=0;
swimProbability=0.05;

jx=0
jy=0;

