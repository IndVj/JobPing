using App;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var jobSitesFromEnv = Environment.GetEnvironmentVariable("JOB_SITE_URLS");

var jobPages = jobSitesFromEnv?.Split(',', StringSplitOptions.RemoveEmptyEntries) ??    
               config.GetSection("JobPages").Get<string[]>() ??
               throw new InvalidOperationException("Job Page Section is null");

var webhookUrl = Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_URL") ??
                config["DiscordWebhookUrl"] ?? 
                throw new InvalidOperationException("DiscordWebhookUrl is null");

var keywords = config.GetSection("Keywords").Get<string[]>() 
    ?? throw new InvalidOperationException("Keywords Section is null");

await JobPingApp.RunAsync(jobPages, webhookUrl, keywords);
