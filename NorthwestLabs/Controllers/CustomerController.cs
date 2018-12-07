﻿using NorthwestLabs.DAL;
using NorthwestLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NorthwestLabs.Controllers
{
    public class CustomerController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PharmacologyCatalog()
        {
            return View();
        }

        public ActionResult ProtocolCatalog()
        {
            return View();
        }

        //Require Customer Info for Getting a Quote
        [HttpGet]
        public ActionResult GetCustomerInfo()
        {
            return View();
        }
        
        //Check if login info is correct
        [HttpPost]
        public ActionResult GetCustomerInfo(FormCollection form)
        {
            string username = form["CustUserName"].ToString();
            CustomerLogin login = db.CustomerLogins.Find(username);
            var custID = db.Database.SqlQuery<Int32>("SELECT CustID FROM Customer WHERE CustUserName = " + login.CustUserName + ";");
            int id = Convert.ToInt32(custID);
            if (login == null)
            {
                ViewBag.ErrorMessage = "Incorrect Login";
                return View();
            }

            return RedirectToAction("GetQuote", id);
        }
        [HttpGet]
        public ActionResult CreateCustomer()
        {
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateDesc");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryDesc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid) 
            {
                customer.customerLogin.LastLogin = System.DateTime.Now;
                customer.customerLogin.LastPasswordChange = System.DateTime.Now;
                customer.customerLogin.CustUserName = customer.CustUserName;
                db.CustomerLogins.Add(customer.customerLogin);
                db.Customers.Add(customer);
                db.SaveChanges();
                int id = customer.CustID;
                ViewBag.ClientID = id;
                return View("GetQuote", db.Assays.ToList());
            }
            return View();
            
        }

        [HttpGet]
        public ActionResult GetQuote(int id)
        {
            ViewBag.ClientID = id;
            return View(db.Assays.ToList());
        }

        [HttpPost]
        public ActionResult GetQuote(FormCollection form)
        {
            //Create variables for form inputs
            String assays = form["quoteAssays"].ToString();
            String ClientID = form["ClientID"].ToString();

            //Parse text for assay IDs
            String[] assayIDs = assays.Split(',');
            List<int> listAssayIDs = new List<int>();
            foreach (var assayID in assayIDs)
            {
                listAssayIDs.Add(Convert.ToInt32(assayID));
            }

            //Remove empty first item in list
            listAssayIDs.Remove(listAssayIDs[0]);

            //Get customer object for customerID
            Customer customer = db.Customers.Find(ClientID);

            //Add Assay descriptions to ViewBag
            List<Assay> listAssays = new List<Assay>();
            for (int i = 0; i < listAssayIDs.Count; i++)
            {
                listAssays.Add(db.Assays.Find(listAssayIDs[i]));
                ViewBag.Assay[i] = listAssays[i].Description;
            }


            //Assign ViewBag for Customer Information
            ViewBag.Name = customer.CustFirstName + " " + customer.CustLastName;
            ViewBag.Email = customer.CustEmail;

            //If Customer info not null, send emails to customer and sales dept
            if (customer.CustFirstName != null && customer.CustEmail != null)
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);


                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("labsnorthwest0@gmail.com", "byuIntex");
                MailMessage Custmail = new MailMessage();

                //Setting From , To and CC
                Custmail.From = new MailAddress("labsnorthwest0@gmail.com", "Northwest Labs");
                Custmail.To.Add(new MailAddress(customer.CustEmail));
                Custmail.CC.Add(new MailAddress("labsnorthwest0@gmail.com"));
                Custmail.Subject = "Northwest Labs Quote Request Submitted Successfully";
                Custmail.Body = "Thank you, " + customer.CustFirstName + " " + customer.CustLastName + ", for contacting Northwest Labs! " +
                    "<br/>The assays that your requested quotes for are as follows: <br /> <ul>";
                foreach (var assay in listAssays)
                {
                    Custmail.Body += "<li> " + assay.Description + "<li />";
                }
                Custmail.Body += "<ul /> A sales representative will review your request and respond promptly. <br /> Northwest Labs";
                Custmail.BodyEncoding = Encoding.UTF8;
                Custmail.IsBodyHtml = true;


                smtpClient.Send(Custmail);
            }

            //Return Quote Request Confirmation View
            return View("QuoteConf");
        }
    }
}