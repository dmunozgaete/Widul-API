using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace API.Endpoints.Events.Services
{
    public class Thumbnail : Gale.REST.Http.HttpReadActionResult<String>
    {

        public Thumbnail(String targetEvent) : base(targetEvent) { }


        /// <summary>
        /// Merge Image's
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private System.IO.MemoryStream MergeImage(byte[] map, Models.EventDetails eventData)
        {

            #region PICTURE PROCESS
            //------------------------------------------------------------------------------------
            //ADD LABEL'S
            System.IO.MemoryStream originalImageStream = new System.IO.MemoryStream(map);
            System.Drawing.Image image = System.Drawing.Image.FromStream(originalImageStream);

            //CROP ACCORDING TO SCALE FIT FOR FACEBOOK
            //https://developers.facebook.com/docs/sharing/best-practices
            System.Drawing.Image croopedImage = ResizeImage(image, new Size(640, 336));

            Bitmap rearImage = new Bitmap(new Bitmap(croopedImage));

            //Height = 640
            //Width = 1200
            using (System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(rearImage))
            {

                graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphic.SmoothingMode = SmoothingMode.AntiAlias;

                GraphicsPath gfxPath = new GraphicsPath();

                Color PrimaryColor = Color.FromArgb(255, 63, 81, 181); //Primary Color (Indigo)
                Pen WhitePen = new Pen(Color.White, 1);


                // CALENDAR BOX
                #region CALENDAR
                Point CalendarPosition = new Point(10, 10);
                int CornerRadius = 20;
                Rectangle Bounds = new Rectangle(CalendarPosition, new Size(130, 130));

                gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

                gfxPath.CloseAllFigures();
                graphic.FillPath(new SolidBrush(PrimaryColor), gfxPath);
                graphic.DrawPath(WhitePen, gfxPath);
                #endregion

                //CENTER ALIGNEMNT
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                //CALENDAR TEXT (MONTH)
                string monthName = eventData.date.ToString("MMM", new System.Globalization.CultureInfo("es-CL")).ToUpper().Substring(0, 3);
                Font monthFont = new Font(FontFamily.GenericSansSerif, 35, FontStyle.Regular);

                //Limit are the (35%) of the rectangle height
                Rectangle limits = new Rectangle(
                    new Point(Bounds.X, Bounds.Y + Convert.ToInt16((Bounds.Height * 0.65))),
                    new Size(Bounds.Width, Convert.ToInt16(Bounds.Height - Bounds.Height * 0.65))
                );

                //graphic.FillRectangle(Brushes.Yellow, limits);
                graphic.DrawString(monthName, monthFont, Brushes.White, limits, sf);


                //CALENDAR TEXT (DAY)
                string day = eventData.date.Day.ToString().PadLeft(2, '0');
                Font dayFont = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);

                //Limit are the (35%) of the rectangle height
                Rectangle dayLimits = new Rectangle(
                    new Point(Bounds.X, Bounds.Y+10),
                    new Size(Bounds.Width, Convert.ToInt16(Bounds.Height - Bounds.Height * 0.35))
                );

                //graphic.FillRectangle(Brushes.Red, dayLimits);
                graphic.DrawString(day, dayFont, Brushes.White, dayLimits, sf);

            }

            var processedImage = new System.IO.MemoryStream();
            rearImage.Save(processedImage, System.Drawing.Imaging.ImageFormat.Png);
            processedImage.Position = 0; //RESET STREAM POSITION TO 0 --> .NET STREAMCONTENT BUG???
            //------------------------------------------------------------------------------------
            #endregion

            return processedImage;

        }

        /// <summary>
        /// Resize Image to Desired Width
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="destinationSize"></param>
        /// <returns></returns>
        private Image ResizeImage(Image imgToResize, Size destinationSize)
        {
            var originalWidth = imgToResize.Width;
            var originalHeight = imgToResize.Height;

            //how many units are there to make the original length
            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            //get the shorter side
            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            //start cropping from the center
            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            //crop the image from the specified location and size
            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            //the future size of the image
            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            //fill-in the whole bitmap
            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //generate the new image
            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;

        }


        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {

            using (var svc = new Gale.Db.DataService("SP_GET_Event"))
            {
                svc.Parameters.Add("EVNT_Token", this.Model);

                Gale.Db.EntityRepository repo = this.ExecuteQuery(svc);

                //Get tables
                Models.EventDetails eventDetail = repo.GetModel<Models.EventDetails>().FirstOrDefault();

                //----------------------------------------------------------------------------------------------------
                //Guard Exception's
                Gale.Exception.RestException.Guard(() => eventDetail == null, "EVENT_DONT_EXISTS", API.Errors.ResourceManager);
                //----------------------------------------------------------------------------------------------------

                //Setting Values ;)!
                eventDetail.place = repo.GetModel<Models.Place>(3).FirstOrDefault();

                string imageUrl = String.Format(
                    "http://maps.google.com/maps/api/staticmap?size=1200x630&markers=color:red|{0},{1}",
                    eventDetail.place.lat,
                    eventDetail.place.lng
                );

                //---------------------------------------------
                //Get Stream Image from the User
                byte[] imageMap = null;

                //Try to download the image
                var webClient = new WebClient();
                imageMap = webClient.DownloadData(imageUrl);
                //---------------------------------------------


                System.IO.MemoryStream streamedImage = MergeImage(imageMap, eventDetail);

                //Create Response
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StreamContent(streamedImage)
                };

                /*
                response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(30)
                };
                 * */

                //Add Content-Type Header
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                return Task.FromResult(response);

            }
        }
    }
}