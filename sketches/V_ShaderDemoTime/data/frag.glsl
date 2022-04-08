#define PROCESSING_TEXTURE_SHADER

// values from Processing:
uniform sampler2D texture; // the texture to use
uniform vec2 texOffset; // size of a texel

// values from vert shader:
varying vec4 vertTexCoord; // uv value at this pixel
varying vec4 vertColor; // vertex color at this pixel

// own variables for experimentation
uniform float time; // time in seconds??

// runs "once per pixel":
void main()
{

    float ratio = texOffset.x / texOffset.y;

    // sample color from neighboring pixel
    vec2 uv = vertTexCoord.xy - vec2(.5);
    //uv.x += texOffset.x;

    // switch to polar coord
    float mag = length(uv); // dis from center
    float rad = atan(uv.y, uv.x); // angle from center

    //mag -= .01 * sin(time);
    rad +=.05 * sin(time);

    uv.x = mag * cos(rad);
    uv.y = mag * sin(rad);

    uv += vec2(.5); // move origin back to zero
    
    // lookup pixel color at uv coordinate:
    vec4 color = texture2D(texture, uv);


    //set the pixel color of gl_FragColor
    gl_FragColor = color;


}