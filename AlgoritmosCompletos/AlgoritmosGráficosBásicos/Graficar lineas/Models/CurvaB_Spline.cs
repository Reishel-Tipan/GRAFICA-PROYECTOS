using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class CurvaB_Spline : IDisposable
    {
        private Graphics graphics;
        private Pen pen;
        private bool mostrarPuntosControl = true;
        private bool mostrarLineasControl = true;
        private bool disposed = false;
        
        // Implementación de IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos administrados
                    pen?.Dispose();
                    // No se debe liberar graphics aquí ya que no lo creamos nosotros
                }
                
                // Liberar recursos no administrados
                
                disposed = true;
            }
        }
        
        ~CurvaB_Spline()
        {
            Dispose(false);
        }

        public CurvaB_Spline(Graphics graphics, Pen pen)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this.pen = pen ?? throw new ArgumentNullException(nameof(pen));
            
            if (this.graphics != null)
            {
                this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        public void ActualizarLapiz(Pen nuevoPen)
        {
            if (nuevoPen == null) return;
            
            // Liberar el lápiz anterior si existe
            if (pen != null)
            {
                pen.Dispose();
            }
            
            // Asignar el nuevo lápiz
            pen = (Pen)nuevoPen.Clone();
        }

        // Método para calcular el valor de la base B-spline (versión optimizada)
        private double BaseBSpline(int i, int k, double t, List<double> nodos)
        {
            // Validaciones básicas
            if (nodos == null || nodos.Count == 0 || i < 0 || k <= 0)
                return 0.0;

            // Para órdenes superiores, usar el algoritmo de Cox-de Boor
            if (k > 1)
            {
                double resultado = 0.0;
                double den1 = nodos[i + k - 1] - nodos[i];
                double den2 = (i + k < nodos.Count) ? (nodos[i + k] - nodos[i + 1]) : 0;

                // Primer término
                if (den1 > 0)
                {
                    double coef1 = (t - nodos[i]) / den1;
                    resultado += coef1 * BaseBSpline(i, k - 1, t, nodos);
                }

                // Segundo término
                if (den2 > 0 && (i + 1) < nodos.Count)
                {
                    double coef2 = (nodos[i + k] - t) / den2;
                    resultado += coef2 * BaseBSpline(i + 1, k - 1, t, nodos);
                }

                return resultado;
            }
            else // k == 1
            {
                // Base de primer orden (función escalón)
                if (i < nodos.Count - 1 && t >= nodos[i] && t < nodos[i + 1])
                    return 1.0;
                
                // Manejar el caso del último nodo (incluirlo)
                if (i == nodos.Count - 2 && Math.Abs(t - nodos[i + 1]) < 1e-10)
                    return 1.0;
                    
                return 0.0;
            }
        }

        // Genera un vector de nodos uniforme para B-spline abierta
        private List<double> GenerarVectorNodos(int numPuntos, int grado)
        {
            var nodos = new List<double>();
            
            // Validación de parámetros
            if (numPuntos <= 0 || grado <= 0)
                return new List<double> { 0, 0, 1, 1 }; // Nodos predeterminados
                
            // Asegurar que el grado sea válido
            if (grado >= numPuntos)
                grado = numPuntos - 1;
                
            // Para curvas B-spline abiertas, necesitamos n + k + 1 nodos
            // donde n = numPuntos - 1 (índice del último punto)
            // y k = grado + 1 (orden de la curva)
            int n = numPuntos - 1;
            int k = grado + 1;
            int numNodos = n + k + 1;
            
            // Crear nodos uniformemente espaciados
            for (int i = 0; i < numNodos; i++)
            {
                // Los primeros 'k' nodos son 0
                if (i < k)
                {
                    nodos.Add(0);
                }
                // Los últimos 'k' nodos son 1
                else if (i > n)
                {
                    nodos.Add(1);
                }
                // Nodos intermedios uniformemente espaciados
                else
                {
                    double t = (double)(i - k + 1) / (n - k + 2);
                    nodos.Add(Math.Max(0, Math.Min(1, t))); // Asegurar que esté entre 0 y 1
                }
            }
            
            // Asegurar que el primer y último nodo sean 0 y 1 respectivamente
            if (nodos.Count > 0)
            {
                nodos[0] = 0;
                nodos[nodos.Count - 1] = 1;
            }
            
            return nodos;
        }

        // Dibuja la curva B-spline parcial mientras se agregan puntos
        public void DibujarCurvaParcial(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bitmap, int resolucion = 100)
        {
            if (puntosControl == null || puntosControl.Count < 2 || bitmap == null || pictureBox == null)
                return;

            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.White);

                    // Dibujar líneas de control si está habilitado
                    if (mostrarLineasControl && puntosControl.Count > 1)
                    {
                        using (var penLinea = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dot })
                        {
                            g.DrawLines(penLinea, puntosControl.ToArray());
                        }
                    }

                    // Dibujar puntos de control si está habilitado
                    if (mostrarPuntosControl)
                    {
                        using (var brush = new SolidBrush(Color.Red))
                        {
                            foreach (var punto in puntosControl)
                            {
                                g.FillEllipse(brush, punto.X - 3, punto.Y - 3, 6, 6);
                                g.DrawEllipse(Pens.DarkRed, punto.X - 3, punto.Y - 3, 6, 6);
                            }
                        }
                    }

                    // Si hay suficientes puntos para dibujar una curva
                    // Necesitamos al menos grado + 1 puntos para una curva B-Spline
                    if (puntosControl.Count > grado)
                    {
                        // Asegurar que el grado no sea mayor que el número de puntos - 1
                        int gradoActual = Math.Min(grado, puntosControl.Count - 1);
                        
                        // Generar vector de nodos
                        var nodos = GenerarVectorNodos(puntosControl.Count, gradoActual);
                        
                        // Calcular y dibujar la curva
                        var puntosCurva = new List<PointF>();
                        
                        // Calcular puntos de la curva con más resolución para una curva más suave
                        for (int i = 0; i <= resolucion; i++)
                        {
                            double t = (double)i / resolucion;
                            puntosCurva.Add(CalcularPuntoBSpline(t, puntosControl, gradoActual, nodos));
                        }
                        
                        // Dibujar la curva
                        if (puntosCurva.Count > 1)
                        {
                            g.DrawCurve(pen, puntosCurva.ToArray());
                        }
                    }
                }

                // Actualizar el PictureBox de forma segura
                if (pictureBox.IsHandleCreated)
                {
                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.BeginInvoke(new Action(() => 
                        {
                            pictureBox.Image = bitmap;
                            pictureBox.Refresh();
                        }));
                    }
                    else
                    {
                        pictureBox.Image = bitmap;
                        pictureBox.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                // Registrar el error o mostrarlo de alguna manera
                Console.WriteLine($"Error en DibujarCurvaParcial: {ex.Message}");
            }
        }

        // Dibuja la curva B-spline completa
        public void DibujarCurva(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bitmap, int resolucion = 200)
        {
            // Validaciones básicas
            if (puntosControl == null || puntosControl.Count < 2 || bitmap == null || pictureBox == null)
                return;

            // Fijar grado a 3 máximo para suavidad y control
            grado = Math.Min(3, puntosControl.Count - 1);

            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.White);

                    // Dibujar líneas de control (opcional)
                    if (mostrarLineasControl && puntosControl.Count > 1)
                    {
                        using (var penLinea = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dot })
                        {
                            g.DrawLines(penLinea, puntosControl.ToArray());
                        }
                    }

                    // Generar vector de nodos para B-Spline abierta
                    var nodos = GenerarVectorNodos(puntosControl.Count, grado);

                    // Calcular puntos de la curva con alta resolución
                    List<PointF> puntosCurva = new List<PointF>();

                    // Rango de t basado en nodos para curva abierta
                    double tMin = nodos[grado];
                    double tMax = nodos[puntosControl.Count];

                    for (int i = 0; i <= resolucion; i++)
                    {
                        double t = tMin + (tMax - tMin) * i / resolucion;
                        t = Math.Max(tMin, Math.Min(tMax - 1e-10, t));

                        PointF punto = CalcularPuntoBSpline(t, puntosControl, grado, nodos);
                        puntosCurva.Add(punto);
                    }

                    // Dibujar la curva con líneas entre puntos calculados (más precisa)
                    if (puntosCurva.Count > 1)
                    {
                        g.DrawLines(pen, puntosCurva.ToArray());
                    }

                    // Opcional: dibujar puntos de control en rojo
                    if (mostrarPuntosControl)
                    {
                        foreach (var punto in puntosControl)
                        {
                            g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                            g.DrawEllipse(Pens.DarkRed, punto.X - 3, punto.Y - 3, 6, 6);
                        }
                    }
                }

                // Actualizar imagen en el PictureBox de forma segura
                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(new Action(() =>
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();
                    }));
                }
                else
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Bitmap)bitmap.Clone();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DibujarCurva corregido: {ex.Message}");
            }
        }


        // Calcula un punto en la curva B-spline para un parámetro t dado
        private PointF CalcularPuntoBSpline(double t, List<PointF> puntosControl, int grado, List<double> nodos)
        {
            try
            {
                // Validaciones de entrada
                if (puntosControl == null || puntosControl.Count == 0)
                    return new PointF(0, 0);

                // Asegurar que t esté en el rango [0,1]
                t = Math.Max(0, Math.Min(1, t));

                // Para 2 puntos, simplemente interpolar linealmente
                if (puntosControl.Count == 2)
                {
                    return new PointF(
                        (float)(puntosControl[0].X * (1 - t) + puntosControl[1].X * t),
                        (float)(puntosControl[0].Y * (1 - t) + puntosControl[1].Y * t)
                    );
                }

                // Para más de 2 puntos, calcular la combinación convexa
                double x = 0, y = 0;
                double suma = 0;
                
                // Asegurarse de que los índices estén dentro de los límites
                int numPuntos = puntosControl.Count;
                for (int i = 0; i < numPuntos; i++)
                {
                    try
                    {
                        double baseBSpline = BaseBSpline(i, grado + 1, t, nodos);
                        // Solo sumar si el valor de la base es significativo
                        if (Math.Abs(baseBSpline) > 1e-10)
                        {
                            x += (puntosControl[i].X * baseBSpline);
                            y += (puntosControl[i].Y * baseBSpline);
                            suma += baseBSpline;
                        }
                    }
                    catch
                    {
                        // Si hay un error en el cálculo de la base, continuar con el siguiente punto
                        continue;
                    }
                }

                // Si la suma es cero o muy pequeña, devolver un punto intermedio seguro
                if (Math.Abs(suma) < 1e-10)
                {
                    // Calcular el centroide de los puntos de control como fallback
                    double cx = 0, cy = 0;
                    foreach (var punto in puntosControl)
                    {
                        cx += punto.X;
                        cy += punto.Y;
                    }
                    return new PointF(
                        (float)(cx / puntosControl.Count),
                        (float)(cy / puntosControl.Count)
                    );
                }

                // Normalizar y devolver el punto calculado
                return new PointF((float)(x / suma), (float)(y / suma));
            }
            catch (Exception ex)
            {
                // En caso de cualquier error, devolver el punto medio entre el primer y último punto
                if (puntosControl != null && puntosControl.Count > 0)
                {
                    if (puntosControl.Count == 1)
                        return puntosControl[0];
                    
                    return new PointF(
                        (puntosControl[0].X + puntosControl[puntosControl.Count - 1].X) / 2,
                        (puntosControl[0].Y + puntosControl[puntosControl.Count - 1].Y) / 2
                    );
                }
                
                // Si no hay puntos de control, devolver el origen
                return PointF.Empty;
            }
        }

        // Animación de la curva B-spline
        public async Task AnimarCurva(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bmAnimacion, int pasos = 100, int delay = 20)
        {
            if (puntosControl == null || puntosControl.Count < 2)
                return;

            try
            {
                // Si solo hay 2 puntos, dibujar una línea recta
                if (puntosControl.Count == 2)
                {
                    using (var g = Graphics.FromImage(bmAnimacion))
                    {
                        g.Clear(Color.White);
                        g.DrawLine(pen, puntosControl[0], puntosControl[1]);
                        
                        // Dibujar puntos de control
                        g.FillEllipse(Brushes.Red, puntosControl[0].X - 3, puntosControl[0].Y - 3, 6, 6);
                        g.FillEllipse(Brushes.Red, puntosControl[1].X - 3, puntosControl[1].Y - 3, 6, 6);
                        
                        // Actualizar la imagen
                        if (pictureBox.InvokeRequired)
                        {
                            pictureBox.Invoke(new Action(() => {
                                pictureBox.Image?.Dispose();
                                pictureBox.Image = (Bitmap)bmAnimacion.Clone();
                            }));
                        }
                        else
                        {
                            pictureBox.Image?.Dispose();
                            pictureBox.Image = (Bitmap)bmAnimacion.Clone();
                        }
                    }
                    return;
                }

                // Generar vector de nodos uniforme
                var nodos = new List<double>();
                int n = puntosControl.Count + grado + 1;
                for (int i = 0; i < n; i++)
                {
                    nodos.Add((double)i / (n - 1));
                }
                
                // Calcular todos los puntos de la curva primero
                List<PointF> puntosCurva = new List<PointF>();
                double tMin = nodos[grado];
                double tMax = nodos[puntosControl.Count];
                
                for (int i = 0; i <= pasos; i++)
                {
                    double t = tMin + (tMax - tMin) * i / pasos;
                    t = Math.Max(tMin, Math.Min(tMax - 1e-10, t));
                    
                    try
                    {
                        PointF punto = CalcularPuntoBSpline(t, puntosControl, grado, nodos);
                        puntosCurva.Add(punto);
                    }
                    catch
                    {
                        // Continuar con el siguiente punto si hay un error
                        continue;
                    }
                }
                
                // Iniciar la animación
                for (int i = 0; i < puntosCurva.Count; i++)
                {
                    // Crear un nuevo bitmap para el frame actual
                    using (var bmFrame = new Bitmap(bmAnimacion.Width, bmAnimacion.Height))
                    using (var g = Graphics.FromImage(bmFrame))
                    {
                        // Dibujar el fondo
                        g.Clear(Color.White);
                        
                        // Dibujar líneas de control
                        if (mostrarLineasControl && puntosControl.Count > 1)
                        {
                            g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                        }
                        
                        // Dibujar puntos de control
                        if (mostrarPuntosControl)
                        {
                            foreach (var punto in puntosControl)
                            {
                                g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                            }
                        }
                        
                        // Dibujar la curva hasta el punto actual
                        if (i > 0)
                        {
                            g.DrawLines(pen, puntosCurva.Take(i + 1).ToArray());
                        }
                        
                        // Dibujar el punto actual
                        g.FillEllipse(Brushes.Blue, puntosCurva[i].X - 3, puntosCurva[i].Y - 3, 6, 6);
                        
                        // Actualizar el PictureBox de manera segura
                        if (pictureBox.InvokeRequired)
                        {
                            pictureBox.Invoke(new Action(() => {
                                pictureBox.Image?.Dispose();
                                pictureBox.Image = (Bitmap)bmFrame.Clone();
                                pictureBox.Refresh();
                            }));
                        }
                        else
                        {
                            pictureBox.Image?.Dispose();
                            pictureBox.Image = (Bitmap)bmFrame.Clone();
                            pictureBox.Refresh();
                        }
                    }
                    
                    // Pequeña pausa para la animación
                    await Task.Delay(delay);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la animación: {ex.Message}");
                // No relanzar la excepción para evitar bloquear la interfaz
            }
        }
    }
}
