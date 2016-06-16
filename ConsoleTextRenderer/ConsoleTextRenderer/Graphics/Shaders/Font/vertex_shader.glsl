#version 330

layout(location = 0) in vec3 position;
//layout(location = 1) in vec2 in_uv;
//layout(location = 2) in vec4 in_color;

//out vec2 out_uv;
//out vec4 out_color;

void main()
{
	gl_Position = vec4(position,1.0);
	//out_uv		= in_uv;
	//out_color	= in_color;
}