using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graficar_lineas.Models;

namespace Graficar_lineas.Controllers
{
    public class ElipseBreshController
    {
        private ElipseBresenham _elipse;
        private readonly PictureBox _pictureBox;
        private readonly ListBox _listBoxPuntos;
        private readonly TextBox _txtCentroX, _txtCentroY, _txtRadioMayor, _txtRadioMenor;
        private readonly Button _btnGraficar, _btnLimpiar;
        private const int FactorEscala = 20; // Factor para escalar los valores de entrada

        public ElipseBreshController(
            PictureBox pictureBox,
            ListBox listBoxPuntos,
            TextBox txtCentroX, TextBox txtCentroY, 
            TextBox txtRadioMayor, TextBox txtRadioMenor,
            Button btnGraficar, Button btnLimpiar)
        {
            try
            {
                // Validar controles
                if (pictureBox == null) throw new ArgumentNullException(nameof(pictureBox));
                if (listBoxPuntos == null) throw new ArgumentNullException(nameof(listBoxPuntos));
                if (txtCentroX == null) throw new ArgumentNullException(nameof(txtCentroX));
                if (txtCentroY == null) throw new ArgumentNullException(nameof(txtCentroY));
                if (txtRadioMayor == null) throw new ArgumentNullException(nameof(txtRadioMayor));
                if (txtRadioMenor == null) throw new ArgumentNullException(nameof(txtRadioMenor));
                if (btnGraficar == null) throw new ArgumentNullException(nameof(btnGraficar));
                if (btnLimpiar == null) throw new ArgumentNullException(nameof(btnLimpiar));

                // Asignar controles
                _pictureBox = pictureBox;
                _listBoxPuntos = listBoxPuntos;
                _txtCentroX = txtCentroX;
                _txtCentroY = txtCentroY;
                _txtRadioMayor = txtRadioMayor;
                _txtRadioMenor = txtRadioMenor;
                _btnGraficar = btnGraficar;
                _btnLimpiar = btnLimpiar;

                // Verificar que el PictureBox tenga un tamaño válido
                if (_pictureBox.Width <= 0 || _pictureBox.Height <= 0)
                {
                    _pictureBox.Width = 600;
                    _pictureBox.Height = 400;
                }

                // Inicializar el modelo de la elipse con el PictureBox
                _elipse = new ElipseBresenham(_pictureBox);
                
                // Configurar el PictureBox
                _pictureBox.Image = new Bitmap(_pictureBox.Width, _pictureBox.Height);
                using (var g = Graphics.FromImage(_pictureBox.Image))
                {
                    g.Clear(Color.White);
                }

                // Configurar manejadores de eventos
                _btnGraficar.Click += BtnGraficar_Click;
                _btnLimpiar.Click += BtnLimpiar_Click;
                _btnLimpiar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el controlador: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void BtnGraficar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar entradas
                if (!int.TryParse(_txtCentroX.Text, out int centroX) ||
                    !int.TryParse(_txtCentroY.Text, out int centroY) ||
                    !int.TryParse(_txtRadioMayor.Text, out int radioMayor) ||
                    !int.TryParse(_txtRadioMenor.Text, out int radioMenor) ||
                    radioMayor <= 0 || radioMenor <= 0)
                {
                    MessageBox.Show("Por favor ingrese valores numéricos válidos. Los radios deben ser positivos.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Limpiar el área de dibujo
                using (var g = Graphics.FromImage(_pictureBox.Image))
                {
                    g.Clear(Color.White);
                }

                // Calcular el centro de la imagen
                int centroImagenX = _pictureBox.Width / 2;
                int centroImagenY = _pictureBox.Height / 2;
                
                // Configurar la elipse (multiplicamos por el factor de escala)
                int centroXEscalado = centroImagenX + (centroX * FactorEscala);
                int centroYEscalado = centroImagenY - (centroY * FactorEscala);
                int radioMayorEscalado = radioMayor * FactorEscala;
                int radioMenorEscalado = radioMenor * FactorEscala;
                
                // Crear una nueva instancia del modelo para asegurar un estado limpio
                _elipse = new ElipseBresenham(_pictureBox);
                
                // Configurar la elipse
                _elipse.ConfigurarElipse(centroXEscalado, centroYEscalado, radioMayorEscalado, radioMenorEscalado);
                
                // Dibujar la elipse
                _elipse.Dibujar();
                
                // Actualizar la lista de puntos
                ActualizarListaPuntos();
                
                // Habilitar el botón de limpiar
                _btnLimpiar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar la elipse: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar botones durante la limpieza
                _btnGraficar.Enabled = false;
                _btnLimpiar.Enabled = false;
                
                // Limpiar en un hilo separado
                await Task.Run(() => _elipse.Limpiar());
                
                // Limpiar la lista de puntos
                _listBoxPuntos.Items.Clear();
                
                // Limpiar campos de texto
                _txtCentroX.Clear();
                _txtCentroY.Clear();
                _txtRadioMayor.Clear();
                _txtRadioMenor.Clear();
                
                // Restaurar estado de los botones
                _btnGraficar.Enabled = true;
                _btnLimpiar.Enabled = false; // Mantener deshabilitado hasta que se dibuje algo
                _txtCentroX.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _btnGraficar.Enabled = true;
                _btnLimpiar.Enabled = true;
            }
        }

        private void ActualizarListaPuntos()
        {
            _listBoxPuntos.Items.Clear();
            if (_elipse?.Puntos != null)
            {
                // Obtener el centro del PictureBox para la conversión de coordenadas
                int centroX = _pictureBox.Width / 2;
                int centroY = _pictureBox.Height / 2;
                
                foreach (var punto in _elipse.Puntos)
                {
                    // Mostrar las coordenadas de píxeles
                    _listBoxPuntos.Items.Add($"Píxel: ({punto.X}, {punto.Y})");
                    
                    // Convertir a coordenadas cartesianas
                    int x = (punto.X - centroX) / FactorEscala;
                    int y = (centroY - punto.Y) / FactorEscala; // Invertir Y para que sea cartesiano
                    
                    // Mostrar las coordenadas cartesianas
                    _listBoxPuntos.Items.Add($"Cartesiano: ({x}, {y})");
                    
                    // Agregar una línea en blanco para separar los puntos
                    _listBoxPuntos.Items.Add("");
                }
                
                // Si no hay puntos, mostrar un mensaje
                if (_elipse.Puntos.Count == 0)
                {
                    _listBoxPuntos.Items.Add("No hay puntos para mostrar.");
                }
            }
            else
            {
                _listBoxPuntos.Items.Add("No se ha dibujado ninguna elipse.");
            }
        }
    }
}
