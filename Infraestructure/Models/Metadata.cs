using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ProductoMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int ProductoID { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<bool> Estado { get; set; }
    }

    internal partial class PersonalEntregaMetadata
    {
        [Display(Name = "Identificación")]
        public int PersonalID { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Telefono")]
        public Nullable<int> telefono { get; set; }
        [Display(Name = "Vehiculo")]
        public int vehiculoID { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
    internal partial class PedidoMetadata
    {
        [Display(Name = "# Pedido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int idPedido { get; set; }
        [Display(Name = "Personal asignado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int idPersonalAsignado { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string direccion { get; set; }
        [Display(Name = "Fecha entrega")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fechaEntrega { get; set; }
        [Display(Name = "Horario de entrega")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime horarioEntrega { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int estado { get; set; }
        [Display(Name = "Tipo entrega")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int tipoEntregaId { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> idUsuario { get; set; }
    }
    internal partial class UsuarioMetaData
    {
        [Display(Name = "ID del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]

        public int UsuarioID { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasena { get; set; }
        public int NumeroTelefono { get; set; }
        public string Direccion { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual TipoUsuario TipoUsuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
