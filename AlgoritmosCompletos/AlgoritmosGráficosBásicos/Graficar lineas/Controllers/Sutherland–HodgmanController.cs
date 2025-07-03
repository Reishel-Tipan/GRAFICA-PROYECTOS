using Graficar_lineas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Controllers
{
    public class Sutherland_HodgmanController
    {
        private PictureBox pictureBox;
        private Graphics graphics;
        private Bitmap bitmap;
        private Sutherland_Hodgman clipper;
        private Rectangle clipWindow;
        private List<Point> polygonPoints;
        private bool isDrawingPolygon = false;
        private bool isClipWindowSet = false;
        private List<Point> clippedPolygon;

        public Sutherland_HodgmanController(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            InitializeGraphics();
            polygonPoints = new List<Point>();
            clippedPolygon = new List<Point>();
        }

        private void InitializeGraphics()
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox.Image = bitmap;
        }

        public void SetClipWindow(Rectangle window)
        {
            clipWindow = window;
            clipper = new Sutherland_Hodgman(clipWindow);
            isClipWindowSet = true;
            DrawClipWindow();
        }

        public void AddPolygonPoint(Point point)
        {
            if (!isClipWindowSet) return;

            polygonPoints.Add(point);
            DrawPolygon();
        }

        public void CompletePolygon()
        {
            if (polygonPoints.Count < 3) return;
            
            // Cerrar el polígono si no está cerrado
            if (polygonPoints[0] != polygonPoints[polygonPoints.Count - 1])
            {
                polygonPoints.Add(polygonPoints[0]);
            }
            
            DrawPolygon();
            isDrawingPolygon = false;
        }

        public void ClipPolygon()
        {
            if (polygonPoints.Count < 3 || !isClipWindowSet) return;

            // Asegurarse de que el polígono esté cerrado
            if (polygonPoints[0] != polygonPoints[polygonPoints.Count - 1])
            {
                polygonPoints.Add(polygonPoints[0]);
            }

            // Realizar el recorte
            clippedPolygon = clipper.ClipPolygon(polygonPoints);
            
            // Dibujar el resultado
            DrawClippedPolygon();
        }

        private void DrawClipWindow()
        {
            using (var pen = new Pen(Color.Blue, 2))
            {
                graphics.DrawRectangle(pen, clipWindow);
            }
            pictureBox.Invalidate();
        }

        private void DrawPolygon()
        {
            // Limpiar y volver a dibujar todo
            graphics.Clear(Color.White);
            DrawClipWindow();

            // Dibujar el polígono actual
            if (polygonPoints.Count > 1)
            {
                using (var pen = new Pen(Color.Green, 2))
                {
                    graphics.DrawLines(pen, polygonPoints.ToArray());
                }
            }

            // Dibujar el polígono recortado si existe
            if (clippedPolygon != null && clippedPolygon.Count > 1)
            {
                using (var pen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    graphics.DrawLines(pen, clippedPolygon.ToArray());
                }
            }

            pictureBox.Invalidate();
        }

        private void DrawClippedPolygon()
        {
            // Limpiar y volver a dibujar todo
            graphics.Clear(Color.White);
            DrawClipWindow();

            // Dibujar el polígono original
            if (polygonPoints.Count > 1)
            {
                using (var pen = new Pen(Color.Green, 1))
                {
                    graphics.DrawLines(pen, polygonPoints.ToArray());
                }
            }

            // Dibujar el polígono recortado
            if (clippedPolygon != null && clippedPolygon.Count > 1)
            {
                using (var brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0)))
                using (var pen = new Pen(Color.Red, 2))
                {
                    graphics.FillPolygon(brush, clippedPolygon.ToArray());
                    graphics.DrawPolygon(pen, clippedPolygon.ToArray());
                }
            }

            pictureBox.Invalidate();
        }

        public void Clear()
        {
            graphics.Clear(Color.White);
            polygonPoints.Clear();
            clippedPolygon.Clear();
            isClipWindowSet = false;
            pictureBox.Invalidate();
        }

        public void StartDrawingPolygon()
        {
            if (!isClipWindowSet) return;
            isDrawingPolygon = true;
            polygonPoints.Clear();
            clippedPolygon.Clear();
        }

        public bool IsDrawingPolygon => isDrawingPolygon;
    }
}
