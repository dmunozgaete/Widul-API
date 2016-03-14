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
    /// Send a New Comment notification to the creator of the event
    /// </summary>
    public class NewParticipant
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="model"></param>
        public NewParticipant(MailMessage mail, dynamic model)
        {
            mail.Body = RenderView(model);
            PrepareAndSend(mail,model);
        }

        private void PrepareAndSend(MailMessage mail, dynamic model)
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
            String resourcePath = "API.Endpoints.Events.Templates.header.jpg";
            System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath);
            LinkedResource header = new LinkedResource(stream , "image/jpg");
            header.ContentId = "header";
            
            alternateView.LinkedResources.Add(header);
            //----------------------------------

            //----------------------------------
            // PARTICIPANT IMAGE
            String resourcePath2 = "API.Endpoints.Events.Templates.person.png";
            System.IO.Stream stream2 = assembly.GetManifestResourceStream(resourcePath2);
            LinkedResource participant_icon = new LinkedResource(stream2, "image/png");
            participant_icon.ContentId = "participant";

            alternateView.LinkedResources.Add(participant_icon);
            //----------------------------------

            //----------------------------------
            // USER IMAGE


            byte[] user_photo = (byte[])model.Participant.photo;
            System.IO.MemoryStream avatar_stream = new System.IO.MemoryStream(user_photo);
            LinkedResource avatar_link = new LinkedResource(avatar_stream, "image/png");
            avatar_link.ContentId = "avatar";
            alternateView.LinkedResources.Add(avatar_link);
            

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
            String resourcePath = "API.Endpoints.Events.Templates.NewParticipant.cshtml";

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