using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using RazorTemplates.Core;

namespace API.Endpoints.Events.Services.Mail
{
    /// <summary>
    /// Send a Welcome Email
    /// </summary>
    public class Invitation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="model"></param>
        public Invitation(MailMessage mail, dynamic model)
        {
            mail.Body = RenderView(model);
            PrepareAndSend(mail);
        }

        private void PrepareAndSend(MailMessage mail)
        {

            var assembly = this.GetType().Assembly;

            mail.IsBodyHtml = true;
            mail.From = new MailAddress(
                System.Configuration.ConfigurationManager.AppSettings["Mail:Account"],
                Templates.Mail.Invitation_From_DisplayName
            );
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html);

            //----------------------------------
            // HEADER IMAGE
            String resourcePath = "API.Endpoints.Events.Templates.header.png";
            System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath);
            LinkedResource header = new LinkedResource(stream);
            header.ContentId = "header";
            alternateView.LinkedResources.Add(header);
            //----------------------------------

            mail.AlternateViews.Add(alternateView);

            SmtpClient client = new SmtpClient();
            client.Send(mail);

            //Dispose Stream's
            stream.Dispose();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string RenderView(dynamic model)
        {
            //----------------------------------
            var assembly = this.GetType().Assembly;
            String resourcePath = "API.Endpoints.Events.Templates.Invitation.cshtml";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                //------------------------------------------------------------------------------------------------------
                // GUARD EXCEPTIONS
                Gale.Exception.RestException.Guard(() => stream == null, "TEMPLATE_DONT_EXIST", API.Errors.ResourceManager);
                //------------------------------------------------------------------------------------------------------

                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    var view = Template.Compile(reader.ReadToEnd());
                    return view.Render(model);
                }
            }
            //----------------------------------
        }

    }
}