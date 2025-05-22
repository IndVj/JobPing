JobPing is a clean-architecture .NET 8 console application that scrapes job listings from company career pages, filters them using custom keywords (e.g., `.NET`, `C#`, `ASP.NET`), and sends real-time alerts to a Discord channel. 
It also tracks which jobs have already been notified using a local JSON file, and can be automated using GitHub Actions.

---

## 📁 Project Structure

```
JobPing/
├── Core/                 # Interfaces and models
├── Infrastructure/       # Web scraping, persistence, and notification logic
├── App/                  # Console entry point and configuration
│   ├── appsettings.json
│   ├── JobPingApp.cs
│   └── Program.cs
├── .github/workflows/   # GitHub Actions workflow
├── jobs.json             # Tracks notified jobs
```

---

## 🚀 How to Run Locally

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/JobPing.git
   cd JobPing/App
   ```

2. **Configure `appsettings.json`**
   Create the file if missing:
   ```json
   {
     "JobPages": [
       "https://example.com/careers1",
       "https://example.com/careers2"
     ],
     "DiscordWebhookUrl": "https://discord.com/api/webhooks/your_id/your_token",
     "Keywords": [ ".NET", "C#", "ASP.NET" ]
   }
   ```

3. **Run the App**
   ```bash
   dotnet run
   ```

---

## 🤖 GitHub Actions Automation

The GitHub Actions workflow (`.github/workflows/schedule.yml`) runs every 6 hours and does the following:
- Builds and runs the app
- Sends notifications for new jobs
- Updates `jobs.json`
- Commits changes to the repository

To enable:
1. Commit and push the project to GitHub
2. Make sure `jobs.json` is included and tracked
3. Add secrets via GitHub repository settings (if hiding sensitive values)

---

## 🧾 Sample Discord Webhook Setup
1. Open Discord → your server
2. Go to **Channel Settings → Integrations → Webhooks**
3. Click **Create Webhook**, name it, and copy the URL
4. Paste the URL into `appsettings.json`

---

## 🛑 .gitignore Suggestions
```gitignore
# Build and IDE
**/bin/
**/obj/
.vscode/
*.user

# Config and state
App/appsettings.json

# Track this if using GitHub Actions
!App/jobs.json
```

---

## 📚 Credits & Notes
- Built with .NET 8 and HtmlAgilityPack
- Uses Discord Webhooks for instant alerts
- Designed for modularity using Clean Architecture

Feel free to fork, adapt, or extend JobPing for your own job tracking needs!
