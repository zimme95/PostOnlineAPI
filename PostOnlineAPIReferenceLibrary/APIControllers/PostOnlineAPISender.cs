﻿using Newtonsoft.Json;
using PostOnlineAPIReferenceLibrary.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PostOnlineAPIReferenceLibrary.APIControllers
{
    public interface IPostOnlineAPISender
    {
        /// <summary>
        /// Method for getting a SenderDTO object from the PostOnline system.
        /// </summary>
        /// <param name="senderID"></param>
        /// <returns></returns>
        Task<SenderDTO> GetSender(long senderID);
        /// <summary>
        /// Method for updating an exsiting Sender in the PostOnline system.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        Task<bool> UpdateSender(SenderDTO sender);
        /// <summary>
        /// Method for creating a new Sender in the PostOnline system.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        Task<bool> CreateSender(SenderDTO sender);
    }

    public class PostOnlineAPISender : IPostOnlineAPISender
    {
        public async Task<SenderDTO> GetSender(long senderID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API.url + "Senders/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            SenderDTO sender = null;
            HttpResponseMessage response = await client.GetAsync(senderID.ToString()).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                sender = JsonConvert.DeserializeObject<SenderDTO>(responseString);
                return sender;
            }
            return null;
        }

        public async Task<bool> UpdateSender(SenderDTO sender)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API.url + "Senders/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent httpobject = new StringContent(JsonConvert.SerializeObject(sender), Encoding.UTF8, "application/json");
            var id = sender.SenderID;
            var response = await client.PutAsync(id.ToString(), httpobject).ConfigureAwait(false);
            var expected = (int)HttpStatusCode.NoContent;
            if (expected == (int)response.StatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreateSender(SenderDTO sender)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API.url + "Senders/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent httpobject = new StringContent(JsonConvert.SerializeObject(sender), Encoding.UTF8, "application/json");
            var id = sender.SenderID;
            HttpResponseMessage response = await client.PostAsync("", httpobject).ConfigureAwait(false);
            var expected = (int)HttpStatusCode.OK;
            if (expected == (int)response.StatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
