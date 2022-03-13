using DruivendoosAPI.DTOs;
using DruivendoosAPI.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeKit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DruivendoosAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
            
        }

        [HttpPost("SendEmail/{form}")]
        public ActionResult SendEmail(FormDTO form)
        {
            var message = new MimeMessage();

            try
            {
                message.From.Add(new MailboxAddress(form.Name, form.Email));
                //hier zeggen naar waar een email moet verstuurd worden
                message.To.Add(new MailboxAddress("DruivenDoos Admin", "druivendoos@glennbeeckman.be"));
                message.Subject = form.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = string.Format(
                    $@"<p>{form.Name} heeft u een e-mail gestuurd vanaf de Druivendoos contact pagina</p>
                            <ul>                               
                                <li>Opgegeven Emailadres waarmee de persoon de e-mail heeft verstuurd: {form.Email}</li>
                            </ul>
                            
                            <p>Het verstuurde bericht:</p>
                            <pre>{form.Message}</pre>
                        "
                    );
                message.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                //hier moet de correcte gegevens ingevuld worden
                //mailserver van host + emailadres van host
                client.Connect("smtp-auth.mailprotect.be", 587, SecureSocketOptions.StartTls);
                //dit is authenticatie de persoon die admin is krijgt alleen een email
                client.Authenticate("druivendoos@glennbeeckman.be", "druiven12345");
                client.Send(message);
                client.Disconnect(true);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Uploads chosen image
        /// </summary>
        /// <param name="wineId"></param>
        /// <returns>returns status of upload (BadRequest = failed or Created)</returns>
        [HttpPost("UploadImage")]
        [Authorize("read:admin")]
        public async Task<HttpResponseMessage> UploadImage()
        {
            var accesKey = configuration.GetConnectionString("AccessKey");
            try
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    var file = Image;
                    if (CloudStorageAccount.TryParse(accesKey, out CloudStorageAccount storageAccount))
                    {
                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                        CloudBlobContainer container = blobClient.GetContainerReference("images");
                        await container.CreateIfNotExistsAsync();
                        var picBlob = container.GetBlockBlobReference(file.FileName);
                        picBlob.Properties.ContentType = "image/jpg";
                        await picBlob.UploadFromStreamAsync(file.OpenReadStream());

                    }

                }
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}