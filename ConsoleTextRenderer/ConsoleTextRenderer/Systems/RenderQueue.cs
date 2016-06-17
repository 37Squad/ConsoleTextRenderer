using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Render
{
    class RenderQueue
    {
        private List<KeyValuePair<object, Renderable.RenderObject>> renderQueue = null;
        private RenderEngine renderEngineReference = null;

        public RenderQueue(ref RenderEngine renderEngine)
        {
            //Render Queue
            this.renderQueue = new List<KeyValuePair<object, Renderable.RenderObject>>();
            //Grab a ref
            this.renderEngineReference = renderEngine;
        }

        //Add a pair
        public void AddPair(object renderableObject,Renderable.RenderObject function)
        {
            this.renderQueue.Add(new KeyValuePair<object, Renderable.RenderObject>(renderableObject, function));
        }

        //Clear this queue
        public void ClearQueue()
        {
            this.renderQueue.Clear();
        }

        //Render this lovely queue
        public void RenderQueued()
        {
            //Grab each pair
            foreach(var pair in this.renderQueue)
            {
                //Invoke the render function supplied, with the Key as the parameter of the render function... lovely
                pair.Value(ref this.renderEngineReference, pair.Key);
            }
        }
    }
}
