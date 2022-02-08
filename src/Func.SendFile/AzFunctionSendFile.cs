using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Func.SendFile
{
    public class AzFunctionSendFile
    {
        private readonly ILogger<AzFunctionSendFile> _logger;

        public AzFunctionSendFile(ILogger<AzFunctionSendFile> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("send-file")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var reqFormData = await req.ReadFormAsync();

            string data = reqFormData["data"];

            if (data == null)
            {
                return new BadRequestObjectResult(Errors.DATA_NOT_PROVIDED);
            }


            var file = reqFormData.Files["myFile"];

            if (file == null)
            {
                return new BadRequestObjectResult(Errors.FILE_NOT_PROVIDED);
            }

            // Do whatever you want with the data and files...

            return new OkObjectResult("This HTTP triggered function executed successfully.");
        }
    }
}
