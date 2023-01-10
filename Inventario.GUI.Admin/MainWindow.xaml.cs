using inventario.BIZ.Manejadores;
using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using Inventario.DAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Inventario.GUI.Admin
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

        IManejadorDeEmpleados manejadorEmpleados;
        IManejadorDeMateriales manejadorDeMateriales;
        IManejadorDeAreas manejadorDeAreas;

        accion accionEmpleado;
        accion accionMateriales;

        public MainWindow()
        {
            InitializeComponent();
            manejadorEmpleados = new ManejadorDeEmpleado(new RepositorioGenerico<Empleado>());
            manejadorDeAreas = new ManejadorDeArea(new RepositorioGenerico<Area>());
            //llamada al metodo dasabilitador de botones empleados
            botonesEditConfigEmpleados(false);
           
            //llamada al metodo para limpiar los texbox empleados 
            limpiarCamposEmpleados();
            //llamada al metodo para precentar los datos en el datagri Empleados
            actualizarTablaEmpleados();
            actualizaCombobox();


            manejadorDeMateriales = new ManejadorDeMateriales(new RepositorioGenerico<Material>());

            //llamar al metodo dasabiliatador de botones Materiales
            btnEditConfigMateriales(false);
            //llamr al metodo para limpiar los texbox Materiales
            LimpiarCamposMateriales();
            //llamar al metodo para precentar los datos en el datagri Materiales
            ActualizaMateriales();

           
        }

        private void actualizaCombobox()
        {
            cbxAreas.ItemsSource = null;
            cbxAreas.ItemsSource = manejadorDeAreas.Listar;
        }

        //Empleados
        #region
        private void actualizarTablaEmpleados()
        {
            DtGEmpleados.ItemsSource = null;
            DtGEmpleados.ItemsSource = manejadorEmpleados.Listar;

           
        }


        //limpiar texbox empleados
        private void limpiarCamposEmpleados()
        {
            txtboxEmpleadoApellido.Clear();
           // txtboxEmpleadoArea.Clear();
            txtboxEmpleadoNombre.Clear();
            txtEmpleadoID.Text = "";
            cbxAreas.ItemsSource = null;
        }

        private void botonesEditConfigEmpleados(bool v)
        {
            btnEmpleadosCancelar.IsEnabled = v;
            btnEmpleadosEditar.IsEnabled = !v;
            btnEmpleadosEliminar.IsEnabled =! v;
            btnEmpleadosGuardar.IsEnabled = v;
            btnEmpleadosNuevo.IsEnabled = !v;
        }

        private void btnEmpleadosNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiarCamposEmpleados();
            botonesEditConfigEmpleados(true);
            actualizaCombobox();
            accionEmpleado = accion.Nuevo;
        }

        private void btnEmpleadosEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empl = DtGEmpleados.SelectedItem as Empleado;
            if(empl != null)
            {
                txtEmpleadoID.Text = empl.Id.ToString();
                txtboxEmpleadoNombre.Text = empl.Nombre;
                txtboxEmpleadoApellido.Text = empl.Apellidos;
                //txtboxEmpleadoArea.Text = empl.Area;
                cbxAreas.Text = empl.Area;
                accionEmpleado = accion.Editar;
                botonesEditConfigEmpleados(true);
            }
           
        }

        private void btnEmpleadosGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(accionEmpleado == accion.Nuevo)
            {
                Empleado empl = new Empleado()
                {
                    Nombre = txtboxEmpleadoNombre.Text,
                    Apellidos = txtboxEmpleadoApellido.Text,
                    Area = cbxAreas.SelectedItem.ToString() //txtboxEmpleadoArea.Text
                };
                if (manejadorEmpleados.Agregar(empl))
                {
                    MessageBox.Show("Empleado Agregado Correctamente!", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiarCamposEmpleados();
                    actualizarTablaEmpleados();
                    actualizaCombobox();
                    botonesEditConfigEmpleados(false);
                }
                else
                {
                    MessageBox.Show("Empleado No se pudo agregar  Correctamente", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Empleado empl = DtGEmpleados.SelectedItem as Empleado;
                empl.Apellidos = txtboxEmpleadoApellido.Text;
                empl.Nombre = txtboxEmpleadoNombre.Text;
                empl.Area = cbxAreas.SelectedItem.ToString(); //txtboxEmpleadoArea.Text;
                if (manejadorEmpleados.Modificar(empl))
                {
                    MessageBox.Show("Empleado Modificado Correctamente!", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiarCamposEmpleados();
                    actualizarTablaEmpleados();
                    actualizaCombobox();
                    botonesEditConfigEmpleados(false);
                }
                else
                {
                    MessageBox.Show("Empleado No se pudo Modificar Correctamente", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEmpleadosEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empl = DtGEmpleados.SelectedItem as Empleado;

            if(empl != null)
            {
                if (MessageBox.Show("Realmente, Desea eliminar este Empleado?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    if (manejadorEmpleados.Eliminar(empl.Id))
                    {
                        MessageBox.Show("Empleado Eliminado", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                        actualizarTablaEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Empleado", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                        actualizarTablaEmpleados();
                    }
                }
            }
        }

        private void btnEmpleadosCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCamposEmpleados();
            botonesEditConfigEmpleados(false);
        }

        #endregion


        //Materiales

        #region
        private void ActualizaMateriales()
        {
            dtgMateriales.ItemsSource = null;
            dtgMateriales.ItemsSource = manejadorDeMateriales.Listar.OrderBy(e=>e.Nombre);
        }

        private void LimpiarCamposMateriales()
        {
            txtboxMaterialDescripcion.Clear();
            txtboxMaterialCategoria.Clear();
            txtboxMaterialNombre.Clear();
            txtMaterialID.Text = "";
            imgFoto.Source = null;
        }

        private void btnEditConfigMateriales(bool v)
        {
            btnMaterialesCancelar.IsEnabled = v;
            btnMaterialesEditar.IsEnabled = !v;
            btnMaterialesEliminar.IsEnabled = !v;
            btnMaterialesGuardar.IsEnabled = v;
            btnMaterialesNuevo.IsEnabled = !v;
        }

        private void btnMaterialesNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposMateriales();
            btnEditConfigMateriales(true);
            accionMateriales = accion.Nuevo;

        }

        private void btnMaterialesEditar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;

            if(mat != null)
            {
                txtEmpleadoID.Text = mat.Id.ToString();
                txtboxMaterialNombre.Text = mat.Nombre;
                txtboxMaterialDescripcion.Text = mat.Decripccion;
                txtboxMaterialCategoria.Text = mat.Categoria;
                imgFoto.Source = ByteToImage(mat.Fotografia);
                accionMateriales = accion.Editar;
                btnEditConfigMateriales(true);
            }
        }

        private ImageSource ByteToImage(byte[] fotografia)
        {

            if (fotografia == null)
            {
                return null;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                MemoryStream memory = new MemoryStream(fotografia);

                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();

                ImageSource image = bitmap as ImageSource;
                return image;
                
            }
            
        }

        private void btnMaterialesGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionMateriales == accion.Nuevo)
            {
                Material mat = new Material()
                {
                    Nombre = txtboxMaterialNombre.Text,
                    Decripccion = txtboxMaterialDescripcion.Text,
                    Categoria = txtboxMaterialCategoria.Text,
                    Fotografia = ImageToByte(imgFoto.Source)
                    
                };
                if (manejadorDeMateriales.Agregar(mat))
                {
                    MessageBox.Show("Material Agregado Correctamente!", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMateriales();
                    ActualizaMateriales();
                    btnEditConfigMateriales(false);
                }
                else
                {
                    MessageBox.Show("Material No se pudo agregar  Correctamente", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Material mat = dtgMateriales.SelectedItem as Material;
               mat.Nombre = txtboxMaterialNombre.Text;
                mat.Decripccion = txtboxMaterialDescripcion.Text;
                mat.Categoria = txtboxMaterialCategoria.Text;
                mat.Fotografia = ImageToByte(imgFoto.Source);
                if (manejadorDeMateriales.Modificar(mat))
                {
                    MessageBox.Show("Material Modificado Correctamente!", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMateriales();
                    ActualizaMateriales();
                   btnEditConfigMateriales(false);
                }
                else
                {
                    MessageBox.Show("Material No se pudo Modificar Correctamente", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private byte[] ImageToByte(ImageSource source)
        {
            if(source != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                

                encoder.Frames.Add(BitmapFrame.Create(source as BitmapSource));
                encoder.Save(memoryStream);

                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }

        private void btnMaterialesEliminar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;
            if (mat != null)
            {
                if (MessageBox.Show("Realmente, Desea eliminar este Material?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorDeMateriales.Eliminar(mat.Id))
                    {
                        MessageBox.Show("Material Eliminado", "Inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizaMateriales();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Material seleccionado", "Inventario", MessageBoxButton.OK, MessageBoxImage.Error);
                        ActualizaMateriales();
                    }
                }
            }

        }

        private void btnMaterialesCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposMateriales();
            btnEditConfigMateriales(false);
        }
        #endregion

        private void btnAgregarArea_Click(object sender, RoutedEventArgs e)
        {
            AreaWPF areaWPF = new AreaWPF();

            if(areaWPF.ShowDialog()==DialogResult)
            {
                actualizaCombobox();
            }
        }

        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Selecciona una Foto";
            openFile.Filter = "Archivos jpg|*.jpg; *.png; *.gif;*.jfif";
            if (openFile.ShowDialog().Value)
            {
                imgFoto.Source = new BitmapImage(new Uri(openFile.FileName));
            }

        }
    }
}
