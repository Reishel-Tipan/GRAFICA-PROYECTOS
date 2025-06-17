using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public class RellenoRombo
    {
        public Point Centro { get; private set; }
        public int Diagonal { get; private set; }
        public List<Point> Vertices { get; private set; }
        private PictureBox pictureBox;
        private Bitmap bmp;
        private Bitmap bmpRelleno;
        private Graphics g;
        private int ancho, alto;
        private Pen lapiz;
        private int factorEscala = 20; // Factor de escala para hacer el dibujo más grande
        private int rangoCuadricula = 20; // Rango de la cuadrícula (de -20 a 20)
        private Color colorRelleno = Color.Yellow;
        private Color colorBorde = Color.Black;
        
        // Propiedad pública para acceder al factor de escala
        public int FactorEscala => factorEscala;

        public RellenoRombo(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            Vertices = new List<Point>();
            lapiz = new Pen(Color.Black, 2);
            
            // Asegurar que el PictureBox sea cuadrado para mejor visualización
            int tamaño = Math.Min(pictureBox.Width, pictureBox.Height);
            pictureBox.Width = tamaño;
            pictureBox.Height = tamaño;
            
            // Inicializar el bitmap y graphics
            ancho = pictureBox.Width;
            alto = pictureBox.Height;
            bmp = new Bitmap(ancho, alto);
            bmpRelleno = new Bitmap(ancho, alto);
            g = Graphics.FromImage(bmp);
            
            // Configurar gráficos para mejor calidad
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(Color.White);
            
            // Dibujar ejes cartesianos y cuadrícula
            DibujarEjes();
            
            pictureBox.Image = bmp;
        }

        private void DibujarEjes()
        {
            // Configuración de pinceles y fuentes
            Pen cuadriculaPen = new Pen(Color.FromArgb(240, 240, 240), 1);
            Pen ejePen = new Pen(Color.Gray, 1);
            Font font = new Font("Arial", 8);
            Brush brush = Brushes.Black;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            int centroX = ancho / 2;
            int centroY = alto / 2;
            int tamFlecha = 5;

            // Dibujar cuadrícula y números
            for (int i = -rangoCuadricula; i <= rangoCuadricula; i++)
            {
                // Líneas verticales
                int x = centroX + (i * factorEscala);
                if (x >= 0 && x <= ancho)
                {
                    g.DrawLine(cuadriculaPen, x, 0, x, alto);
                    // Números del eje X
                    if (i != 0 && x > 0 && x < ancho - 10)
                    {
                        g.DrawString(i.ToString(), font, brush, x, centroY + 5, format);
                    }
                }

                // Líneas horizontales
                int y = centroY + (i * factorEscala);
                if (y >= 0 && y <= alto)
                {
                    g.DrawLine(cuadriculaPen, 0, y, ancho, y);
                    // Números del eje Y
                    if (i != 0 && y > 10 && y < alto - 10)
                    {
                        g.DrawString((-i).ToString(), font, brush, centroX - 15, y, format);
                    }
                }
            }

            // Dibujar ejes principales
            g.DrawLine(new Pen(Color.Gray, 1), 0, centroY, ancho, centroY); // Eje X
            g.DrawLine(new Pen(Color.Gray, 1), centroX, 0, centroX, alto);  // Eje Y

            // Flechas
            // Flecha eje X
            g.DrawLine(ejePen, ancho - 10, centroY - tamFlecha, ancho, centroY);
            g.DrawLine(ejePen, ancho - 10, centroY + tamFlecha, ancho, centroY);
            // Flecha eje Y
            g.DrawLine(ejePen, centroX - tamFlecha, 10, centroX, 0);
            g.DrawLine(ejePen, centroX + tamFlecha, 10, centroX, 0);

            // Etiquetas de los ejes
            g.DrawString("X", font, Brushes.Black, ancho - 15, centroY - 20);
            g.DrawString("Y", font, Brushes.Black, centroX + 10, 5);
            g.DrawString("0", font, Brushes.Black, centroX - 15, centroY + 5);
        }

        public void ConfigurarRombo(int centroX, int centroY, int diagonal)
        {
            // Convertir coordenadas del formulario a coordenadas del plano cartesiano
            int centroXCartesiano = ancho / 2 + (centroX * factorEscala);
            int centroYCartesiano = alto / 2 - (centroY * factorEscala);
            diagonal = diagonal * factorEscala; // Aplicar factor de escala a la diagonal
            
            Centro = new Point(centroXCartesiano, centroYCartesiano);
            Diagonal = diagonal;
            
            CalcularVertices();
        }

        private void CalcularVertices()
        {
            Vertices.Clear();
            
            int x = Centro.X;
            int y = Centro.Y;
            int d = Diagonal / 2; // Mitad de la diagonal (ya incluye el factor de escala)
            
            // Siempre dibujar en forma de diamante (orientación 0)
            // Arriba
            Vertices.Add(new Point(x, y - d));
            // Derecha
            Vertices.Add(new Point(x + d, y));
            // Abajo
            Vertices.Add(new Point(x, y + d));
            // Izquierda
            Vertices.Add(new Point(x - d, y));
        }

        public void Dibujar()
        {
            if (Vertices.Count < 4) return;

            // Limpiar el área de dibujo
            g.Clear(Color.White);
            DibujarEjes();

            // Dibujar el relleno si existe
            if (bmpRelleno != null)
            {
                // Crear un bitmap temporal para el relleno
                using (Bitmap tempRelleno = new Bitmap(bmpRelleno))
                {
                    // Asegurarse de que el relleno solo se aplique dentro del rombo
                    for (int x = 0; x < ancho; x++)
                    {
                        for (int y = 0; y < alto; y++)
                        {
                            if (tempRelleno.GetPixel(x, y).ToArgb() == colorRelleno.ToArgb())
                            {
                                // Verificar si el punto está dentro del rombo
                                if (EstaDentroDelRombo(new Point(x, y)))
                                {
                                    bmpRelleno.SetPixel(x, y, colorRelleno);
                                }
                                else
                                {
                                    bmpRelleno.SetPixel(x, y, Color.White);
                                }
                            }
                        }
                    }
                    g.DrawImage(bmpRelleno, 0, 0);
                }
            }


            // Dibujar el borde del rombo
            using (var pen = new Pen(colorBorde, 2))
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Point inicio = Vertices[i];
                    Point fin = Vertices[(i + 1) % Vertices.Count];
                    g.DrawLine(pen, inicio, fin);
                }
            }


            // Actualizar la imagen
            pictureBox.Image = bmp;
        }

        public void Limpiar()
        {
            Vertices.Clear();
            g.Clear(Color.White);
            bmpRelleno = new Bitmap(ancho, alto);
            DibujarEjes();
            pictureBox.Image = bmp;
        }

        // Método para verificar si un punto está dentro del rombo
        private bool EstaDentroDelRombo(Point punto)
        {
            if (Vertices.Count < 4) return false;
            
            // Algoritmo del punto dentro de un polígono
            bool resultado = false;
            int j = Vertices.Count - 1;
            
            for (int i = 0; i < Vertices.Count; i++)
            {
                if ((Vertices[i].Y < punto.Y && Vertices[j].Y >= punto.Y) || 
                    (Vertices[j].Y < punto.Y && Vertices[i].Y >= punto.Y))
                {
                    if (Vertices[i].X + (punto.Y - Vertices[i].Y) / 
                        (float)(Vertices[j].Y - Vertices[i].Y) * 
                        (Vertices[j].X - Vertices[i].X) < punto.X)
                    {
                        resultado = !resultado;
                    }
                }
                j = i;
            }
            return resultado;
        }

        // Método de relleno por inundación simplificado
        public void Rellenar(Point puntoInicio)
        {
            // Verificar si el punto está dentro del rombo
            if (!EstaDentroDelRombo(puntoInicio))
                return;

            // Crear un bitmap temporal para el relleno
            using (Bitmap bmpTemp = new Bitmap(ancho, alto))
            using (Graphics gTemp = Graphics.FromImage(bmpTemp))
            {
                // Dibujar el rombo en el bitmap temporal
                gTemp.Clear(Color.White);
                using (var pen = new Pen(Color.Black, 2))
                {
                    for (int i = 0; i < Vertices.Count; i++)
                    {
                        Point inicio = Vertices[i];
                        Point fin = Vertices[(i + 1) % Vertices.Count];
                        gTemp.DrawLine(pen, inicio, fin);
                    }
                }

                // Obtener el color del borde (negro)
                Color colorBorde = Color.Black;
                
                // Si el punto de inicio está en el borde, salir
                if (bmpTemp.GetPixel(puntoInicio.X, puntoInicio.Y).ToArgb() == colorBorde.ToArgb())
                    return;

                // Crear una pila para el relleno
                Stack<Point> pila = new Stack<Point>();
                pila.Push(puntoInicio);

                // Matriz para marcar píxeles visitados
                bool[,] visitado = new bool[ancho, alto];
                
                // Algoritmo de relleno por inundación
                while (pila.Count > 0)
                {
                    Point p = pila.Pop();

                    // Verificar límites
                    if (p.X < 0 || p.X >= ancho || p.Y < 0 || p.Y >= alto)
                        continue;

                        
                    // Si ya fue visitado, saltar
                    if (visitado[p.X, p.Y])
                        continue;


                    // Obtener el color del píxel actual
                    Color pixelColor = bmpTemp.GetPixel(p.X, p.Y);
                    
                    // Si el píxel es el borde, saltar
                    if (pixelColor.ToArgb() == colorBorde.ToArgb())
                        continue;

                    // Marcar como visitado
                    visitado[p.X, p.Y] = true;
                    
                    // Rellenar el píxel en el bitmap de relleno
                    bmpRelleno.SetPixel(p.X, p.Y, colorRelleno);

                    // Agregar píxeles vecinos (4-vecinos)
                    pila.Push(new Point(p.X + 1, p.Y));
                    pila.Push(new Point(p.X - 1, p.Y));
                    pila.Push(new Point(p.X, p.Y + 1));
                    pila.Push(new Point(p.X, p.Y - 1));
                }


                // Actualizar la imagen final
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Limpiar y dibujar ejes
                    g.Clear(Color.White);
                    DibujarEjes();
                    
                    // Dibujar el relleno
                    g.DrawImage(bmpRelleno, 0, 0);
                    
                    // Redibujar el borde
                    using (var pen = new Pen(colorBorde, 2))
                    {
                        for (int i = 0; i < Vertices.Count; i++)
                        {
                            Point inicio = Vertices[i];
                            Point fin = Vertices[(i + 1) % Vertices.Count];
                            g.DrawLine(pen, inicio, fin);
                        }
                    }
                }
                
                pictureBox.Image = bmp;
            }
        }
    }
}
