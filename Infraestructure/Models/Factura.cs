//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        public string Clave { get; set; }
        public string CodigoActividad { get; set; }
        public string NumeroConsecutivox { get; set; }
        public Nullable<System.DateTime> FechaEmision { get; set; }
        public Nullable<int> IdVendedor { get; set; }
        public string nombre { get; set; }
        public Nullable<int> cedula { get; set; }
        public string CondicionVenta { get; set; }
        public Nullable<int> MedioPago { get; set; }
        public Nullable<decimal> TotalVenta { get; set; }
        public Nullable<decimal> TotalDescuento { get; set; }
        public Nullable<decimal> TotalVentaNeta { get; set; }
        public Nullable<decimal> TotalComprobante { get; set; }
        public Nullable<decimal> TotalImpuesto { get; set; }
        public int IdPedido { get; set; }
        public int IdFactura { get; set; }
    
        public virtual TipoPago TipoPago { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
