using System.Collections.Generic;

namespace DACServices.Entities.Vendor.Clases
{
    public class ItrisPedidoEntity
    {
        public Pedido Pedido { get; set; }

    }
    public class Pedido
    {
        public int ID { get; set; }
        public string FK_ERP_EMPRESAS { get; set; }
        public int FK_ERP_ASESORES { get; set; }
        public string FECHA { get; set; }
        public string ESTADO { get; set; }
        public string CODIGO { get; set; }
        public IList<PedidoArticulo> PedidoArticulo { get; set; }
    }

    public class PedidoArticulo
    {
        public int FK_ARTICULOS { get; set; }
        public string SABOR { get; set; }
        public string CANTIDAD { get; set; }
    }


}
