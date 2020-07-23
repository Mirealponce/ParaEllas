using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Newtonsoft.Json;
using OnlineShoppingStore.Models.DAL;

//using OnlineShoppingStore.Models;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        ConexionVentas db = new ConexionVentas();
        // GET: Admin
        //public UnidadGenericaDeTrabajo _UnidadDeTrabajo = new UnidadGenericaDeTrabajo();
       
        
        public ActionResult Dashboard()
        {
            //var venta = db.Venta.ToList();
            var stock = db.DetalleProducto.ToList();
            var ventas = db.Venta.OrderBy(v=>v.totalVenta).ToList();

            object[] totalventas = new object[ventas.Count];
            string[] Clientes = new string[ventas.Count];

            object[] stocks = new object[stock.Count];
            string[] prod= new string[stock.Count];

            for (int i=0;i< totalventas.Length;i++)
            {
                totalventas[i] = ventas[i].totalVenta;
                Clientes[i] = ventas[i].Cliente.nombre+" " +ventas[i].Cliente.apellido;
            }

            for (int i = 0; i < stocks.Length; i++)
            {
                prod[i] = stock[i].Producto.nombreProducto+ " " + stock[i].Talla;
                stocks[i] = stock[i].CantidadStock;
            }
            //GRÁFICO VENTAS 
            Highcharts graficoV = new Highcharts("graficoV");

            graficoV.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Bar,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Aquamarine),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.Transparent,
                BorderRadius = 0,
                BorderWidth = 2

            });

            graficoV.SetTitle(new Title()
            {
                Text = "Mis Ventas"
            });

            graficoV.SetSubtitle(new Subtitle()
            {
                Text = "Clientes compradores"
            });

            graficoV.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Clientes", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = Clientes
               //   TickmarkPlacement=on,
            });

            graficoV.SetYAxis(new YAxis()
            {
                // Type = AxisTypes.Category,
                Title = new YAxisTitle()
                {
                    Text = "Ventas",
                    Style = "fontWeight: 'bold', fontSize: '17px'",

                },
                //Categories = new[] { "5000", "10000", "15000", "20000" },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0,
                Max = 20000,


            });

            graficoV.SetLegend(new Legend
            {
                Enabled = false,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });

            graficoV.SetSeries(new Series[]
            {
                new Series{

                    Name = "$ Compra",
                    Data = new Data(totalventas)

                },

            }
            );
            //GRAFICO CLIENTES
            Highcharts columnChart = new Highcharts("columnchart");

            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Spline,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Coral),
                Style = "fontWeight: 'bold', fontSize: '17px',color:'#000000'",
                BorderColor = System.Drawing.Color.Coral,
              //  PlotBackgroundColor= new BackColorOrGradient(System.Drawing.Color.Red),
                BorderRadius = 0,
                BorderWidth = 2


            });

            columnChart.SetTitle(new Title()
            {
                Text = "Stock Disponible"
            });

            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Prenda - Tallas"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Clientes", Style = "fontWeight: 'bold', fontSize: '17px',color:'#000000'" },
                Categories = prod,
                
                //   TickmarkPlacement=on,
            });;

            columnChart.SetYAxis(new YAxis()
            {
               // Type = AxisTypes.Category,
                Title = new YAxisTitle()
                {
                    Text = "Cantidad Stock",
                    Style = "fontWeight: 'bold', fontSize: '17px', color:'#000000'",
                    
                    
                },
                //Categories = new[] { "5000", "10000", "15000", "20000" },
                
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0,
                Max=500,
              

            });
           /* columnChart.SetPlotOptions(new PlotOptions{
                Area =
                {
                    LineColor= System.Drawing.Color.Red,
                }
            });*/
            columnChart.SetLegend(new Legend
            {
                Enabled = false,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });

            columnChart.SetSeries(new Series[]
            {
                new Series{
                    
                    Name = "Stock",
                    Data = new Data(stocks
                    
                    ),
                    
                    
                    
                },
              

            }
            );

            
            ViewBag.GraficoVentasCliente = columnChart;
            ViewBag.GraficoVentas = graficoV;
            return View();
        }
        public ActionResult Categories()
        {
            //List<Categoria> allcategories = _UnidadDeTrabajo.GetRepositoryInstance<Categoria>().GetAllRecords().ToList();
            return View(/*allcategories*/);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory( int idCategoria)
        {
          /*  CategoryDetail cd;
#pragma warning disable CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'int' nunca es igual a 'NULL' de tipo 'int?'
                if (idCategoria != null)
#pragma warning restore CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'int' nunca es igual a 'NULL' de tipo 'int?'
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_UnidadDeTrabajo.GetRepositoryInstance<Categoria>().GetFirstorDefault(idCategoria)));
            }
            else
            {
                cd = new CategoryDetail();
            }
          */
            return View(/*"UpdateCategory",cd*/);
        }
        
        public ActionResult Producto()
        {
            return View();
        }
            
        

    }
}