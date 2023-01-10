using inventario.BIZ;
using inventario.BIZ.Manejadores;
using iNVENTARIO.COMMON.entidades;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventario.GUIL.Almacen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         enum accion
        {
            Nuevo,
            Editar
        }
        ManejadorDeVales manejadorDeVales;
        ManejadorDeEmpleado manejadorDeEmpleado;
        ManejadorDeMateriales manejadorDeMateriales;
        Vale vale;


        accion accionVale;
        public MainWindow()
        {
            InitializeComponent();

            manejadorDeVales = new ManejadorDeVales(new RepositorioGenerico<Vale>());
            manejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioGenerico<Empleado>());
            manejadorDeMateriales = new ManejadorDeMateriales(new RepositorioGenerico<Material>());

            ActualizarTablaVales();
            gridDetalles.IsEnabled = false;
            
        }

        private void ActualizarTablaVales()
        {
            dtgVales.ItemsSource = null;
            dtgVales.ItemsSource = manejadorDeVales.Listar;
        }

        private void btnNuevoVale_Click(object sender, RoutedEventArgs e)
        {

            gridDetalles.IsEnabled = true;
            ActualizarComboxDetalles();

            vale = new Vale();
            vale.MaterialesPrestado = new List<Material>();
            actualizarListaDeMaterialesEnVale();
            accionVale = accion.Nuevo;
        }

        private void actualizarListaDeMaterialesEnVale()
        {
            dtgMaterialesVales.ItemsSource = null;
            dtgMaterialesVales.ItemsSource = vale.MaterialesPrestado;
        }

        private void ActualizarComboxDetalles()
        {
            cbxAlmacenista.ItemsSource = null;
            cbxAlmacenista.ItemsSource = manejadorDeEmpleado.EmpleadoPorArea("Almacen");

            cbxMateriales.ItemsSource = null;
            cbxMateriales.ItemsSource = manejadorDeMateriales.Listar;

            cbxSolicitante.ItemsSource = null;
            cbxSolicitante.ItemsSource = manejadorDeEmpleado.Listar;

            
        }

        private void btnEliminarVale_Click(object sender, RoutedEventArgs e)
        {
            Vale v = dtgVales.SelectedItem as Vale;

            if(v != null)
            {
                if(MessageBox.Show("Realmente Deseas Eliminar este Vale?", "Almacen", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    if (manejadorDeVales.Eliminar(v.Id))
                    {
                        MessageBox.Show("Eliminado con exito", "Almacen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        ActualizarTablaVales();
                        LimpiarCampoDetalles();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar", "Almacen", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }

        }

        private void btnCancelarVales_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardarVale_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (accionVale == accion.Nuevo)
                {
                    vale.EncargadoDeAlmacen = cbxAlmacenista.SelectedItem as Empleado;
                    vale.FechaEntrega = dtpFechaEntrega.SelectedDate.Value;
                    vale.FechaHoraSolicitud = DateTime.Now;
                    vale.Solicitante = cbxSolicitante.SelectedItem as Empleado;
                    if (manejadorDeVales.Agregar(vale))
                    {
                        MessageBox.Show("Vale gurardado con Exito", "Almacen", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarCampoDetalles();
                        gridDetalles.IsEnabled = false;
                        ActualizarTablaVales();
                    }
                    else
                    {
                        MessageBox.Show("Erro al guardar el Vale ", "Almacen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    vale.EncargadoDeAlmacen = cbxAlmacenista.SelectedItem as Empleado;
                    vale.FechaEntrega = dtpFechaEntrega.SelectedDate.Value;
                    vale.FechaHoraSolicitud = DateTime.Now;
                    vale.Solicitante = cbxSolicitante.SelectedItem as Empleado;

                    if (manejadorDeVales.Modificar(vale))
                    {
                        MessageBox.Show("Vale modificado con Exito", "Almacen", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarCampoDetalles();
                        gridDetalles.IsEnabled = false;
                        ActualizarTablaVales();
                    }
                    else
                    {
                        MessageBox.Show("Erro al Modificar el Vale ", "Almacen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, "+ex,"Almacen",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            

        }

        private void LimpiarCampoDetalles()
        {
            dtpFechaEntrega.SelectedDate = DateTime.Now;
            lblFechahoraEntregaReal.Content = "";
            lblFechahoraPrestamo.Content = "";
            dtgMaterialesVales.ItemsSource = null;
            cbxAlmacenista.ItemsSource = null;
            cbxMateriales.ItemsSource = null;
            cbxSolicitante.ItemsSource = null;
                
        }

        private void btnEntregarVale_Click(object sender, RoutedEventArgs e)
        {
            lblFechahoraEntregaReal.Content = DateTime.Now;
            vale.FechaEntregaReal = DateTime.Parse(lblFechahoraEntregaReal.Content.ToString());
        }

        private void btnAgregarMaterial_Click(object sender, RoutedEventArgs e)
        {
            Material m = cbxMateriales.SelectedItem as Material;
            if(m != null)
            {
                vale.MaterialesPrestado.Add(m);
                actualizarListaDeMaterialesEnVale();
            }
        }

        private void btneEliminarMaterial_Click(object sender, RoutedEventArgs e)
        {
            Material m = dtgMaterialesVales.SelectedItem as Material;
            if (m != null)
            {
                vale.MaterialesPrestado.Remove(m);
                actualizarListaDeMaterialesEnVale();
            }
        }

        private void dtgVales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Vale v = dtgVales.SelectedItem as Vale;
            if(v != null)
            {
                gridDetalles.IsEnabled = true;
                vale = v;
                actualizarListaDeMaterialesEnVale();
                accionVale = accion.Editar;
                lblFechahoraPrestamo.Content = vale.FechaHoraSolicitud.ToString();
                lblFechahoraEntregaReal.Content = vale.FechaEntregaReal.ToString();
                ActualizarComboxDetalles();
                cbxAlmacenista.Text = vale.EncargadoDeAlmacen.ToString();
                cbxSolicitante.Text = vale.Solicitante.ToString();
                dtpFechaEntrega.SelectedDate = vale.FechaEntrega;
            }
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes reportes = new Reportes();

            reportes.Show();
        }
    }
}
