using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Graphics
{
    struct VertexBufferData
    {
        public VertexBufferData(float _x,float _y,float _z,float _u,float _v,float _r,float _g,float _b,float _a)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
            this.u = _u;
            this.v = _v;
            this.r = _r;
            this.g = _g;
            this.b = _b;
            this.a = _a;
        }

        float x;
        float y;
        float z;
        float u;
        float v;
        float r;
        float g;
        float b;
        float a;
    }
}
