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
    // lookup pixel color at uv coordinate:
    vec4 color = texture2D(texture, vertTexCoord.xy);

    // TODO: invert

    // 0 => 1
    // 1 => 0
    // .5 => .5
    // .25 => .75
    // .75 => .25
    // y = 1-x

    color.rgb = 1- color.rgb;

    //set the pixel color of gl_FragColor
    gl_FragColor = color;


}