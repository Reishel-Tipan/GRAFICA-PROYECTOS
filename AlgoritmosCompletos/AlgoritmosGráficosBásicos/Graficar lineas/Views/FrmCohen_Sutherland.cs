using Graficar_lineas.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmCohen_Sutherland : Form
    {
        private enum ModoDibujo { Ninguno, Ventana, Linea }
        private ModoDibujo modoActual = ModoDibujo.Ninguno;
        
        private Point puntoInicio;
        private Rectangle ventanaRecorte;
        private Point[] linea = new Point[2];
        private int contadorClicks = 0;
        
        private readonly Pen penVentana = new Pen(Color.BlueViolet, 2); 
        private readonly Pen penLineaOriginal = new Pen(Color.LightGreen, 2); 
        private readonly Pen penLineaRecortada = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }; 
        private ToolStripStatusLabel tsslMensaje;
        private bool lineaRecortada = false;
        private Point[] lineaRecortadaPuntos = new Point[2];
        private Cohen_SutherlandController controller;

        public FrmCohen_Sutherland()
        {
            InitializeComponent();
            ConfigurarControles();
            controller = new Cohen_SutherlandController(pbxLienzo);
            ActualizarMensaje("Haz clic para dibujar la ventana de recorte");
        }

        private void ConfigurarControles()
        {
            // Configurar PictureBox
            pbxLienzo.BackColor = Color.White;
            pbxLienzo.BorderStyle = BorderStyle.FixedSingle;
            pbxLienzo.MouseClick += PbxLienzo_MouseClick;
            pbxLienzo.Paint += PbxLienzo_Paint;

            // Configurar botones
            btnRecortar.Enabled = false;
            btnRecortar.Click += BtnRecortar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnExit.Click += BtnExit_Click;

            // Configurar barra de estado
            tsslMensaje = new ToolStripStatusLabel();
            statusStrip1.Items.Add(tsslMensaje);
        }

        private void PbxLienzo_MouseClick(object sender, MouseEventArgs e)
        {
            if (modoActual == ModoDibujo.Ninguno)
            {
                // Primer modo: dibujar ventana de recorte
                if (contadorClicks == 0)
                {
                    puntoInicio = e.Location;
                    contadorClicks = 1;
                    ActualizarMensaje("Haz clic en la esquina opuesta para la ventana");
                }
                else
                {
                    int x = Math.Min(puntoInicio.X, e.X);
                    int y = Math.Min(puntoInicio.Y, e.Y);
                    int width = Math.Abs(e.X - puntoInicio.X);
                    int height = Math.Abs(e.Y - puntoInicio.Y);

                    ventanaRecorte = new Rectangle(x, y, width, height);
                    contadorClicks = 0;
                    modoActual = ModoDibujo.Linea;
                    btnRecortar.Enabled = false;
                    ActualizarMensaje("Ahora dibuja la línea (dos clics)");
                    pbxLienzo.Invalidate();
                }
            }
            else if (modoActual == ModoDibujo.Linea)
            {
                // Segundo modo: dibujar línea
                if (contadorClicks == 0)
                {
                    linea[0] = e.Location;
                    contadorClicks = 1;
                    ActualizarMensaje("Haz clic para el punto final de la línea");
                }
                else
                {
                    linea[1] = e.Location;
                    contadorClicks = 0;
                    btnRecortar.Enabled = true;
                    ActualizarMensaje("Línea dibujada. Haz clic en 'Recortar' para ver el resultado");
                    pbxLienzo.Invalidate();
                }
            }
        }

        private void BtnRecortar_Click(object sender, EventArgs e)
        {
            if (ventanaRecorte != Rectangle.Empty && linea[0] != Point.Empty && linea[1] != Point.Empty)
            {
                // Configurar la ventana de recorte y la línea en el controlador
                controller.SetClipWindow(ventanaRecorte);
                controller.SetLine(linea[0], linea[1]);
                
                // Realizar el recorte
                controller.ClipLine();
                
                // Actualizar el estado
                lineaRecortada = true;
                lineaRecortadaPuntos[0] = linea[0];
                lineaRecortadaPuntos[1] = linea[1];
                
                ActualizarMensaje("Línea recortada correctamente");
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar todo
            controller.Clear();
            ventanaRecorte = Rectangle.Empty;
            linea[0] = linea[1] = Point.Empty;
            lineaRecortada = false;
            lineaRecortadaPuntos[0] = lineaRecortadaPuntos[1] = Point.Empty;
            contadorClicks = 0;
            modoActual = ModoDibujo.Ninguno;
            btnRecortar.Enabled = false;
            ActualizarMensaje("Haz clic para dibujar la ventana de recorte");
        }

        private void PbxLienzo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibujar ventana de recorte (azul)
            if (!ventanaRecorte.IsEmpty)
            {
                g.DrawRectangle(penVentana, ventanaRecorte);
            }

            // Dibujar línea original (verde)
            if (linea[0] != Point.Empty && linea[1] != Point.Empty)
            {
                g.DrawLine(penLineaOriginal, linea[0], linea[1]);
            }
        }

        private void ActualizarMensaje(string mensaje)
        {
            if (tsslMensaje != null)
            {
                tsslMensaje.Text = mensaje;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
