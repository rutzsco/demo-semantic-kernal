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
using Microsoft.SemanticKernel.Skills.OpenAPI.Extensions;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Skills.Web.Bing;

namespace Demo.SemanticKernal
{
    public class GitHubEndpoint
    {
        private IKernel _sk;

 
        public GitHubEndpoint(IKernel sk)
        {
            _sk = sk;
        }

        [FunctionName("GitHubEndpoint")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string topic = req.Query["topic"];


            var plugin = await _sk.ImportAIPluginAsync("GitHubPlugin", new Uri("https://www.klarna.com/.well-known/ai-plugin.json"));

            var planner = new ActionPlanner(_sk);

            var ask = "i want to buy a laptop and my budget is 200 dollars";
            var plan = await planner.CreatePlanAsync(ask);
            var result = await plan.InvokeAsync();

            var bingConnector = new BingConnector("");

            //Console.WriteLine("Plan results:");
            //Console.WriteLine(result.Variables["stepsTaken"]);

            return new OkObjectResult(result);
        }
    }
}