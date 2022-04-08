#define PROCESSING_TEXTURE_SHADER

// values from Processing:
uniform sampler2D texture; // the texture to use
uniform vec2 texOffset; // size of a texel

// values from vert shader:
varying vec4 vertTexCoord; // uv value at this pixel
varying vec4 vertColor; // vertex color at this pixel

// own variables for experimentation

uniform vec2 mouse; //mouse position

// runs "once per pixel":
void main()
{

    float ratio = texOffset.x / texOffset.y;

    // sample color from neighboring pixel
    vec2 uv = vertTexCoord.xy - mouse;
    
    float dis = length(uv);

    
    // lookup pixel color at uv coordinate:
    vec4 color = vec4(dis, dis, dis, 1);


    //set the pixel color of gl_FragColor
    gl_FragColor = color;


}