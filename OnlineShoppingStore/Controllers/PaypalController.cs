using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.DAL;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult metodoPaypal()
        {
            if (Session["sesion"] != null)
            {
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                try
                {

                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {


                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/metodoPaypal?";
                        var Guid = Convert.ToString((new Random()).Next(100000));

                        var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + Guid);

                        var link = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (link.MoveNext())
                        {
                            Links lnk = link.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {

                                paypalRedirectUrl = lnk.href;
                            }
                        }

                        // saving the paymentID in the key guid
                        Session.Add(Guid, createdPayment.id);
                        return Redirect(paypalRedirectUrl);

                    }
                    else
                    {

                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                        if (executedPayment.state.ToLower() != "approved")
                        {

                            return View("ErrorTransaccion");
                        }

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                    return View("ErrorTransaccion");
                }

                ViewBag.Result = "Exitoo";
                return View("Exito");
            }
            else
            {
                return Redirect("~/Clientes/IniciarSesion?mensaje=1");
            }
        }




        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }


        private PayPal.Api.Payment payment;

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var ItemList = new ItemList()
            {
                items = new List<Item>()
            };

            if (Session["cart"] != "")
            {
                List<Models.Carrito> cart = (List<Models.Carrito>)(Session["cart"]);
                foreach (var item in cart)
                {
                    ItemList.items.Add(
                        new Item()
                        {
                            name = item.Productos.nombreProducto.ToString(),
                            currency = "USD",
                            price = item.Productos.precio.ToString(),
                            quantity = item.Cantidad.ToString(),
                            sku = "sku"


                        });
                }


                var payer = new Payer() { payment_method = "paypal" };

                var rediUrl = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };



                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = Session["subto"].ToString(),

                };

                var amount = new Amount()
                {
                    currency = "USD",

                    total = Session["SesTotal"].ToString(),
                    details = details,

                };

                var transactionList = new List<Transaction>();
                // Adding description about the transaction
                transactionList.Add(new Transaction()
                {
                    description = "Transaction description",
                    invoice_number = (new Random()).Next(100000).ToString(), //Generate an Invoice No
                    amount = amount,
                    item_list = ItemList
                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = rediUrl
                };

            }

            return this.payment.Create(apiContext);






        }

        public ActionResult ErrorTransaccion()
        {
            return View();
        }

        public ActionResult Exito()
        {
            return View();
        }
    }
}
