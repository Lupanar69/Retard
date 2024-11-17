using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.SpritesheetInstancing
{
    public partial class SpritesheetInstancing
    {
        //Data Struct for the instancing buffer
        public struct InstanceData(float rotation, float depth, Color color, Rectangle rectangle, Vector2 pos, Vector2 scale) : IVertexType
        {
            public float Depth = depth;                                                     // Depth
            public float Rotation = rotation;                                               // Roatation                              4 byte
            public Color Color = color;                                                     // Tint                                  4 byte
            public int RectangleXY = RectangleSqueezer(rectangle.X, rectangle.Y);           // x,y Position in SpriteSheet           4 byte
            public int RectangleWH = RectangleSqueezer(rectangle.Width, rectangle.Height);  // y Height, x Width                     4 byte
            public Vector2 Position = pos;                                                  // Position                              8 byte
            public Vector2 Scale = scale;                                                   // X,Y Scale                             8 byte

            public static readonly VertexDeclaration VertexDeclaration = new(
                new VertexElement(0, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 1),  //4 byte TEXCOORD1
                new VertexElement(4, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 2),  //4 byte TEXCOORD2
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 1),               //4 byte COLOR1
                new VertexElement(12, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 3), //4 byte TEXCOORD3
                new VertexElement(16, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 4), //4 byte TEXCOORD4
                new VertexElement(20, VertexElementFormat.Vector2, VertexElementUsage.Position, 1),         //8 byte POSITION1
                new VertexElement(28, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 5) //8 byte TEXCOORD5
            );

            /// <summary>
            /// Render Rectangles are not bigger than 65k
            /// Bitshifts 2 values together for a smaller vertexbuffer
            /// </summary>
            /// <returns></returns>            
            public static int RectangleSqueezer(int x, int y)
            {
                // If x and y bigger than max bits on the right side = 65535
                if (x > 65535)
                {
                    x = 65535; // 0000 0000 0000 0000 1111 1111 1111 1111
                }
                if (y > 65535)
                {
                    y = 65535; // 0000 0000 0000 0000 1111 1111 1111 1111
                }

                // Shifts the bits of x to the left
                int value = x << 16; // 1111 1111 1111 1111 0000 0000 0000 0000 
                                     //                                     | x           y           value
                value |= y;          // overrides all with the second Value | 1010 0000 | 0000 1101 = 1010 1101

                return value;
            }

            readonly VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
        }
    }
}
