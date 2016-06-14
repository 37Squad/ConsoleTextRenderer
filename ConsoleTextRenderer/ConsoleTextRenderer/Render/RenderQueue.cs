using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer.Render
{
    class RenderQueue
    {
        //List of renderable entities
        List<Systems.Entity> renderableEntities;
        public RenderQueue()
        {
            this.renderableEntities = new List<Systems.Entity>();
        }

        public void AddEntity(Systems.Entity entity)
        {
            this.renderableEntities.Add(entity);
        }

        public void ClearList()
        {
            this.renderableEntities.Clear();
        }

        public void RenderAll()
        {
            foreach(Systems.Entity entity in this.renderableEntities)
            {
                entity.getRenderManager().renderer.Render(entity);
            }
        }
    }
}
