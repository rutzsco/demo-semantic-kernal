using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.SemanticKernel;
using Demo.SemanticKernal.Services.Skills;
using Microsoft.SemanticKernel.SemanticFunctions;

namespace Demo.SemanticKernal
{
    public class JokeEndpoint
    {
        private IKernel _sk;

 
        public JokeEndpoint(IKernel sk)
        {
            _sk = sk;
        }

        [FunctionName("JokeEndpoint")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string topic = req.Query["topic"];

            var jokeFunction = _sk.RegisterJokeSkill();
            var result = await jokeFunction.InvokeAsync(topic);

            return new OkObjectResult(result.Result);
        }
    }
}