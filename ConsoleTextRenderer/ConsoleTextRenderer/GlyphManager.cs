using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    class GlyphManager
    {
        //'glyphPos' and 'glyphLine' are indexes for the 'glyphs' array
        //holds current glpyh position in the current line
        private int glyphPos = 0;
        //holds current glyph line
        private int glyphLine = 0;
        //this is the array that contains the glpyh data
        private Glyph[][] glyphs = null;
        //max lines
        private int maxLines = 0;
        //max characters per line
        private int maxCharacters = 0;

        //Constructor
        public GlyphManager(int lines,int characters)
        {
            this.maxCharacters  = characters;
            this.maxLines       = lines;
            this.ClearGlpyhs();
        }

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

        //Reset all glyphs
        public void ClearGlpyhs()
        {
            //Iterate over 'glyphs'
            for(int x = 0; x < this.maxLines;x++)
            {
                for(int y = 0; y < this.maxCharacters;y++)
                {
                    //this is the NULL character - we will use it to indicate that there is no glyph stored here
                    this.glyphs[x][y] = Glyph.GLYPH_EMPTY;
                }
            }

            //Reset indexes too!
            this.glyphLine  = 0;
            this.glyphPos   = 0;
        }

        //Write a glyph
        public void WriteChar(char character)
        {
            this.glyphs[glyphLine][glyphPos++] = new Glyph(character);
            if(glyphPos >= this.maxCharacters)
            {
                glyphLine++;
            }
            if(glyphLine >= this.maxLines)
            {
                //What do we do with an overflow? I don't know!
                //Clear it!
                //For now...
                this.ClearGlpyhs();
            }
        }

        //Basically a wrapper for WriteChar
        public void WriteString(String characters)
        {
            foreach(char character in characters)
            {
                this.WriteChar(character);
            }
        }

    }
}
