﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderItem.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        Cart obj = new Cart();



        static string GetToken(string url)
        {
            var user = new User { name = "kunal", password = "savsani8658kunal" };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }
     

        [HttpPost]
       [Route("api/Order")]
        public Cart PostOrder([FromBody] Cart c)
        {
            string token = GetToken("https://localhost:44307/api/Token");

            obj.Id = 1;
            obj.uesrId = 1;
            obj.menuItemId = c.menuItemId;
            int id = obj.menuItemId;

        
            using (var client = new HttpClient())
            {
                
           //       client.BaseAddress = new Uri("https://localhost:44307/");   
                client.BaseAddress = new Uri("http://52.143.242.4/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   HttpResponseMessage response = new HttpResponseMessage();
                  response =  client.GetAsync("api/MenuItem/"+id).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                obj.menuItemName = result;
                return obj;
            }
       }
    }
}

