using inventario.BIZ.Manejadores;
using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Inventario.GUI.Admin
{
    /// <summary>
    /// Lógica de interacción para AreaWPF.xaml
    /// </summary>
    public partial class AreaWPF : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorDeAreas manejadorDeAreas;
        accion accionArea;

        public AreaWPF()
        {
            InitializeComponent();
            manejadorDeAreas = new ManejadorDeArea(new RepositorioGenerico<Area>());
        }

        private void btnAreaEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAreaNuevo_Click(object sender, RoutedEventArgs e)
        {
            accionArea = accion.Nuevo;
            if (accionArea == accion.Nuevo)
            {
                Area empl = new Area()
                {
                    Nombre = txbNombreArea.Text,

                };
                if (manejadorDeAreas.Agregar(empl))
                {
                    MessageBox.Show("Area Agregada Exitosamente", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                    txbNombreArea.Text = "";
                    this.Close();
                }

            }
        }
    }
}
