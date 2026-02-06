using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Agents;

public class WriterCriticAgentService : IAgentService
{
	private const int MaxIterations = 3;
	private readonly IConfiguration _configuration;
	private readonly IProductService _productService;

	public WriterCriticAgentService(IProductService productService, IConfiguration configuration)
	{
		_productService = productService;
		_configuration = configuration;
	}

	public async Task<AgentResponse> ProcessQuestionAsync(string question)
	{
		string apiKey = _configuration["OpenAI:ApiKey"]
						?? throw new InvalidOperationException("OpenAI API key not configured in appsettings.json");

		string modelId = _configuration["OpenAI:ModelId"] ?? "gpt-4o-mini";

		IKernelBuilder builder = Kernel.CreateBuilder();
		builder.AddOpenAIChatCompletion(modelId, apiKey);
		Kernel kernel = builder.Build();

		IChatCompletionService chatService = kernel.GetRequiredService<IChatCompletionService>();
		string productCatalog = await _productService.GetProductCatalogPromptAsync();

		string currentResponse = "";
		int iteration = 0;
		bool approved = false;
		string feedback = "";

		while (iteration < MaxIterations && !approved)
		{
			iteration++;

			// Writer Agent
			currentResponse = await GenerateWriterResponse(chatService, productCatalog, question, feedback, iteration);

			// Critic Agent
			CriticFeedback criticResult =
				await GetCriticFeedback(chatService, productCatalog, question, currentResponse);
			approved = criticResult.Approved;
			feedback = criticResult.Feedback;
		}

		return new AgentResponse
			(currentResponse, iteration, approved);
	}

	private async Task<string> GenerateWriterResponse(
		IChatCompletionService chatService,
		string productCatalog,
		string question,
		string feedback,
		int iteration)
	{
		string systemPrompt =
			$@"Jestes pomocnym asystentem sklepu PetWorld - sklepu internetowego z produktami dla zwierzat.
			Twoim zadaniem jest pomagac klientom znalezc odpowiednie produkty i udzielac porad.
			
			{productCatalog}
			
			ZASADY:
			1. Odpowiadaj po polsku, przyjazne i profesjonalnie
			2. Rekomenduj produkty z powyzszego katalogu, jesli pasuja do pytania
			3. Podawaj ceny produktow
			4. Jesli pytanie dotyczy produktu ktorego nie mamy, przepros i zaproponuj alternatywe
			5. Udzielaj krotkich, konkretnych odpowiedzi (max 3-4 zdania dla prostych pytan)
			6. Mozesz udzielac porad dotyczacych opieki nad zwierzetami";

		string userPrompt;
		if (iteration == 1 || string.IsNullOrEmpty(feedback))
			userPrompt = $"Pytanie klienta: {question}";
		else
			userPrompt =
				$"Pytanie klienta: {question}\n\nPopraw swoja poprzednia odpowiedz biorac pod uwage ten feedback: {feedback}";

		ChatHistory history = new();
		history.AddSystemMessage(systemPrompt);
		history.AddUserMessage(userPrompt);

		ChatMessageContent response = await chatService.GetChatMessageContentAsync(history);
		return response.Content ?? "Przepraszam, wystapil problem. Prosze sprobowac ponownie.";
	}

	private async Task<CriticFeedback> GetCriticFeedback(
		IChatCompletionService chatService,
		string productCatalog,
		string question,
		string response)
	{
		string systemPrompt =
			$@"Jestes krytykiem oceniajacym odpowiedzi asystenta sklepu PetWorld.

			{productCatalog}
			
			Ocen odpowiedz wedlug kryteriow:
			1. Czy odpowiedz jest pomocna i odpowiada na pytanie klienta?
			2. Czy rekomendowane produkty istnieja w katalogu i sa odpowiednie?
			3. Czy podane ceny sa prawidlowe?
			4. Czy odpowiedz jest profesjonalna i przyjazna?
			5. Czy odpowiedz nie jest zbyt dluga?
			
			Odpowiedz TYLKO w formacie JSON:
			{{""approved"": true, ""feedback"": """"}}
			lub
			{{""approved"": false, ""feedback"": ""krotki opis co poprawic""}}";

		string userPrompt = $"Pytanie klienta: {question}\n\nOdpowiedz do oceny: {response}";

		ChatHistory history = new();
		history.AddSystemMessage(systemPrompt);
		history.AddUserMessage(userPrompt);

		ChatMessageContent result = await chatService.GetChatMessageContentAsync(history);
		string content = result.Content ?? "{\"approved\": true, \"feedback\": \"\"}";

		try
		{
			content = ExtractJson(content);
			CriticFeedback? feedbackResult = JsonSerializer.Deserialize<CriticFeedback>(content,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			return feedbackResult ?? new CriticFeedback(true, "");
		}
		catch
		{
			return new CriticFeedback(true, "");
		}
	}

	private string ExtractJson(string content)
	{
		int start = content.IndexOf('{');
		int end = content.LastIndexOf('}');
		if (start >= 0 && end > start)
			return content.Substring(start, end - start + 1);

		return content;
	}
}