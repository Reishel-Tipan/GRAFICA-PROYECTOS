using Graficar_lineas.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graficar_lineas.Views
{
    public partial class FrmSutherland_Hodgman : Form
    {
        private enum ModoDibujo { Ninguno, Ventana, Poligono }
        private ModoDibujo modoActual = ModoDibujo.Ninguno;
        
        private Point puntoInicio;
        private Rectangle ventanaRecorte;
        private List<Point> puntosPoligono = new List<Point>();
        private int contadorClicks = 0;
        
        private readonly Pen penVentana = new Pen(Color.Blue, 2); 
        private readonly Pen penPoligono = new Pen(Color.Green, 2);
        private readonly Pen penPoligonoRecortado = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
        private readonly Brush brushPoligonoRecortado = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
        
        private ToolStripStatusLabel tsslMensaje;
        private bool poligonoCompleto = false;
        private bool poligonoRecortado = false;
        private Sutherland_HodgmanController controller;

        public FrmSutherland_Hodgman()
        {
            InitializeComponent();
            ConfigurarControles();
            controller = new Sutherland_HodgmanController(pbxLienzo);
            ActualizarMensaje("Haz clic para dibujar la ventana de recorte");
        }

        private void ConfigurarControles()
        {
            // Configurar PictureBox
            pbxLienzo.BackColor = Color.White;
            pbxLienzo.BorderStyle = BorderStyle.FixedSingle;
            pbxLienzo.MouseClick += PbxLienzo_MouseClick;
            pbxLienzo.Paint += PbxLienzo_Paint;
            pbxLienzo.MouseDoubleClick += PbxLienzo_MouseDoubleClick;

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
                    modoActual = ModoDibujo.Poligono;
                    btnRecortar.Enabled = false;
                    ActualizarMensaje("Haz clic para dibujar los vértices del polígono. Doble clic para terminar.");
                    pbxLienzo.Invalidate();
                    
                    // Configurar la ventana de recorte en el controlador
                    controller.SetClipWindow(ventanaRecorte);
                    controller.StartDrawingPolygon();
                }
            }
            else if (modoActual == ModoDibujo.Poligono)
            {
                // Segundo modo: dibujar polígono
                if (poligonoCompleto)
                {
                    // Si ya hay un polígono completo, comenzar uno nuevo
                    puntosPoligono.Clear();
                    poligonoCompleto = false;
                    poligonoRecortado = false;
                    btnRecortar.Enabled = false;
                }
                
                // Agregar el punto al polígono
                puntosPoligono.Add(e.Location);
                controller.AddPolygonPoint(e.Location);
                
                // Habilitar el botón de recortar si hay al menos 3 puntos
                if (puntosPoligono.Count >= 3)
                {
                    btnRecortar.Enabled = true;
                }
                
                pbxLienzo.Invalidate();
            }
        }

        private void PbxLienzo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (modoActual == ModoDibujo.Poligono && puntosPoligono.Count >= 3)
            {
                // Completar el polígono con doble clic
                poligonoCompleto = true;
                controller.CompletePolygon();
                ActualizarMensaje("Polígono completado. Haz clic en 'Recortar' para ver el resultado");
            }
        }

        private void BtnRecortar_Click(object sender, EventArgs e)
        {
            if (poligonoCompleto && puntosPoligono.Count >= 3)
            {
                controller.ClipPolygon();
                poligonoRecortado = true;
                ActualizarMensaje("Polígono recortado. Haz clic para comenzar un nuevo polígono.");
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar todo
            controller.Clear();
            ventanaRecorte = Rectangle.Empty;
            puntosPoligono.Clear();
            poligonoCompleto = false;
            poligonoRecortado = false;
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

            // Dibujar polígono (verde)
            if (puntosPoligono.Count > 1)
            {
                g.DrawLines(penPoligono, puntosPoligono.ToArray());
                
                // Mostrar los puntos del polígono
                foreach (var punto in puntosPoligono)
                {
                    g.FillEllipse(Brushes.Green, punto.X - 3, punto.Y - 3, 6, 6);
                }
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

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
