#version 430

in vec2 out_uv;
in vec4 out_color;

uniform sampler2D textureAtlas;
uniform bool render;

//Black
const vec4 ignoreColor = vec4(0.0);

void main()
{
	if (render)
	{
		//Grab texture from UV coords
		vec4 textured_frag = texture(textureAtlas, out_uv);

		//If we have a white pixel, then we are instead going to use the color passed in
		//We can make our text different colors!
		if (textured_frag == vec4(1.0, 1.0, 1.0, 1.0))
		{
			textured_frag = out_color;
		}

		gl_FragColor = textured_frag;
	}
	else
	{
		discard;
	}
}