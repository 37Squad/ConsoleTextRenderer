using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTextRenderer
{
    class RenderQueue
    {
        //List of renderable entities
        List<Entity> renderableEntities;
        public RenderQueue()
        {
            this.renderableEntities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            this.renderableEntities.Add(entity);
        }

        public void ClearList()
        {
            this.renderableEntities.Clear();
        }

        public void RenderAll()
        {
            foreach(Entity entity in this.renderableEntities)
            {
                entity.getRenderManager().renderer.Render(entity);
            }
        }
    }
}
