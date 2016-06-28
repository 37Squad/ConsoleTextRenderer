using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Systems
{
    class Flicker
    {
        private int tickCount = 0;
        private float scanlineY = 0.0f;

        public Flicker()
        {

        }

        public void Update()
        {
            this.tickCount++;
            this.scanlineY += 0.0025f;
            if(this.scanlineY >= 2.0f)
            {
                this.scanlineY = 0.0f;
            }
        }

        public int GetTickCount()
        {
            return this.tickCount;
        }

        public float GetScanline()
        {
            return this.scanlineY;
        }
    }
}
