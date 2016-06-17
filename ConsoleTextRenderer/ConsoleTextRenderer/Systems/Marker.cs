using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class Marker : Entity
    {
        //Position
        public float x = 0.0f;
        public float y = 0.0f;
        //Blink Rate Per Second
        public long bps = 2;
        //Flag used to render our blinker
        private bool render = true;
        //reference to our GlyphManager so we know where to put our blinker
        private GlyphManager glyphManagerRef;
        //previous time
        private long prevTick = 0;

        public Marker(float _x,float _y,long _bps,ref Systems.GlyphManager _glyphManagerRef)
        {
            this.x = _x;
            this.y = _y;
            this.bps = _bps;
            this.prevTick = DateTime.Now.Ticks;
            this.glyphManagerRef = _glyphManagerRef;
        }

        public void Update()
        {
            long currentTick = DateTime.Now.Ticks;
            if (currentTick - this.prevTick > (10000000 / bps))
            {
                this.render = !this.render;
                this.prevTick = currentTick;
            }
        }

        public RenderManager getRenderManager()
        {
            return RenderManager.marker_renderManager;
        }
    }
}
