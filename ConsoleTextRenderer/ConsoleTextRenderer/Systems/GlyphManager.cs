using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class GlyphManager
    {
        //'glyphPos' and 'glyphLine' are indexes for the 'glyphs' array
        //holds current glpyh position in the current line
        private int glyphPos = 0;
        //holds current glyph line
        private int glyphLine = 0;
        //this is the array that contains the glpyh data
        private Glyph[,] glyphs = null;
        //this is a glpyh lookup table- convert chars into its equivalent glyph
        //Not used currently - not sure if we need it
        //EDIT: WE NEED IT!
        private Dictionary<Glyph, char> glyphLookupTable = new Dictionary<Glyph, char>();

        //max lines
        private int maxLines = 0;
        //max characters per line
        private int maxCharacters = 0;

        //Pixels / image dimension
        public static float glyphUVWidth = 16.0f / 512.0f;
        public static float glyphUVHeight = 16.0f / 512.0f;
        //image dimension shouldn't be changed...
        public static float glyphMapWidth = 512.0F;
        public static float glyphMapHeight = 512.0F;
        public static float glyphWidth  = 0.03125f;
        public static float glyphHeight = 0.03125f;

        //Constructor
        public GlyphManager(int lines,int characters)
        {
            this.glyphs = new Glyph[lines,characters];
            this.maxCharacters  = characters;
            this.maxLines       = lines;
            //this.glyphWidth /= 2 * this.maxCharacters;
            //this.glyphHeight = this.glyphWidth;
            this.ClearGlpyhs();

            //Glyph Map initialization
            this.glyphLookupTable[Glyph.GLYPH_A] = 'A';
            this.glyphLookupTable[Glyph.GLYPH_B] = 'B';
            this.glyphLookupTable[Glyph.GLYPH_C] = 'C';
            this.glyphLookupTable[Glyph.GLYPH_D] = 'D';
            this.glyphLookupTable[Glyph.GLYPH_E] = 'E';
            this.glyphLookupTable[Glyph.GLYPH_F] = 'F';
            this.glyphLookupTable[Glyph.GLYPH_G] = 'G';
            this.glyphLookupTable[Glyph.GLYPH_H] = 'H';
            this.glyphLookupTable[Glyph.GLYPH_I] = 'I';
            this.glyphLookupTable[Glyph.GLYPH_J] = 'J';
            this.glyphLookupTable[Glyph.GLYPH_K] = 'K';
            this.glyphLookupTable[Glyph.GLYPH_L] = 'L';
            this.glyphLookupTable[Glyph.GLYPH_M] = 'M';
            this.glyphLookupTable[Glyph.GLYPH_N] = 'N';
            this.glyphLookupTable[Glyph.GLYPH_O] = 'O';
            this.glyphLookupTable[Glyph.GLYPH_P] = 'P';
            this.glyphLookupTable[Glyph.GLYPH_Q] = 'Q';
            this.glyphLookupTable[Glyph.GLYPH_R] = 'R';
            this.glyphLookupTable[Glyph.GLYPH_S] = 'S';
            this.glyphLookupTable[Glyph.GLYPH_T] = 'T';
            this.glyphLookupTable[Glyph.GLYPH_U] = 'U';
            this.glyphLookupTable[Glyph.GLYPH_V] = 'V';
            this.glyphLookupTable[Glyph.GLYPH_W] = 'W';
            this.glyphLookupTable[Glyph.GLYPH_X] = 'X';
            this.glyphLookupTable[Glyph.GLYPH_Y] = 'Y';
            this.glyphLookupTable[Glyph.GLYPH_Z] = 'Z';

            this.glyphLookupTable[Glyph.GLYPH_a] = 'a';
            this.glyphLookupTable[Glyph.GLYPH_b] = 'b';
            this.glyphLookupTable[Glyph.GLYPH_c] = 'c';
            this.glyphLookupTable[Glyph.GLYPH_d] = 'd';
            this.glyphLookupTable[Glyph.GLYPH_e] = 'e';
            this.glyphLookupTable[Glyph.GLYPH_f] = 'f';
            this.glyphLookupTable[Glyph.GLYPH_g] = 'g';
            this.glyphLookupTable[Glyph.GLYPH_h] = 'h';
            this.glyphLookupTable[Glyph.GLYPH_i] = 'i';
            this.glyphLookupTable[Glyph.GLYPH_j] = 'j';
            this.glyphLookupTable[Glyph.GLYPH_k] = 'k';
            this.glyphLookupTable[Glyph.GLYPH_l] = 'l';
            this.glyphLookupTable[Glyph.GLYPH_m] = 'm';
            this.glyphLookupTable[Glyph.GLYPH_n] = 'n';
            this.glyphLookupTable[Glyph.GLYPH_o] = 'o';
            this.glyphLookupTable[Glyph.GLYPH_p] = 'p';
            this.glyphLookupTable[Glyph.GLYPH_q] = 'q';
            this.glyphLookupTable[Glyph.GLYPH_r] = 'r';
            this.glyphLookupTable[Glyph.GLYPH_s] = 's';
            this.glyphLookupTable[Glyph.GLYPH_t] = 't';
            this.glyphLookupTable[Glyph.GLYPH_u] = 'u';
            this.glyphLookupTable[Glyph.GLYPH_v] = 'v';
            this.glyphLookupTable[Glyph.GLYPH_w] = 'w';
            this.glyphLookupTable[Glyph.GLYPH_x] = 'x';
            this.glyphLookupTable[Glyph.GLYPH_y] = 'y';
            this.glyphLookupTable[Glyph.GLYPH_z] = 'z';

            this.glyphLookupTable[Glyph.GLYPH_0] = '0';
            this.glyphLookupTable[Glyph.GLYPH_1] = '1';
            this.glyphLookupTable[Glyph.GLYPH_2] = '2';
            this.glyphLookupTable[Glyph.GLYPH_3] = '3';
            this.glyphLookupTable[Glyph.GLYPH_4] = '4';
            this.glyphLookupTable[Glyph.GLYPH_5] = '5';
            this.glyphLookupTable[Glyph.GLYPH_6] = '6';
            this.glyphLookupTable[Glyph.GLYPH_7] = '7';
            this.glyphLookupTable[Glyph.GLYPH_8] = '8';
            this.glyphLookupTable[Glyph.GLYPH_9] = '9';

            this.glyphLookupTable[Glyph.GLYPH_EMPTY] = ' ';
            

        }
       
        //Get current line
        public int GetLine() { return this.glyphLine; }
        //Get current pos
        public int GetPosition() { return this.glyphPos; }

        //Return maximum lines
        public int GetMaxLines()
        {
            return this.maxLines;
        }

        //Return maximum characters per line
        public int GetMaxCharacters()
        {
            return this.maxCharacters;
        }
        
        public Glyph[,] GetGlyphs()
        {
            return this.glyphs;
        }

        public Glyph[] Slice(int line,int length)
        {
            if (length > maxCharacters) length = maxCharacters;
            Glyph[] glyphs = new Glyph[length];

            for(int i = 0;i < length;i++)
            {
                glyphs[i] = this.glyphs[line, i];
            }

            return glyphs;
        }

        public String GlyphArrayToString(Glyph[] glyphs)
        {
            String result = String.Empty;

            foreach(Glyph glyph in glyphs)
            {
                if(glyph == Glyph.GLYPH_NULL)
                {
                    continue;
                }
                else
                {
                    result += this.glyphLookupTable[glyph];
                }
            }

            return result;
        }

        //Backspace
        public void GlyphBackspace()
        {
           if(this.glyphPos == 0 && this.glyphLine == 0)
            {
                this.glyphs[0, 0] = Glyph.GLYPH_NULL;
                return;
            }
           else
            {
                if(this.glyphPos == 0)
                {
                    this.glyphLine--;
                    this.glyphPos = this.maxCharacters - 1;
                    this.glyphs[this.glyphLine, this.glyphPos] = Glyph.GLYPH_NULL;
                }
                else
                {
                    this.glyphPos--;
                    this.glyphs[this.glyphLine, this.glyphPos] = Glyph.GLYPH_NULL;
                }
            }
        }

        //Enter key
        public void GlyphEnter()
        {
            this.glyphLine++;
            this.glyphPos = 0;
            if(this.glyphLine >= this.maxLines)
            {
                this.ClearGlpyhs();
            }
        }

        //Reset all glyphs
        public void ClearGlpyhs()
        {
            //Iterate over 'glyphs'
            for(int x = 0; x < this.maxLines;x++)
            {
                for(int y = 0; y < this.maxCharacters;y++)
                {
                    //this is the NULL character - we will use it to indicate that there is no glyph stored here
                    this.glyphs[x,y] = Glyph.GLYPH_NULL;
                }
            }

            //Reset indexes too!
            this.glyphLine  = 0;
            this.glyphPos   = 0;
        }

        public Glyph[] GetSlice(int idx)
        {
            Glyph[] slice = new Glyph[this.maxCharacters];
            
            for(int i = 0;i < maxCharacters;i++)
            {
                slice[i] = glyphs[idx, i];
            }

            return slice;
        }

        //Write a glyph to the list
        public void WriteGlyph(Glyph glyph)
        {
            if (this.glyphPos >= this.maxCharacters)
            {
                glyphLine++;
                this.glyphPos = 0;

                if (this.glyphLine >= this.maxLines)
                {
                    this.ClearGlpyhs();
                    this.glyphPos = 0;
                    this.glyphLine = 0;
                }
                else
                {
                    this.glyphs[glyphLine, glyphPos] = glyph;
                    glyphPos++;
                }
            }
            else
            {
                this.glyphs[glyphLine, glyphPos] = glyph;
                glyphPos++;
            }
        }
    }
}
