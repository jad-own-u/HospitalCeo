﻿using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Console;

/*
 * Brett Taylor
 * Commands for the console
 */

namespace HospitalCeo.Utils
{
    public class Commands
    {
        [Command("camera-pos", "Prints of the camera's position and zoom as well as the width and height of the screen.")]
        private static void printCameraPosition()
        {
            DebugConsole.instance.log("Camera position: " + InputManager.camera.position + ", zoom: " + InputManager.camera.zoom + ", screen width: " + Screen.width + ", screen height: :" + Screen.height);
        }

        [Command("camera-reset", "Resets the camera to (0, 0)")]
        private static void cameraReset()
        {
            InputManager.camera.position = new Vector2(0, 0);
        }

        [Command("building", "Starts Buidling")]
        private static void startBuilding(string buildingName)
        {
            Type type = Type.GetType(buildingName);
            if (type == null)
            {
                DebugConsole.instance.log(buildingName + " not a valid building");
                return;
            }
            Building.BuildingController.StartBuilding(type);
        }


        [Command("tile-under-cursor", "Prints the tile's position and the building on top of it. AS well as draws a box around it")]
        private static void tileUnderCursor()
        {
            World.Tile t = World.WorldController.GetMouseOverTile();
            if (t == null)
            {
                DebugConsole.instance.log("No tile under cursor");
                return;
            }

            DebugConsole.instance.log(t);
            DebugConsole.instance.log("Infrastructure Building: " + t.GetInfrastructureItem());
            DebugConsole.instance.log("Gameplay Building: " + t.GetGameplayItem());
        }

        [Command("draw-tile-under-cursor", "Draws the tile's number and position on screen")]
        private static void drawTileUnderCursor()
        {
            if (!Core.debugRenderEnabled)
            {
                DebugConsole.instance.log("Will not draw unless debug-renderer is turned on");
                DebugConsole.instance.Open();
            }

            World.TileRenderer.drawTileInformation = !World.TileRenderer.drawTileInformation;
        }

        [Command("draw-tile-infrastructure-status", "Draws a square on each tile indicating its infrastructure status")]
        private static void drawTileInfrastructureStatus()
        {
            if (!Core.debugRenderEnabled)
            {
                DebugConsole.instance.log("Will not draw unless debug-renderer is turned on");
                DebugConsole.instance.Open();
            }

            World.TileSprite.drawInfrastructureStatus = !World.TileSprite.drawInfrastructureStatus;
        }
    }
}
