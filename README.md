# JobPing

**JobPing** is a modular, clean-architecture **.NET 8 console application** that scrapes job listings from company career pages, filters them using specific keywords (like `.NET`, `C#`, `ASP.NET`), and sends real-time notifications to a Discord channel.

It’s designed to be site-agnostic and extensible: you can add a new scraper per site by implementing a simple interface.

---

## 🔧 Features
- ✅ Clean architecture with separation of concerns: Core, Infrastructure, and App layers
- 🌐 Supports multiple career sites with plug-and-play scraper classes
- 🧠 Filters jobs by custom keywords
- 🔔 Sends alerts to Discord using webhooks
- 💾 Tracks already-notified jobs in a local `jobs.json` file
- ⏱ Automatable with GitHub Actions for scheduled job runs (e.g., every 6 hours)

---

## 🧱 Architecture

```
JobPing/
├── Core/                 # Interfaces and shared models
│   └── IJobScraper.cs, Job.cs
├── Infrastructure/       # Implements scrapers, Discord notifier, JSON store
│   └── LesJeudisScraper.cs, DiscordNotifier.cs, JsonJobRepository.cs
├── App/                  # Console entry point and config loader
│   ├── appsettings.json
│   ├── JobPingApp.cs
│   └── Program.cs
├── jobs.json             # Tracks seen jobs
├── .github/workflows/    # GitHub Actions workflow
```

---

## 🚀 Getting Started

### 1. Clone the Repo
```bash
git clone https://github.com/your-username/JobPing.git
cd JobPing
```

### 2. Setup Configuration
Create `App/appsettings.json`:
```json
{
  "JobPages": [
    "https://lesjeudis.com/jobs"
  ],
  "DiscordWebhookUrl": "https://discord.com/api/webhooks/...",
  "Keywords": [ ".NET", "C#", "ASP.NET" ]
}
```

> 💡 Use `appsettings.template.json` to share structure without secrets

### 3. Run Locally
```bash
cd App
dotnet run
```

> Ensure you are using **.NET 8 SDK**:
```bash
dotnet --version  # should be 8.x.x
```

---

## 🤖 GitHub Actions (Optional)
Automate scraping and Discord alerts every 6 hours. Workflow file: `.github/workflows/schedule.yml`

### Includes:
- Restore, build, run app
- Commit updated `jobs.json` to repo

---

## 🛠 Adding a New Scraper
Each site is different. To scrape a new site:
1. Create a class in `Infrastructure/` implementing `IJobScraper`
2. Inject it in `JobPingApp.cs` based on the URL

Example:
```csharp
public class LesJeudisScraper : IJobScraper
{
    public async Task<List<Job>> ScrapeJobsAsync() {
        // site-specific logic here
    }
}
```

---

## 📄 .gitignore Suggestions
```gitignore
# Binaries
**/bin/
**/obj/

# User settings
.vscode/
*.user

# Secrets
App/appsettings.json

# Keep runtime job state
!App/jobs.json
```

---

## 📌 Notes
- Some sites require custom XPath selectors or authentication.
- HTML structures change — scrapers may need maintenance.
- Respect robots.txt and TOS when scraping public sites.

---

## 🙌 Contributing
1. Fork the repo
2. Add support for a new job site
3. Submit a pull request!

---

## 🔗 License
MIT — free to use, modify, and share.
