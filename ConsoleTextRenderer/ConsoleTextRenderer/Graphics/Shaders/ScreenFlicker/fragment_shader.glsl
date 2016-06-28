#version 430

in vec2 out_uv;
in vec4 out_color;

layout(binding = 0) uniform sampler2D flickerMask;

uniform int ticks = 0;
uniform float scanlineY = 0.0;

const float scanRegionHeight = 0.0025;

float snoise(vec2 uv)
{
	return fract(sin(dot(uv.xy, vec2(12.9898, 78.233))) * 43758.5433);
}

void main()
{
	vec4 texture = texture2D(flickerMask, out_uv);
	vec4 noise = vec4(snoise(vec2(ticks * out_uv.y * out_uv.x) ));

	float alphaGain = 0.0;
	float distanceFromLine = scanlineY - out_uv.y;
	if (abs(distanceFromLine) <= scanRegionHeight)
	{
		alphaGain += 0.15;
	}
	else
	{
		//Has sign
		float dist = scanlineY - out_uv.y;

		if (dist < 0)
		{
			//alphaGain += pow(0.5, distanceFromLine);
		}
		else
		{
			alphaGain += pow(0.01, distanceFromLine) * 0.15;
		}
	}

	gl_FragColor = vec4(0.0f, 1.0f, 0.0f, noise.x * 0.25 + alphaGain);
}