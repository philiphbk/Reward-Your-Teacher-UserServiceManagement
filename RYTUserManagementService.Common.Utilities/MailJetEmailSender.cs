﻿using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserManagementService.Common.Utilities
{
    public class MailJetEmailSender : IEmailSender 
    {
        private readonly IConfiguration _configuration;
        public MailJetOptions _mailJetOptions;
        public MailJetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();

            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);
            {
                //Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
              new JObject {
      {
       "From",
       new JObject {
        {"Email", "glory4christ@proton.me"},
        {"Name", "Erhieyovwe"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Erhieyovwe"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
        htmlMessage
      }, 
     }
             });
            await client.PostAsync(request);
           
        }
    }
}
    




