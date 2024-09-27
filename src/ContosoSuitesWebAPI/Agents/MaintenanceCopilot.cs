using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ContosoSuitesWebAPI.Entities;
namespace ContosoSuitesWebAPI.Agents
{
    public class MaintenanceCopilot(Kernel kernel)
    {
        public readonly Kernel _kernel = kernel;
        private ChatHistory _history = new();

        public async Task<string> Chat(string userPrompt)
        {
            // Exercise 5 Task 2 TODO #4: Comment out or delete the throw exception line below,
            // and then uncomment the remaining code in the function.
        
            var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

            var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            _history.AddUserMessage(userPrompt);

            var result = await chatCompletionService.GetChatMessageContentAsync(
                _history,
                executionSettings: openAIPromptExecutionSettings,
                _kernel
            );

            _history.AddAssistantMessage(result.Content!);

            return result.Content!;
        }

   
    }
}
