using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graficar_lineas.Controllers;

namespace Graficar_lineas.Views
{
    public partial class FrmScanline : Form
    {
        private ScanlineController _controller;
        private bool _isFilling = false;
        private bool _isDrawing = false;
        private Point _startPoint;
        private Point _lastPoint;
        private Color _currentColor = Color.Black;
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Pen _drawingPen;
        private Bitmap _tempBitmap;
        private bool _isMouseDown = false;

        public FrmScanline()
        {
            InitializeComponent();
            // Asegurar que el estilo de borde se establezca después de InitializeComponent
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            InitializeForm();
            this.Load += FrmScanline_Load;
        }

        private void FrmScanline_Load(object sender, EventArgs e)
        {
            // Inicializar la superficie de dibujo
            InitializeDrawingSurface();
            
            // Configurar el controlador
            _controller = new ScanlineController(_bitmap, _graphics, listBox);
            
            // Configurar el color inicial
            pic_color.BackColor = _currentColor;
        }

        private void InitializeDrawingSurface()
        {
            // Crear el bitmap y graphics principales
            _bitmap = new Bitmap(pictuCanva.Width, pictuCanva.Height);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            _graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            
            // Configurar el lápiz con un grosor de 2 píxeles
            _drawingPen = new Pen(_currentColor, 2.5f);
            _drawingPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            _drawingPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            
            _graphics.Clear(Color.White);
            
            // Crear un bitmap temporal para el dibujo
            _tempBitmap = new Bitmap(_bitmap);
            
            // Asignar el bitmap al PictureBox
            pictuCanva.Image = _bitmap;
        }

        private void InitializeForm()
        {
            this.Text = "Algoritmo de Relleno Scanline";
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Configurar PictureBox
            pictuCanva.BackColor = Color.White;
            pictuCanva.BorderStyle = BorderStyle.Fixed3D;
            
            // Configurar botones
            BtnColorSet.Click += BtnColorSet_Click;
            rellenar.Click += Rellenar_Click;
            btnLimpiarTodo.Click += BtnLimpiarTodo_Click;
            BtnLine.Click += BtnLine_Click;
            
            // Configurar eventos del PictureBox
            pictuCanva.MouseDown += PictuCanva_MouseDown;
            pictuCanva.MouseMove += PictuCanva_MouseMove;
            pictuCanva.MouseUp += PictuCanva_MouseUp;
            
            // Configurar color inicial
            _currentColor = Color.Black;
            pic_color.BackColor = _currentColor;
            
            // Activar el modo de dibujo por defecto
            ActivarModoDibujo();
        }

        private void InitializeController()
        {
            _controller = new ScanlineController(_bitmap, _graphics, listBox);
        }

        private void BtnColorSet_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = _currentColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _currentColor = colorDialog.Color;
                    pic_color.BackColor = _currentColor;
                    
                    // Actualizar el lápiz con el nuevo color y mantener el grosor
                    _drawingPen?.Dispose();
                    _drawingPen = new Pen(_currentColor, 2.5f);
                    _drawingPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    _drawingPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    
                    // Actualizar el color en el controlador
                    if (_controller != null)
                    {
                        _controller.SetFillColor(_currentColor);
                    }
                }
            }
        }

        private void Rellenar_Click(object sender, EventArgs e)
        {
            _isFilling = true;
            _isDrawing = false;
            this.Cursor = Cursors.Cross;
            BtnLine.BackColor = SystemColors.Control;
            rellenar.BackColor = Color.LightBlue;
        }

        private void BtnLimpiarTodo_Click(object sender, EventArgs e)
        {
            _graphics.Clear(Color.White);
            pictuCanva.Invalidate();
            listBox.Items.Clear();
        }

        private void ActivarModoDibujo()
        {
            _isFilling = false;
            _isDrawing = true;
            this.Cursor = Cursors.Cross;
            rellenar.BackColor = SystemColors.Control;
            BtnLine.BackColor = Color.LightBlue;
        }

        private void BtnLine_Click(object sender, EventArgs e)
        {
            ActivarModoDibujo();
        }

        private void PictuCanva_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            
            _isMouseDown = true;
            _startPoint = e.Location;
            _lastPoint = e.Location;
            
            if (_isFilling)
            {
                try
                {
                    _controller.SetFillColor(_currentColor);
                    _controller.Fill(e.Location);
                    pictuCanva.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al rellenar: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PictuCanva_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown || !_isDrawing) return;

            // Actualizar la posición actual del mouse
            _lastPoint = e.Location;
            
            // Actualizar la vista para mostrar la línea temporal
            pictuCanva.Invalidate();
        }

        private void PictuCanva_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            
            if (_isDrawing && _isMouseDown)
            {
                // Dibujar la línea final en el bitmap principal
                using (var g = Graphics.FromImage(_bitmap))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawLine(_drawingPen, _startPoint, e.Location);
                }
                
                // Actualizar la vista
                pictuCanva.Invalidate();
            }
            
            _isMouseDown = false;
        }

        private void PictuCanva_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar la línea temporal mientras se arrastra
            if (_isMouseDown && _isDrawing)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(_drawingPen, _startPoint, _lastPoint);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Manejar Ctrl+Z para deshacer y Ctrl+Y para rehacer
            if (keyData == (Keys.Control | Keys.Z))
            {
                //_controller.Undo();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Y))
            {
                //_controller.Redo();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
