using DB_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DB_UI.Controllers
{
    public class DefaultController : Controller
    {
        static async Task<List<Customer>> GetCustAsync()
        {
            List<Customer> temp = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/GetCustomer");
            if (response.IsSuccessStatusCode)
            {
                temp = await response.Content.ReadAsAsync<List<Customer>>();
            }
            return temp;
        
        }

        static async Task<Customer> GetCustAsync(int id)
        {
            Customer temp = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/Get/"+id);
            if (response.IsSuccessStatusCode)
            {
                temp = await response.Content.ReadAsAsync<Customer>();
            }
            return temp;
  
        }
        // GET: Default
        public async Task<ActionResult> Index()
        {
            List<Customer> lists = await GetCustAsync();
            
         
            return View(lists);
        }

        // GET: Default/Details/5
       
        public async Task<ActionResult> Details(int id)
        {
            Customer temp = await GetCustAsync(id);
            return View(temp);
        }

        // GET: Default/Create
        public ActionResult CreateAsync()
        {

            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/Post");

                var createCustomer = client.PostAsJsonAsync<Customer>("Customer", customer);
                createCustomer.Wait();

                var savedata = createCustomer.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                // return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/Get/" + id);
            Customer customer = await response.Content.ReadAsAsync<Customer>();
            return View(customer);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Customer_name,Customer_adress,Customer_country,Pasaport_number,Customer_phone,Customer_email")] int? id, Customer customer)
        {
           
                // TODO: Add update logic here
                string json = JsonConvert.SerializeObject(customer);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://localhost:58513/Api/Customer/Put/"+id;
                var client = new HttpClient();
                //var formatter = new System.Net.Http.Formatting.BaseJsonMediaTypeFormatter();

                await client.PutAsync(url, data);

              
                //return View(customer);
                return RedirectToAction("Index");
        
        }

        // GET: Default/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/Get/" + id);
            Customer customer = await response.Content.ReadAsAsync<Customer>();

            return View(customer);
        }

        // POST: Default/Delete/5
        [HttpPost,ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                // TODO: Add delete logic here
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/Get/" + id);

                var url = "http://localhost:58513/Api/Customer/Delete/" + id;
                await client.DeleteAsync(url);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> GetDashAsync()
        {
            
            Dash temp = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:58513/Api/Customer/GetDashCustomer");
            if (response.IsSuccessStatusCode)
            {
                temp = await response.Content.ReadAsAsync<Dash>();
            }
            return View(temp);

        }
    }
}
