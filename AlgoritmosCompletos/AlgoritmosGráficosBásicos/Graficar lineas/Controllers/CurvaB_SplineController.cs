using Graficar_lineas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Controllers
{
    public class CurvaB_SplineController
    {
        private readonly PictureBox pictureBox;
        private Bitmap bitmap;
        private readonly Pen pen;
        private readonly List<PointF> puntosControl;
        private bool isAnimating;
        private bool modoSeleccionPuntos;
        private int puntosRequeridos;
        private readonly CurvaB_Spline curvaBSpline;
        private Color colorActual = Color.Black;
        private int grosorActual = 1;
        private int gradoActual = 2; // Valor por defecto, se actualiza desde el formulario

        public CurvaB_SplineController(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            this.pen = new Pen(Color.Black, 1);
            this.puntosControl = new List<PointF>();
            this.curvaBSpline = new CurvaB_Spline(Graphics.FromImage(bitmap), pen);
            pictureBox.Image = bitmap;
        }

        public void EstablecerColor(Color color)
        {
            if (pen != null)
            {
                pen.Color = color;
                colorActual = color;
            }
        }

        public void EstablecerGrosor(int grosor)
        {
            if (pen != null)
            {
                pen.Width = grosor;
                grosorActual = grosor;
            }
        }

        public void IniciarSeleccionPuntos(int numPuntos)
        {
            if (isAnimating) return;
            
            puntosRequeridos = numPuntos;
            puntosControl.Clear();
            modoSeleccionPuntos = true;
            
            // Limpiar el área de dibujo
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            pictureBox.Image = bitmap;
            pictureBox.Refresh();
        }

        public void AgregarPunto(Point punto)
        {
            if (!modoSeleccionPuntos || puntosControl.Count >= puntosRequeridos) return;
            
            // Agregar el nuevo punto
            puntosControl.Add(punto);
            
            // Crear una copia del bitmap actual
            using (var tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
            using (var g = Graphics.FromImage(tempBitmap))
            {
                // Limpiar el área de dibujo
                g.Clear(Color.White);
                
                // Dibujar líneas de control si hay más de un punto
                if (puntosControl.Count > 1)
                {
                    g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                }
                
                // Dibujar todos los puntos de control
                foreach (var p in puntosControl)
                {
                    g.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);
                }
                
                // Si hay suficientes puntos para dibujar una curva parcial
                if (puntosControl.Count > gradoActual)
                {
                    // Crear una copia temporal de los puntos de control
                    var puntosTemporales = new List<PointF>(puntosControl);
                    
                    // Si no es el último punto, dibujar la curva parcial
                    if (puntosControl.Count < puntosRequeridos)
                    {
                        // Usar el método DibujarCurva con los puntos actuales
                        curvaBSpline.DibujarCurvaParcial(puntosTemporales, gradoActual, pictureBox, tempBitmap);
                    }
                    else
                    {
                        // Si es el último punto, dibujar la curva completa
                        curvaBSpline.DibujarCurva(puntosTemporales, gradoActual, pictureBox, tempBitmap);
                        modoSeleccionPuntos = false;
                    }
                }
                
                // Actualizar el bitmap principal
                using (var gMain = Graphics.FromImage(bitmap))
                {
                    gMain.DrawImage(tempBitmap, 0, 0);
                }
            }
            
            // Actualizar la vista
            pictureBox.Image = bitmap;
            pictureBox.Refresh();
        }

        public void DibujarCurva()
        {
            if (puntosControl.Count < 2) return;
            
            // Obtener el grado del formulario (asumiendo que está en un NumericUpDown)
            int grado = 2; // Valor por defecto, se actualizará desde el formulario
            
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            
            curvaBSpline.DibujarCurva(puntosControl, grado, pictureBox, bitmap);
        }

        public async Task AnimarCurva()
        {
            if (puntosControl.Count <= gradoActual || isAnimating) 
                return;
            
            try
            {
                isAnimating = true;
                
                // Crear un nuevo bitmap para la animación
                Bitmap bmAnimacion = new Bitmap(pictureBox.Width, pictureBox.Height);
                
                // Dibujar el estado actual en el bitmap de animación
                using (Graphics g = Graphics.FromImage(bmAnimacion))
                {
                    g.DrawImage(bitmap, 0, 0);
                }
                
                // Llamar al método de animación
                await curvaBSpline.AnimarCurva(puntosControl, gradoActual, pictureBox, bmAnimacion);
                
                // Actualizar el bitmap principal con el resultado final
                bitmap = new Bitmap(bmAnimacion);
                pictureBox.Image = bitmap;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la animación: {ex.Message}");
            }
            finally
            {
                isAnimating = false;
            }
        }

        public void LimpiarTodo()
        {
            puntosControl.Clear();
            modoSeleccionPuntos = false;
            puntosRequeridos = 0;
            
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            
            pictureBox.Image = bitmap;
            pictureBox.Refresh();
        }
        
        public void ActualizarGrado(int nuevoGrado)
        {
            if (nuevoGrado >= 1 && nuevoGrado <= 5) // Asegurar que el grado esté en un rango razonable
            {
                gradoActual = nuevoGrado;
                
                // Redibujar la curva con el nuevo grado si ya hay suficientes puntos
                if (puntosControl.Count > gradoActual)
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                    }
                    
                    // Volver a dibujar la curva con el nuevo grado
                    DibujarCurva();
                }
            }
        }
    }
}
