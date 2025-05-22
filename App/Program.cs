using App;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var jobPages = config.GetSection("JobPages").Get<string[]>() ?? throw new InvalidOperationException("Job Page Section is null");
var webhookUrl = config["DiscordWebhookUrl"] ?? throw new InvalidOperationException("DiscordWebhookUrl is null");
var keywords = config.GetSection("Keywords").Get<string[]>() ?? throw new InvalidOperationException("Keywords Section is null");

await JobPingApp.RunAsync(jobPages, webhookUrl, keywords);
