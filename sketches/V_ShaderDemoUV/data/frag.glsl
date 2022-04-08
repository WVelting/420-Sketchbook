#define PROCESSING_TEXTURE_SHADER

// values from Processing:
uniform sampler2D texture; // the texture to use
uniform vec2 texOffset; // size of a texel

// values from vert shader:
varying vec4 vertTexCoord; // uv value at this pixel
varying vec4 vertColor; // vertex color at this pixel

// runs "once per pixel":
void main()
{

    float ratio = texOffset.x / texOffset.y;

    // lookup pixel color at uv coordinate:
    vec4 color = texture2D(texture, vertTexCoord.xy);
    vec2 uv = vertTexCoord.xy;

    // TODO: make red border
    if(uv.x > .9 || uv.x < .1 || uv.y > .9 || uv.y < .1)
    {
        color = vec4(1,0,0,1);

    }

    // make blue circle in center
    vec2 dis = vec2(.5) - uv;

    dis.x /= ratio;

    float r = length(dis); // unit is percentage of screen

    if(r < .1)
    {
        color = vec4(0, 0, 1, 1);
    }


    //set the pixel color of gl_FragColor
    gl_FragColor = color;


}