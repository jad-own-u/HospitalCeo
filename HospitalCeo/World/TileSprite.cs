﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

/* 
 * Brett Taylor
 * Inherits sprite
 * Disables the normal debug render
 * When there are 10,000+ tiles, debug render will freeze the game
 */

namespace HospitalCeo.World
{
    public class TileSprite : Sprite
    {
        public static bool DRAW_INFRASTRUCTURE_STATUS = false;
        public static bool DRAW_GAMEPLAY_STATUS = false;
        public static bool DRAW_PATHFIND_STATUS = false;
        private Tile tile;

        public TileSprite(Tile tile, Subtexture texture) : base(texture)
        {
            this.tile = tile;
            this.setMaterial(new Material(BlendState.AlphaBlend));
            this.renderLayer = Utils.RenderLayers.TILE;
        }

        public override void debugRender(Graphics graphics)
        {
            if (!isVisibleFromCamera(WorldController.SCENE.camera))
                return;

            if (DRAW_INFRASTRUCTURE_STATUS)
            {
                Color color;
                if (tile.GetInfrastructureItem() != null) color = new Color(Color.Blue, 0.1f);
                else color = new Color(Color.Red, 0.1f);
                graphics.batcher.drawRect(new Rectangle((int)tile.GetPosition().X - 20, (int)tile.GetPosition().Y - 10, 20, 20), color);
            }

            if (DRAW_GAMEPLAY_STATUS)
            {
                Color color;
                if (tile.GetGameplayItem() != null) color = new Color(Color.Green, 0.1f);
                else color = new Color(Color.Purple, 0.1f);
                graphics.batcher.drawRect(new Rectangle((int)tile.GetPosition().X + 20, (int)tile.GetPosition().Y - 10, 20, 20), color);
            }

            if (DRAW_PATHFIND_STATUS)
            {
                if (!tile.CanPathfindTo())
                    return;

                Pathfinding.PathfindingNode<Tile> node = tile.GetPathfindNode();
                graphics.batcher.drawRect(new Rectangle((int)tile.GetPosition().X - 10, (int)tile.GetPosition().Y - 10, 20, 20), new Color(Color.HotPink, 0.5f));

                foreach (Pathfinding.PathfindingEdge<Tile> edge in node.Edges)
                {
                    if (edge != null)
                        graphics.batcher.drawLine(tile.GetPosition(), edge.PathNode.NodeData.GetPosition(), new Color(Color.OrangeRed, 0.5f), thickness: 4);
                }
            }
        }
    }
}
