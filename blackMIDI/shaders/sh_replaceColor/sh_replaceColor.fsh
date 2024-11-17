//
// Simple passthrough fragment shader
//
varying vec2 v_vTexcoord;
varying vec4 v_vColour;

uniform float range;
uniform vec4 colorMatch1;
uniform vec4 colorMatch2;
uniform vec4 colorReplace1;
uniform vec4 colorReplace2;

void main()
{
	vec4 pixelColor=v_vColour * texture2D( gm_BaseTexture, v_vTexcoord );
	if(abs(pixelColor.r-colorMatch1.r)<=range){
		if(abs(pixelColor.g-colorMatch1.g)<=range){
			if(abs(pixelColor.b-colorMatch1.b)<=range){
				//pixelColor.rgb=colorReplace.rgb;
				pixelColor.r=colorReplace1.r+(pixelColor.r-colorMatch1.r);
				pixelColor.g=colorReplace1.g+(pixelColor.g-colorMatch1.g);
				pixelColor.b=colorReplace1.b+(pixelColor.b-colorMatch1.b);
			}
		}
	}else if(abs(pixelColor.r-colorMatch2.r)<=range){
		if(abs(pixelColor.g-colorMatch2.g)<=range){
			if(abs(pixelColor.b-colorMatch2.b)<=range){
				//pixelColor.rgb=colorReplace.rgb;
				pixelColor.r=colorReplace2.r+(pixelColor.r-colorMatch2.r);
				pixelColor.g=colorReplace2.g+(pixelColor.g-colorMatch2.g);
				pixelColor.b=colorReplace2.b+(pixelColor.b-colorMatch2.b);
			}
		}
	}
    gl_FragColor = pixelColor;
}
