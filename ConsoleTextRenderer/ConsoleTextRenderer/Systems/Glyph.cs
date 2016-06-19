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
        private static int uCtr = 0, vCtr = 0;
        
        public static Glyph GLYPH_EMPTY = new Glyph(0.5f, 0.5f);
        public static Glyph GLYPH_NULL  = new Glyph(-1.0f,-1.0f);

        public static Glyph GLYPH_A = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_B = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_C = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_D = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_E = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_F = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_G = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_H = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_I = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_J = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_K = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_L = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_M = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_N = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_O = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_P = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_Q = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_R = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_S = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_T = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_U = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_V = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_W = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_X = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_Y = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_Z = new Glyph(AdvanceU(), AdvanceV());

        public static Glyph GLYPH_a = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_b = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_c = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_d = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_e = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_f = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_g = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_h = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_i = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_j = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_k = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_l = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_m = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_n = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_o = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_p = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_q = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_r = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_s = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_t = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_u = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_v = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_w = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_x = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_y = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_z = new Glyph(AdvanceU(), AdvanceV());

        public static Glyph GLYPH_0 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_1 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_2 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_3 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_4 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_5 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_6 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_7 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_8 = new Glyph(AdvanceU(), GetV());
        public static Glyph GLYPH_9 = new Glyph(AdvanceU(), GetV());
        //Special glyphs?

        //VERY DIRTY
        //Should really lookup 
        private static float AdvanceU()
        {
            float result = uCtr * GlyphManager.glyphUVWidth;
            uCtr++;
            return result;
        }

        private static float AdvanceV()
        {
            float result =  0.0f + (float)vCtr * GlyphManager.glyphUVHeight;
            vCtr++;
            uCtr = 0;
            return result;
        }

        private static float GetU()
        {
            return (float)uCtr * GlyphManager.glyphUVWidth;
        }

        private static float GetV()
        {
            return (float)vCtr * GlyphManager.glyphUVHeight;
        }

        public float U0 = 0.0F;
        public float V0 = 0.0F;

        public Glyph(float u0,float v0)
        {
            this.U0 = u0;
            this.V0 = v0;
        }
    }
}
