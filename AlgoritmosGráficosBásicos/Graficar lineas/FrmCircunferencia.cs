using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas
{
    public partial class FrmCircunferencia : Form
    {
        private DDA circunferencia;
        private Bitmap bmp;
        private Graphics gBmp;
        private int cellSize = 10; // Tamaño de celda fijo para mejor visualización
        private int centerX, centerY; // Centro del área de dibujo
        private int canvasSize; // Tamaño del área de dibujo en celdas
        private int marginX, marginY; // Márgenes para centrar el dibujo

        public FrmCircunferencia()
        {
            InitializeComponent();
            circunferencia = new DDA(picDibujo);
            InicializarAreaDibujo();
        }

        private void InicializarAreaDibujo()
        {
            // Crear un nuevo bitmap para dibujar
            bmp = new Bitmap(picDibujo.Width, picDibujo.Height);
            gBmp = Graphics.FromImage(bmp);
            
            // Calcular dimensiones
            canvasSize = Math.Min(picDibujo.Width, picDibujo.Height) / cellSize;
            marginX = (picDibujo.Width - canvasSize * cellSize) / 2;
            marginY = (picDibujo.Height - canvasSize * cellSize) / 2;
            
            // Calcular el centro del área de dibujo
            centerX = canvasSize / 2;
            centerY = canvasSize / 2;
            
            LimpiarAreaDibujo();
        }

        private void LimpiarAreaDibujo()
        {
            // Limpiar el área de dibujo
            gBmp.Clear(Color.White);
            
            // Dibujar cuadrícula
            using (var gridPen = new Pen(Color.LightGray, 1))
            {
                for (int i = 0; i <= canvasSize; i++)
                {
                    int x = marginX + i * cellSize;
                    gBmp.DrawLine(gridPen, x, marginY, x, marginY + canvasSize * cellSize);
                    
                    int y = marginY + i * cellSize;
                    gBmp.DrawLine(gridPen, marginX, y, marginX + canvasSize * cellSize, y);
                }
            }
            
            // Dibujar ejes
            using (var axisPen = new Pen(Color.Black, 2))
            {
                gBmp.DrawLine(axisPen, marginX, marginY + centerY * cellSize, 
                    marginX + canvasSize * cellSize, marginY + centerY * cellSize); // Eje X
                gBmp.DrawLine(axisPen, marginX + centerX * cellSize, marginY, 
                    marginX + centerX * cellSize, marginY + canvasSize * cellSize); // Eje Y
            }
            
            // Actualizar el PictureBox
            picDibujo.Image = bmp;
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Pasar el PictureBox al método de validación para verificar que la circunferencia quepa en la vista
            if (circunferencia.ValidarDatos(txtX_1.Text, txtY_1.Text, txtRadio.Text, picDibujo))
            {
                // Limpiar el área de dibujo y la lista de puntos
                LimpiarAreaDibujo();
                listBoxPuntos.Items.Clear();
                
                // Iniciar la animación
                circunferencia.IniciarAnimacion(ActualizarDibujo);
                
                // Actualizar la lista de puntos inicial
                MostrarTodosLosPuntos();
                
                // Forzar la actualización de la interfaz de usuario
                Application.DoEvents();
            }
        }

        private void ActualizarDibujo()
        {
            // Limpiar el área de dibujo
            LimpiarAreaDibujo();
            
            // Obtener los puntos actuales
            var puntos = circunferencia.ObtenerPuntos();
            
            // Dibujar cada punto
            using (var brush = new SolidBrush(Color.Blue))
            {
                foreach (var punto in puntos)
                {
                    // Ajustar las coordenadas para el dibujo
                    int x = marginX + centerX * cellSize + punto.X * cellSize;
                    int y = marginY + centerY * cellSize - punto.Y * cellSize; // Invertir Y para que crezca hacia arriba
                    
                    // Asegurarse de que las coordenadas estén dentro de los límites
                    if (x >= marginX && x < marginX + canvasSize * cellSize && 
                        y >= marginY && y < marginY + canvasSize * cellSize)
                    {
                        gBmp.FillRectangle(brush, x, y, cellSize, cellSize);
                    }
                }
            }
            
            // Actualizar el PictureBox
            picDibujo.Refresh();
            
            // Actualizar la lista de puntos en el ListBox
            MostrarTodosLosPuntos();
            
            // Forzar la actualización de la interfaz de usuario
            Application.DoEvents();
        }

        private void btnReseat_Click(object sender, EventArgs e)
        {
            // Detener la animación si está en curso
            if (circunferencia.AnimacionEnCurso)
            {
                circunferencia.DetenerAnimacion();
            }
            
            // Limpiar los controles
            txtX_1.Clear();
            txtY_1.Clear();
            txtRadio.Clear();
            
            // Limpiar el área de dibujo
            LimpiarAreaDibujo();
            
            // Limpiar la lista de puntos
            listBoxPuntos.Items.Clear();
        }

        private void MostrarTodosLosPuntos()
        {
            // Obtener los puntos actuales
            var puntos = circunferencia.ObtenerPuntos();
            
            // Verificar si hay puntos para mostrar
            if (puntos.Count == 0) return;
            
            // Usar BeginUpdate/EndUpdate para mejorar el rendimiento
            listBoxPuntos.BeginUpdate();
            try
            {
                // Limpiar solo si es necesario (para evitar parpadeo)
                if (listBoxPuntos.Items.Count != puntos.Count)
                {
                    listBoxPuntos.Items.Clear();
                    
                    // Ordenar los puntos para mostrarlos de manera más organizada
                    var puntosOrdenados = new List<Point>(puntos);
                    puntosOrdenados.Sort((p1, p2) => 
                        p1.X != p2.X ? p1.X.CompareTo(p2.X) : p1.Y.CompareTo(p2.Y));
                    
                    // Agregar los puntos al ListBox
                    foreach (var punto in puntosOrdenados)
                    {
                        listBoxPuntos.Items.Add($"({punto.X}, {punto.Y})");
                    }
                }
            }
            finally
            {
                listBoxPuntos.EndUpdate();
            }
            
            // Asegurarse de que el ListBox sea visible
            if (listBoxPuntos.Visible == false)
            {
                listBoxPuntos.Visible = true;
            }
        }

        private void FrmCircunferencia_Load(object sender, EventArgs e)
        {
            // Inicializar el ListBox si es necesario
            if (listBoxPuntos == null)
            {
                listBoxPuntos = new ListBox();
                listBoxPuntos.Dock = DockStyle.Fill;
                groupBox3.Controls.Add(listBoxPuntos);
            }
        }
        
        private void FrmCircunferencia_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Asegurarse de liberar recursos
            if (gBmp != null)
            {
                gBmp.Dispose();
            }
            if (bmp != null)
            {
                bmp.Dispose();
            }
        }
    }
}
