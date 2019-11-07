using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class CRMController : Controller
    {
        // GET: CRM
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(string firstname, string lastname, string telephone1, string emailaddress1)
        {
            var service = new CrmServiceClient("AuthType=Office365;Url=https://org2837659f.crm4.dynamics.com;"
                + "Username=danj@CRM097147.OnMicrosoft.com;Password=TechLabs1;");

            Entity en = new Entity("contact");

            string Emailformte = "{ Email = " + emailaddress1 + " }";
            var query = (from contact in new OrganizationServiceContext(service).CreateQuery("contact")
                         select new
                         {
                             Email = contact["emailaddress1"]
                         }).ToList();

            bool exits = false;
            foreach (var item in query)
            {
                if (item.ToString() == Emailformte)
                {
                    exits = true;
                    break;
                }

            }

            if (exits == true)
            {
                ViewBag.EmailMessage = "This Email is Registered before";

                return View();
            }
            else
            {
                service.Create(new Entity("contact")
                {
                    ["emailaddress1"] = emailaddress1,
                    ["firstname"] = firstname,
                    ["lastname"] = lastname,
                    ["telephone1"] = telephone1,
                    ["jobtitle"] = "ASP.NET Developer",
                });
                return RedirectToRoute("HomePage");

            //    return RedirectToAction("Success", "CRM");

            }
        }
    }
}