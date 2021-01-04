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
                SABOR = "NARANJA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "UVA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "LIMA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "POMELO",
                CANTIDAD = "1 PALLET"
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
                FECHA = "01 /01/2001 17:29:31",
                ESTADO = "PROCESANDO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "UVA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "UVA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "MELON",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "ZANDIA",
                CANTIDAD = "1 PALLET"
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
                FECHA = "01 /01/2001 17:29:31",
                ESTADO = "DESPACHADO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "CEREZA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "TONICA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "COLA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "POMELO",
                CANTIDAD = "1 PALLET"
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
                FECHA = "01 /01/2001 17:29:31",
                ESTADO = "ENTREGADO",
                CODIGO = "ASD123ADSASD"
            };

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "ANANA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "UVA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "NARANJA",
                CANTIDAD = "1 PALLET"
            };
            _articulos.Add(articulos);

            articulos = new PedidoArticulo()
            {
                FK_ARTICULOS = 26,
                SABOR = "COLA",
                CANTIDAD = "1 PALLET"
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
        public List<ItrisPedidoEntity> Get(int id)
        {
            return _pedidos.Where(x => x.Pedido.FK_ERP_EMPRESAS == id.ToString()).ToList();
        }


        public ItrisPedidoEntity Create(ItrisPedidoEntity obj)
        {
            obj.Pedido.ID = _pedidos.OrderByDescending(x => x.Pedido.ID).FirstOrDefault().Pedido.ID + 1;
            obj.Pedido.ESTADO = "PROCESANDO";

            _pedidos.Add(obj);
            return obj;
        }


        public ItrisPedidoEntity Delete(ItrisPedidoEntity obj)
        {
            var result = _pedidos.Where(x => x.Pedido.ID == obj.Pedido.ID).FirstOrDefault();
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
