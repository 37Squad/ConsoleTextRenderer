#version 430

in vec2 out_uv;
in vec4 out_color;

uniform sampler2D textureAtlas;

//Black
const vec4 ignoreColor = vec4(0.0);

void main()
{
	//Grab texture from UV coords
	vec4 textured_frag = texture(textureAtlas,out_uv);
	
	//If our texture is BLACK, then let it through
	if( equal(textured_frag,ignoreColor) )
	{
		gl_FragColor = textured_frag;
	}
	//OTHERWISE, if we have a set color, use it!
	//We do this so that we can color our text!
	else
	{
		gl_FragColor = out_color;
	}

	gl_FragColor = out_color;
}