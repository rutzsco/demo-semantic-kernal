using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.SemanticKernel.SkillDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SemanticKernal.Services.Skills
{
    public static class JokeSkill
    {
        public static ISKFunction RegisterJokeSkill(this IKernel sk)
        {
            string skPrompt = PromptUtility.GetPromptByName("JokePrompt");

            var promptConfig = new PromptTemplateConfig
            {
                Completion =
                {
                    MaxTokens = 1000,
                    Temperature = 0.9,
                    TopP = 0.0,
                    PresencePenalty = 0.0,
                    FrequencyPenalty = 0.0,
                }
            };
            var promptTemplate = new PromptTemplate(skPrompt, promptConfig, sk);
            var functionConfig = new SemanticFunctionConfig(promptConfig, promptTemplate);
            var jokeFunction = sk.RegisterSemanticFunction("FunSkill", "Joke", functionConfig);
            return jokeFunction;
        }
    }
}
