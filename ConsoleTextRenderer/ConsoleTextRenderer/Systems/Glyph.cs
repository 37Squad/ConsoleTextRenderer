using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class Glyph
    {
        //Add all of the UV data we need for each glyph in LoadGlyphData
        //Alpha-numeric glpyhs
        //Should make a square for this! Right now it is in the middle of nowhere
        public static Glyph GLYPH_EMPTY = new Glyph(0.5f, 0.5f);
        public static Glyph GLYPH_NULL  = new Glyph(-1.0f,-1.0f);

        public static Glyph GLYPH_A = new Glyph(1.0f, 1.0f);
        public static Glyph GLYPH_B = new Glyph(0.03125f, 0.0f);
        public static Glyph GLYPH_C = new Glyph(0.0625f, 0.0f);
        public static Glyph GLYPH_D = new Glyph(0.09375f, 0.0f);
        public static Glyph GLYPH_E = new Glyph(0.125f, 0.0f);
        public static Glyph GLYPH_F = new Glyph(0.15625f, 0.0f);
        public static Glyph GLYPH_G = new Glyph(0.1875f, 0.0f);
        public static Glyph GLYPH_H = new Glyph(0.21875f, 0.0f);
        public static Glyph GLYPH_I;
        public static Glyph GLYPH_J;
        public static Glyph GLYPH_K;
        public static Glyph GLYPH_L;
        public static Glyph GLYPH_M;
        public static Glyph GLYPH_N;
        public static Glyph GLYPH_O;
        public static Glyph GLYPH_P;
        public static Glyph GLYPH_Q;
        public static Glyph GLYPH_R;
        public static Glyph GLYPH_S;
        public static Glyph GLYPH_T;
        public static Glyph GLYPH_U;
        public static Glyph GLYPH_V;
        public static Glyph GLYPH_W;
        public static Glyph GLYPH_X;
        public static Glyph GLYPH_Y;
        public static Glyph GLYPH_Z;

        public static Glyph GLYPH_a;
        public static Glyph GLYPH_b;
        public static Glyph GLYPH_c;
        public static Glyph GLYPH_d;
        public static Glyph GLYPH_e;
        public static Glyph GLYPH_f;
        public static Glyph GLYPH_g;
        public static Glyph GLYPH_h;
        public static Glyph GLYPH_i;
        public static Glyph GLYPH_j;
        public static Glyph GLYPH_k;
        public static Glyph GLYPH_l;
        public static Glyph GLYPH_m;
        public static Glyph GLYPH_n;
        public static Glyph GLYPH_o;
        public static Glyph GLYPH_p;
        public static Glyph GLYPH_q;
        public static Glyph GLYPH_r;
        public static Glyph GLYPH_s;
        public static Glyph GLYPH_t;
        public static Glyph GLYPH_u;
        public static Glyph GLYPH_v;
        public static Glyph GLYPH_w;
        public static Glyph GLYPH_x;
        public static Glyph GLYPH_y;
        public static Glyph GLYPH_z;

        public static Glyph GLYPH_0;
        public static Glyph GLYPH_1;
        public static Glyph GLYPH_2;
        public static Glyph GLYPH_3;
        public static Glyph GLYPH_4;
        public static Glyph GLYPH_5;
        public static Glyph GLYPH_6;
        public static Glyph GLYPH_7;
        public static Glyph GLYPH_8;
        public static Glyph GLYPH_9;
        //Special glyphs?

        public float U0 = 0.0F;
        public float V0 = 0.0F;

        public Glyph(float u0,float v0)
        {
            this.U0 = u0;
            this.V0 = v0;
        }
    }
}
