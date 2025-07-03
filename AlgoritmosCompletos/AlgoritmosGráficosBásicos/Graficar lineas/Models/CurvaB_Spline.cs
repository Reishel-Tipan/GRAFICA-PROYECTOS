using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficar_lineas.Models
{
    public class CurvaB_Spline
    {
        private Graphics graphics;
        private Pen pen;
        private bool mostrarPuntosControl = true;
        private bool mostrarLineasControl = true;

        public CurvaB_Spline(Graphics graphics, Pen pen)
        {
            this.graphics = graphics;
            this.pen = pen;
        }

        // Método para calcular el valor de la base B-spline (versión no recursiva)
        private double BaseBSpline(int i, int k, double t, List<double> nodos)
        {
            // Validaciones básicas
            if (nodos == null || nodos.Count == 0 || i < 0 || k <= 0)
                return 0.0;

            // Si solo hay un punto, devolver 1 si t está en [0,1]
            if (nodos.Count == 1)
                return (t >= 0 && t <= 1) ? 1.0 : 0.0;

            // Para k=1 (primer orden)
            if (k == 1)
            {
                if (i >= nodos.Count - 1)
                    return 0.0;
                return (t >= nodos[i] && t < nodos[i + 1]) ? 1.0 : 0.0;
            }

            // Para órdenes superiores, usar el algoritmo de Cox-de Boor
            double resultado = 0.0;
            double den1 = nodos[i + k - 1] - nodos[i];
            double den2 = (i + k < nodos.Count) ? (nodos[i + k] - nodos[i + 1]) : 0;

            // Primer término
            if (den1 != 0)
            {
                double coef1 = (t - nodos[i]) / den1;
                resultado += coef1 * BaseBSpline(i, k - 1, t, nodos);
            }

            // Segundo término
            if (den2 != 0 && (i + 1) < nodos.Count)
            {
                double coef2 = (nodos[i + k] - t) / den2;
                resultado += coef2 * BaseBSpline(i + 1, k - 1, t, nodos);
            }

            return resultado;
        }

        // Genera un vector de nodos uniforme para B-spline con manejo robusto de casos límite
        private List<double> GenerarVectorNodos(int n, int k)
        {
            var nodos = new List<double>();
            
            // Validación de parámetros
            if (n <= 0 || k <= 0)
            {
                // Retornar un vector de nodos predeterminado en caso de parámetros inválidos
                return new List<double> { 0, 0, 1, 1 };
            }
            
            // Asegurar que k no sea mayor que n
            k = Math.Min(k, n);
            
            // Para curvas B-spline abiertas, los primeros y últimos k nodos son iguales
            // Esto asegura que la curva pase por los puntos inicial y final
            
            // Primeros k nodos en 0
            for (int i = 0; i < k; i++)
                nodos.Add(0);
            
            // Nodos intermedios uniformemente espaciados
            int numNodosIntermedios = n - k + 1;
            
            // Asegurar al menos un nodo intermedio
            if (numNodosIntermedios <= 1)
            {
                // Si no hay suficientes puntos para nodos intermedios, 
                // solo agregar un nodo en 0.5
                if (nodos.Count < 2) // Asegurar al menos 2 nodos
                    nodos.Add(0.5);
            }
            else
            {
                // Agregar nodos intermedios uniformemente espaciados
                for (int i = 1; i < numNodosIntermedios; i++)
                {
                    double valor = (double)i / numNodosIntermedios;
                    nodos.Add(valor);
                }
            }
            
            // Asegurar que el último nodo sea 1.0
            double ultimoValor = nodos.Count > 0 ? nodos[nodos.Count - 1] : 0;
            
            // Agregar nodos finales (k nodos en 1.0)
            for (int i = 0; i < k; i++)
            {
                // Si ya estamos en 1.0, no es necesario agregar más
                if (nodos.Count > 0 && Math.Abs(nodos[nodos.Count - 1] - 1.0) < 1e-10)
                    break;
                    
                nodos.Add(1.0);
            }
            
            // Asegurar que tengamos al menos 2 nodos
            if (nodos.Count < 2)
            {
                nodos.Clear();
                nodos.Add(0.0);
                nodos.Add(1.0);
            }
            
            return nodos;
        }

        // Dibuja la curva B-spline parcial mientras se agregan puntos
        public void DibujarCurvaParcial(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bitmap, int resolucion = 100)
        {
            if (puntosControl == null || puntosControl.Count <= grado)
                return;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Dibujar líneas de control si está habilitado
                if (mostrarLineasControl && puntosControl.Count > 1)
                {
                    g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                }

                // Dibujar puntos de control si está habilitado
                if (mostrarPuntosControl)
                {
                    foreach (var punto in puntosControl)
                    {
                        g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                    }
                }

                // Generar vector de nodos para la curva parcial
                var nodos = GenerarVectorNodos(puntosControl.Count, grado + 1);
                
                // Dibujar la curva parcial
                PointF? puntoAnterior = null;
                
                // Ajustar el rango de t para la curva parcial
                double tMin = nodos[grado];
                double tMax = nodos[puntosControl.Count - 1]; // Solo hasta el último punto de control
                
                // Reducir la resolución para curvas parciales para mejor rendimiento
                int resolucionParcial = Math.Max(20, resolucion / 2);
                
                for (int i = 0; i <= resolucionParcial; i++)
                {
                    // Calcular t en el rango [tMin, tMax]
                    double t = tMin + (tMax - tMin) * i / resolucionParcial;
                    
                    // Asegurarse de que t esté dentro de los límites
                    t = Math.Max(tMin, Math.Min(tMax - 1e-10, t));
                    
                    // Calcular el punto en la curva
                    PointF punto = CalcularPuntoBSpline(t, puntosControl, grado, nodos);
                    
                    // Dibujar la línea desde el punto anterior al actual
                    if (puntoAnterior.HasValue)
                    {
                        // Usar un lápiz semitransparente para la curva parcial
                        using (var penParcial = new Pen(Color.FromArgb(128, pen.Color), pen.Width))
                        {
                            g.DrawLine(penParcial, puntoAnterior.Value, punto);
                        }
                    }
                    
                    puntoAnterior = punto;
                }
            }
        }

        // Dibuja la curva B-spline completa
        public void DibujarCurva(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bitmap, int resolucion = 100)
        {
            if (puntosControl == null || puntosControl.Count < 2)
                return;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Limpiar el área de dibujo
                g.Clear(Color.White);
                
                // Dibujar líneas de control si está habilitado
                if (mostrarLineasControl && puntosControl.Count > 1)
                {
                    g.DrawLines(Pens.LightGray, puntosControl.ToArray());
                }

                // Dibujar puntos de control si está habilitado
                if (mostrarPuntosControl)
                {
                    foreach (var punto in puntosControl)
                    {
                        g.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                    }
                }

                // Si solo hay 2 puntos, dibujar una línea recta
                // Si solo hay 2 puntos, dibujar una línea recta
                if (puntosControl.Count == 2)
                {
                    g.DrawLine(pen, puntosControl[0], puntosControl[1]);
                    
                    // Actualizar la imagen del PictureBox
                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke(new Action(() => {
                            pictureBox.Image?.Dispose();
                            pictureBox.Image = (Bitmap)bitmap.Clone();
                        }));
                    }
                    else
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Bitmap)bitmap.Clone();
                    }
                    return;
                }

                // Generar vector de nodos
                var nodos = new List<double>();
                int n = puntosControl.Count + grado + 1;
                for (int i = 0; i < n; i++)
                {
                    nodos.Add((double)i / (n - 1));
                }
                
                // Dibujar la curva
                PointF? puntoAnterior = null;
                
                // Ajustar el rango de t para la curva completa
                double tMin = nodos[grado];
                double tMax = nodos[puntosControl.Count];
                
                // Asegurar que haya al menos 2 puntos para dibujar
                if (puntosControl.Count >= 2)
                {
                    for (int i = 0; i <= resolucion; i++)
                    {
                        // Calcular t en el rango [tMin, tMax]
                        double t = tMin + (tMax - tMin) * i / resolucion;
                        
                        // Asegurarse de que t esté dentro de los límites
                        t = Math.Max(tMin, Math.Min(tMax - 1e-10, t));
                        
                        try
                        {
                            // Calcular el punto en la curva
                            PointF punto = CalcularPuntoBSpline(t, puntosControl, grado, nodos);
                            
                            // Dibujar la línea desde el punto anterior al actual
                            if (puntoAnterior.HasValue)
                            {
                                g.DrawLine(pen, puntoAnterior.Value, punto);
                            }
                            
                            puntoAnterior = punto;
                        }
                        catch
                        {
                            // En caso de error, continuar con el siguiente punto
                            continue;
                        }
                    }
                }
                
                // Dibujar el último punto de control
                if (puntosControl.Count > 0)
                {
                    var ultimoPunto = puntosControl[puntosControl.Count - 1];
                    g.FillEllipse(Brushes.Red, ultimoPunto.X - 3, ultimoPunto.Y - 3, 6, 6);
                }
            }
            
            // Actualizar la imagen del PictureBox
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke(new Action(() => {
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

        // Calcula un punto en la curva B-spline para un parámetro t dado (versión simplificada)
        private PointF CalcularPuntoBSpline(double t, List<PointF> puntosControl, int grado, List<double> nodos)
        {
            try
            {
                // Validaciones básicas
                if (puntosControl == null || puntosControl.Count == 0)
                    return PointF.Empty;

                // Si solo hay un punto, devolverlo
                if (puntosControl.Count == 1)
                    return puntosControl[0];

                // Asegurar que el grado sea válido (1 = lineal, 2 = cuadrático, 3 = cúbico)
                grado = Math.Max(1, Math.Min(3, puntosControl.Count - 1));

                // Crear un vector de nodos uniforme si no se proporciona uno o es inválido
                if (nodos == null || nodos.Count == 0 || nodos.Count < puntosControl.Count + grado + 1)
                {
                    nodos = new List<double>();
                    int n = Math.Max(4, puntosControl.Count + grado + 1); // Mínimo 4 nodos para grado 1
                    for (int i = 0; i < n; i++)
                    {
                        nodos.Add((double)i / (n - 1));
                    }
                }

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
        public async Task AnimarCurva(List<PointF> puntosControl, int grado, PictureBox pictureBox, Bitmap bitmap, int pasos = 100, int delay = 20)
        {
            if (puntosControl == null || puntosControl.Count < 2)
                return;

            try
            {
                // Si solo hay 2 puntos, dibujar una línea recta
                if (puntosControl.Count == 2)
                {
                    using (var g = Graphics.FromImage(bitmap))
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
                                pictureBox.Image = (Bitmap)bitmap.Clone();
                            }));
                        }
                        else
                        {
                            pictureBox.Image?.Dispose();
                            pictureBox.Image = (Bitmap)bitmap.Clone();
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
                    using (var bmFrame = new Bitmap(bitmap.Width, bitmap.Height))
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
