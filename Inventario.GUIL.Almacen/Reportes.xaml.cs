using inventario.BIZ;
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

namespace Inventario.GUIL.Almacen
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        IManejadorDeEmpleados manejadorDeEmpleados;
        IManejadorDeVales manejadorDeVales;
        public Reportes()
        {
            InitializeComponent();
            manejadorDeEmpleados = new ManejadorDeEmpleado(new RepositorioGenerico<Empleado>());
            manejadorDeVales = new ManejadorDeVales(new RepositorioGenerico<Vale>());
            ActualizarComboBox();
        }

        private void ActualizarComboBox()
        {
            cbxPersona.ItemsSource = null;
            cbxPersona.ItemsSource = manejadorDeEmpleados.Listar;
        }

        private void cbxPersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxPersona.SelectedItem != null)
            {
                dtgTablaVales.ItemsSource = null;
                dtgTablaVales.ItemsSource = manejadorDeVales.BuscarNoEntregadosPorEmpleado((cbxPersona.SelectedItem as Empleado));
            }
        }
    }
}
