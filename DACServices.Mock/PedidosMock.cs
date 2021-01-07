﻿using DACServices.Entities.Vendor.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Mock
{
    public class PedidosMock
    {
        PedidoArticulo articulos = null;
        List<ItrisPedidoEntity> _pedidos = null;
        ItrisPedidoEntity _pedido = null;
        public static PedidosMock _instancia;
        Pedido pedido = null;
        List<PedidoArticulo> _articulos = null;
        protected PedidosMock()
        {
            _pedidos = new List<ItrisPedidoEntity>();
            _pedido = new ItrisPedidoEntity();
            _articulos = new List<PedidoArticulo>();
            pedido = new Pedido()
            {
                ID = 1,
                FK_ERP_EMPRESAS = "14200",
                FK_ERP_ASESORES = 33,
                FECHA = "01 /01/2001 17:29:31", 
                ESTADO = "ENVIANDO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 2
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 2
            };
            _articulos.Add(articulos);
            _pedido.Pedido = pedido;
            _pedido.Pedido.PedidoArticulo = _articulos;

            _pedidos.Add(_pedido);


            _pedido = new ItrisPedidoEntity();
            _articulos = new List<PedidoArticulo>();
            pedido = new Pedido()
            {
                ID = 2,
                FK_ERP_EMPRESAS = "14200",
                FK_ERP_ASESORES = 33,
                FECHA = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                ESTADO = "PROCESANDO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 4
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 2
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);
            _pedido.Pedido = pedido;
            _pedido.Pedido.PedidoArticulo = _articulos;

            _pedidos.Add(_pedido);

            _pedido = new ItrisPedidoEntity();
            _articulos = new List<PedidoArticulo>();
            pedido = new Pedido()
            {
                ID = 3,
                FK_ERP_EMPRESAS = "14200",
                FK_ERP_ASESORES = 33,
                FECHA = "01/01/2001 17:29:31",
                ESTADO = "DESPACHADO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 3
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 3
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);
            _pedido.Pedido = pedido;
            _pedido.Pedido.PedidoArticulo = _articulos;

            _pedidos.Add(_pedido);

            _pedido = new ItrisPedidoEntity();
            _articulos = new List<PedidoArticulo>();
            pedido = new Pedido()
            {
                ID = 4,
                FK_ERP_EMPRESAS = "14200",
                FK_ERP_ASESORES = 33,
                FECHA = "03/01/2001 17:29:31",
                ESTADO = "ENTREGADO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 2
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                CANTIDAD = 1
            };
            _articulos.Add(articulos);
            _pedido.Pedido = pedido;
            _pedido.Pedido.PedidoArticulo = _articulos;

            _pedidos.Add(_pedido);

        }
        public static PedidosMock Instancia()
        {
            if (_instancia == null)
                _instancia = new PedidosMock();
            return _instancia;
        }
        public ItrisPedidoEntity Get(int id)
        {
            return _pedidos.Where(x => x.Pedido.ID == id).FirstOrDefault();
        }

        public List<ItrisPedidoEntity> Get()
        {
            return _pedidos.ToList();
        }
        public ItrisPedidoEntity Create(ItrisPedidoEntity obj)
        {
            obj.Pedido.ID = _pedidos.OrderByDescending(x => x.Pedido.ID).FirstOrDefault().Pedido.ID + 1;
            obj.Pedido.FECHA = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            obj.Pedido.ESTADO = "PROCESANDO";

            _pedidos.Add(obj);
            return obj;
        }


        public ItrisPedidoEntity Delete(int id)
        {
            var result = _pedidos.Where(x => x.Pedido.ID == id).FirstOrDefault();
            if (result != null)
                _pedidos.Remove(result);
            return result;
        }

        public ItrisPedidoEntity Update(ItrisPedidoEntity obj)
        {
            ItrisPedidoEntity result = null;
            var tempObj = _pedidos.Where(x => x.Pedido.ID == obj.Pedido.ID).FirstOrDefault();
            if (tempObj != null)
            {
                _pedidos.Remove(tempObj);
                _pedidos.Add(obj);
                result = obj;
            }
            return result;
        }
    }
}
