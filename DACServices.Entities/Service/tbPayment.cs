//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DACServices.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbPayment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPayment()
        {
            this.tbPaymentDetail = new HashSet<tbPaymentDetail>();
        }
    
        public int pay_id { get; set; }
        public int usu_id { get; set; }
        public int pst_id { get; set; }
        public string pay_concepto { get; set; }
        public int pay_monto { get; set; }
        public int pay_producto { get; set; }
        public int pay_cuotas { get; set; }
        public string pay_url_formulario { get; set; }
        public string pay_url_formulario_custom { get; set; }
        public string pay_email_to { get; set; }
        public int pay_cantidad_mails_enviados { get; set; }
        public string pay_informacion_adicional { get; set; }
        public System.DateTime pay_fecha { get; set; }
    
        public virtual tbPaymentStatus tbPaymentStatus { get; set; }
        public virtual tbUsuario tbUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPaymentDetail> tbPaymentDetail { get; set; }
    }
}
