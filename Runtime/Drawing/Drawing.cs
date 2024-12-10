using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEditor.Search;
using UnityEngine;

namespace StvDEV.Drawing
{
    /// <summary>
    /// Drawing class.
    /// </summary>
    public static class Drawing 
    {
        /// <summary>
        /// Draws a pixel on the specified texture, limiting it to the specified dimensions at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <param name="x">Pixel X</param>
        /// <param name="y">Pixel Y</param>
        /// <param name="color">Color</param>
        public static void DrawPixel(Texture2D texture, int x, int y, int width, int height, Color color)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return;
            }

            texture.SetPixel(x, ConvertToLeftTop(y, height), color);
        }

        /// <summary>
        /// Draws a pixel at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="x">Pixel X</param>
        /// <param name="y">Pixel Y</param>
        /// <param name="color">Color</param>
        public static void DrawPixel(this Texture2D texture, int x, int y, Color color)
        {
            DrawPixel(texture, x, y, texture.width, texture.height, color);   
        }

        /// <summary>
        /// Draws a line on the specified texture at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="lineRect">Line data as Rect</param>
        /// <param name="color">Color</param>
        public static void DrawLine(this Texture2D texture, RectInt lineRect, Color color)
            => DrawLine(texture, lineRect.min, lineRect.max, color);

        /// <summary>
        /// Draws a line on the specified texture at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startPoint">Start point</param>
        /// <param name="endPoint">End point</param>
        /// <param name="color">Color</param>
        public static void DrawLine(this Texture2D texture, Vector2Int startPoint, Vector2Int endPoint, Color color)
            => DrawLine(texture, startPoint.x, startPoint.y, endPoint.x, endPoint.y, color);

        // Bresenham's Line Algorithm.
        /// <summary>
        /// Draws a line on the specified texture at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startX">Start point X</param>
        /// <param name="startY">Start point Y</param>
        /// <param name="endX">End point X</param>
        /// <param name="endY">Ent point Y</param>
        /// <param name="color">Color</param>
        public static void DrawLine(this Texture2D texture, int startX, int startY, int endX, int endY, Color color)
        {
            int width = texture.width;
            int height = texture.height;

            bool steep = Math.Abs(endY - startY) > Math.Abs(endX - startX);

            if (steep)
            {
                Swap(ref startX, ref startY);
                Swap(ref endX, ref endY);
            }

            if (startX > endX)
            {
                Swap(ref startX, ref endX);
                Swap(ref startY, ref endY);
            }

            int deltaX = endX - startX;
            int deltaY = Math.Abs(endY - startY);

            int error = deltaX / 2;
            int yStep = startY < endY ? 1 : -1;

            int y = startY;

            for (var x = startX; x < endX; x++)
            {
                if (steep)
                {
                    DrawPixel(texture, y, x, width, height, color);
                }
                else
                {
                    DrawPixel(texture, x, y, width, height, color);
                }

                error -= deltaY;

                if (error < 0)
                {
                    y += yStep;
                    error += deltaX;
                }
            }

        }

        /// <summary>
        /// Draws a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="rect">Rectangle</param>
        /// <param name="color">Color</param>
        public static void DrawRectangle(this Texture2D texture, RectInt rect, Color color)
            => DrawRectangle(texture, rect.min, rect.max, color);

        /// <summary>
        /// Draws a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startPoint">Start point</param>
        /// <param name="endPoint">End point</param>
        /// <param name="color">Color</param>
        public static void DrawRectangle(this Texture2D texture, Vector2Int startPoint, Vector2Int endPoint, Color color)
            => DrawRectangle(texture, startPoint.x, startPoint.y, endPoint.x, endPoint.y, color);

        /// <summary>
        /// Draws a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startX">Start point x</param>
        /// <param name="startY">Start point y</param>
        /// <param name="endX">End point x</param>
        /// <param name="endY">End point y</param>
        /// <param name="color">Color</param>
        public static void DrawRectangle(this Texture2D texture, int startX, int startY, int endX, int endY, Color color)
        {
            DrawLine(texture, startX, startY, startX, endY, color);
            DrawLine(texture, startX, endY, endX, endY, color);
            DrawLine(texture, endX, endY, endX, startY, color);
            DrawLine(texture, endX, startY, startX, startY, color);
        }

        /// <summary>
        /// Fills a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="rect">Rectangle</param>
        /// <param name="color">Color</param>
        public static void FillRectangle(this Texture2D texture, RectInt rect, Color color)
            => FillRectangle(texture, rect.min, rect.max, color);

        /// <summary>
        /// Fills a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startPoint">Start point</param>
        /// <param name="endPoint">End point</param>
        /// <param name="color">Color</param>
        public static void FillRectangle(this Texture2D texture, Vector2Int startPoint, Vector2Int endPoint, Color color)
            => FillRectangle(texture, startPoint.x, startPoint.y, endPoint.x, endPoint.y, color);

        /// <summary>
        /// Fills a rectangle at the specified coordinates.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startX">Start point X</param>
        /// <param name="startY">Start point Y</param>
        /// <param name="endX">End point X</param>
        /// <param name="endY">End point Y</param>
        /// <param name="color">Color</param>
        public static void FillRectangle(this Texture2D texture, int startX, int startY, int endX, int endY, Color color)
        {
            if (startX > endX)
            {
                Swap(ref startX, ref endX);
            }

            if (startY > endY)
            {
                Swap(ref startY, ref endY);
            }

            int width = endX - startX;
            int height = endY - startY;

            startY = ConvertToLeftTop(startY, texture.height) - height;

            Color32[] colors = new Color32[width * height];
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }

            texture.SetPixels32(startX, startY, width, height, colors);
        }

        /// <summary>
        /// Draws a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="center">Circle center</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void DrawCircle(this Texture2D texture, Vector2Int center, int radius, Color color)
            => DrawCircle(texture, center.x, center.y, radius, color);

        /// <summary>
        /// Draws a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="centerX">Circle center X</param>
        /// <param name="centerY">Circle center Y</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void DrawCircle(this Texture2D texture, int centerX, int centerY, int radius, Color color)
            => DrawArc(texture, centerX, centerY, radius, 0, 360, color);

        /// <summary>
        /// Fills a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="center">Circle center</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void FillCircle(this Texture2D texture, Vector2Int center, int radius, Color color)
            => FillCircle(texture, center.x, center.y, radius, color);

        /// <summary>
        /// Fills a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="centerX">Circle center X</param>
        /// <param name="centerY">Circle center Y</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void FillCircle(this Texture2D texture, int centerX, int centerY, int radius, Color color)
            => FillPie(texture, centerX, centerY, radius, 0, 360, color);

        /// <summary>
        /// Draws a part of a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="center">Circle center</param>
        /// <param name="startAngle">Part start angle [0; 360]</param>
        /// <param name="endAngle">Part end angle [0; 360]</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void DrawArc(this Texture2D texture, Vector2Int center, int radius, int startAngle, int endAngle,  Color color)
            => DrawArc(texture, center.x, center.y, radius, startAngle, endAngle, color);

        /// <summary>
        /// Draws a part of a circle with a specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="centerX">Circle center X</param>
        /// <param name="centerY">Circle center Y</param>
        /// <param name="startAngle">Part start angle [0; 360]</param>
        /// <param name="endAngle">Part end angle [0; 360]</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void DrawArc(this Texture2D texture, int centerX, int centerY, int radius, int startAngle, int endAngle, Color color)
            => DrawSectors(texture, centerX, centerY, radius, startAngle, endAngle,  color);

        /// <summary>
        /// Fills in the part of the circle with the specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="center">Circle center</param>
        /// <param name="startAngle">Part start angle [0; 360]</param>
        /// <param name="endAngle">Part end angle [0; 360]</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void FillPie(this Texture2D texture, Vector2Int center, int radius, int startAngle, int endAngle, Color color)
            => FillPie(texture, center.x, center.y, radius, startAngle, endAngle, color);

        /// <summary>
        /// Fills in the part of the circle with the specified radius.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="centerX">Circle center X</param>
        /// <param name="centerY">Circle center Y</param>
        /// <param name="startAngle">Part start angle [0; 360]</param>
        /// <param name="endAngle">Part end angle [0; 360]</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="color">Color</param>
        public static void FillPie(this Texture2D texture, int centerX, int centerY, int radius, int startAngle, int endAngle, Color color)
            => DrawSectors(texture, centerX, centerY, radius, startAngle, endAngle, color, true);

        private enum SectorType
        {
            NotDraw,
            Start,
            Draw,
            End,
            StartAndEnd
        }

        private const double RADIANTS_TO_DEGREE = 180 / Math.PI; 
        
        private static void DrawSectors(this Texture2D texture, int centerX, int centerY, int radius,  int startAngle, int endAngle,  Color color, bool filled = false)
        {
            int width = texture.width;
            int height = texture.height;

            if (radius < 0)
            {
                radius = Math.Abs(radius);
            }

            SectorType[] arcSectors = new SectorType[8];

            for (var i = 0; i < 8; i++)
            {
                arcSectors[i] = SectorType.NotDraw;
            }

            if (startAngle < 0)
            {
                startAngle = 360 + (startAngle % 360);
            }

            if (endAngle < 0)
            {
                endAngle = 360 + (endAngle % 360);
            }

            int startSector = (startAngle / 45);
            int endSector = (endAngle / 45);

            if (startSector == endSector)
            {
                arcSectors[startSector] = SectorType.StartAndEnd;
            }
            else
            {
                for (var i = startSector; i < (endSector > startSector ? Math.Min(endSector, 8) : 8); i++)
                {
                    arcSectors[i] = SectorType.Draw;
                }

                bool round = false;
                
                if (endSector >= 8)
                {
                    round = true;
                }

                endSector %= 8;

                if ((startSector > endSector) || (startSector == endSector && round))
                {
                    for (var i = 0; i < endSector; i++)
                    {
                        arcSectors[i] = SectorType.Draw;
                    }
                }

                if (startSector != endSector)
                {
                    arcSectors[startSector] = SectorType.Start;
                    arcSectors[endSector] = SectorType.End;
                }

            }

            int x = 0;
            int y = radius;
            int startPoint = (int)(centerX + radius * Math.Cos(startAngle / RADIANTS_TO_DEGREE));
            int endPoint = (int)(centerX + radius * Math.Cos(endAngle / RADIANTS_TO_DEGREE));

            int radiusError = Math.Abs(2 * (1 - radius));

            while (y > x)
            {
                DrawNegativeSectorPoint(centerX + y, centerY + x, arcSectors[0], startPoint, endPoint);
                DrawNegativeSectorPoint(centerX + x, centerY + y, arcSectors[1], startPoint, endPoint);
                DrawNegativeSectorPoint(centerX - x, centerY + y, arcSectors[2], startPoint, endPoint);
                DrawNegativeSectorPoint(centerX - y, centerY + x, arcSectors[3], startPoint, endPoint);
                DrawPositiveSectorPoint(centerX - y, centerY - x, arcSectors[4], startPoint, endPoint);
                DrawPositiveSectorPoint(centerX - x, centerY - y, arcSectors[5], startPoint, endPoint);
                DrawPositiveSectorPoint(centerX + x, centerY - y, arcSectors[6], startPoint, endPoint);
                DrawPositiveSectorPoint(centerX + y, centerY - x, arcSectors[7], startPoint, endPoint);

                if (radiusError + y > 0)
                {
                    y--;
                    radiusError = radiusError - 2 * y + 1;
                }
                else
                {
                    if ( x > radiusError)
                    {
                        x++;
                        radiusError = radiusError + 2 * x + 1;
                    }
                }
            }

            if (filled)
            {

                var cosRadius = radius * Math.Cos(45 / RADIANTS_TO_DEGREE);
                var sinRadius = radius * Math.Sin(45 / RADIANTS_TO_DEGREE);

                DrawNegativeSectorPoint((int)(centerX + cosRadius), (int)(centerY + sinRadius), arcSectors[0], startPoint, endPoint);
                DrawNegativeSectorPoint((int)(centerX - cosRadius), (int)(centerY + sinRadius) + 1, arcSectors[2], startPoint, endPoint);
                DrawPositiveSectorPoint((int)(centerX - cosRadius), (int)(centerY - sinRadius), arcSectors[4], startPoint, endPoint);
                DrawPositiveSectorPoint((int)(centerX + cosRadius) + 1, (int)(centerY - sinRadius), arcSectors[6], startPoint, endPoint);

            }

            void DrawPositiveSectorPoint(int pointX, int pointY, SectorType sector, int startPoint, int endPoint)
            {
                switch(sector)
                {
                    case SectorType.NotDraw:
                        return;
                    case SectorType.Draw:
                        DrawPoint();
                        return;
                    case SectorType.Start:
                        if (pointX >= startPoint)
                        {
                            DrawPoint();
                        }
                        return;
                    case SectorType.End:
                        if (pointX <= endPoint)
                        {
                            DrawPoint();
                        }
                        return;
                    case SectorType.StartAndEnd:
                        if (pointX >= startPoint && pointX <= endPoint)
                        {
                            DrawPoint();
                        }
                        return;
                }

                void DrawPoint()
                {
                    DrawPixel(texture, pointX, pointY, width, height, color);
                    if (filled)
                    {
                        DrawLine(texture, centerX, centerY, pointX, pointY, color);
                    }
                }
            }

            void DrawNegativeSectorPoint(int pointX, int pointY, SectorType sector, int startPoint, int endPoint)
            {
                switch (sector)
                {
                    case SectorType.NotDraw:
                        return;
                    case SectorType.Draw:
                        DrawPoint();
                        return;
                    case SectorType.Start:
                        if (pointX <= startPoint)
                        {
                            DrawPoint();
                        }
                        return;
                    case SectorType.End:
                        if (pointX >= endPoint)
                        {
                            DrawPoint();
                        }
                        return;
                    case SectorType.StartAndEnd:
                        if (pointX <= startPoint && pointX >= endPoint)
                        {
                            DrawPoint();
                        }
                        return;
                }

                void DrawPoint()
                {
                    DrawPixel(texture, pointX, pointY, width, height, color);
                    if (filled)
                    {
                        DrawLine(texture, centerX, centerY, pointX, pointY, color);
                    }
                }
            }

        }

        /// <summary>
        /// Fills an area of one color with another color before the color changes.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="start">Start point</param>
        /// <param name="color">Color</param>
        public static void FloodFill(this Texture2D texture, Vector2Int start, Color color)
            => FloodFill(texture, start.x, start.y, color);

        /// <summary>
        /// Fills an area of one color with another color before the color changes.
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="startX">Start point X</param>
        /// <param name="startY">Start point Y</param>
        /// <param name="color">Color</param>
        public static void FloodFill(this Texture2D texture, int startX, int startY, Color color)
        {
            int width = texture.width;
            int height = texture.height;
            startY = ConvertToLeftTop(startY, height);

            Color32 newColor = color;

            Vector2Int start = new(startX, startY);

            Color32[] image = texture.GetPixels32();

            Color32 originalColor = texture.GetPixel(startX, startY);

            if (originalColor.ColorEquals(newColor))
            {
                return;
            }

            image[startX + startY * width] = newColor;

            Queue<Vector2Int> nodes = new();
            nodes.Enqueue(start);

            var i = 0;
            var size = width * height;

            while (nodes.Count > 0 && i <= size)
            {
                i++;

                Vector2Int current = nodes.Dequeue();
                int x = current.x;
                int y = current.y;

                int index;

                if (x > 0)
                {
                    index = x - 1 + y * width;
                    if (image[index].ColorEquals(originalColor))
                    {
                        image[index] = newColor;
                        nodes.Enqueue(new Vector2Int(x - 1, y));
                    }
                }

                if (x < width - 1 )
                {
                    index = x + 1 + y * width;
                    if (image[index].ColorEquals(originalColor))
                    {
                        image[index] = newColor;
                        nodes.Enqueue(new Vector2Int(x + 1, y));
                    }
                }

                if (y > 0)
                {
                    index = x + (y - 1) * width;
                    if (image[index].ColorEquals(originalColor))
                    {
                        image[index] = newColor;
                        nodes.Enqueue(new Vector2Int(x, y - 1));
                    }
                }

                if (y < height - 1)
                {
                    index = x + (y + 1) * width;
                    if (image[index].ColorEquals(originalColor))
                    {
                        image[index] = newColor;
                        nodes.Enqueue(new Vector2Int(x, y + 1));
                    }
                }
            }

            texture.SetPixels32(image);
        }

        private static void Swap(ref int x, ref int y)
        {
            x += y;
            y = x - y;
            x -= y;
        }

        private static int ConvertToLeftTop(int y, int height)
        {
            return height - y;
        }
    
        private static bool ColorEquals(this Color32 color1, Color32 color2)
        {
            return color1.a == color2.a
                    && color1.r == color2.r
                    && color1.g == color2.g
                    && color1.b == color2.b;
        }
    }
}
