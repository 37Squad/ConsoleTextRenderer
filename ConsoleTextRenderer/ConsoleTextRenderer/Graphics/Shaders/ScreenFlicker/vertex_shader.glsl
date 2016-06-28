#version 430

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 in_uv;
layout(location = 2) in vec4 in_color;

layout(location = 0) uniform mat4 model			= mat4(1.0);
layout(location = 1) uniform mat4 view			= mat4(1.0);
layout(location = 2) uniform mat4 projection	= mat4(1.0);

out vec2 out_uv;
out vec4 out_color;

void main()
{
	gl_Position = model * view * projection * vec4(position,1.0);
	out_uv		= in_uv;
	out_color	= in_color;
}