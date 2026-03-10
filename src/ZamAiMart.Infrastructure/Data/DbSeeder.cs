using BCrypt.Net;
using ZamAiMart.Core.Entities;

namespace ZamAiMart.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Seed default admin
        if (!context.AdminUsers.Any())
        {
            context.AdminUsers.Add(new AdminUser
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234")
            });
            await context.SaveChangesAsync();
        }

        // Seed categories
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { CategoryName = "AI Chatbots" },
                new() { CategoryName = "AI Image Generators" },
                new() { CategoryName = "AI Video Tools" },
                new() { CategoryName = "AI Code Assistants" },
                new() { CategoryName = "AI Writing Tools" },
                new() { CategoryName = "AI Voice & Audio" },
                new() { CategoryName = "AI Music Generators" },
                new() { CategoryName = "AI Business Tools" },
                new() { CategoryName = "AI Productivity" },
                new() { CategoryName = "AI Design Tools" },
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        // Seed sample AI websites
        if (!context.AIWebsites.Any())
        {
            var websites = new List<AIWebsite>
            {
                new() { Name = "ChatGPT", Category = "AI Chatbots", PriceINR = 1660, IsFree = false, WebsiteURL = "https://chat.openai.com", Description = "OpenAI's conversational AI assistant, perfect for writing, coding, and analysis.", LogoURL = "https://upload.wikimedia.org/wikipedia/commons/0/04/ChatGPT_logo.svg" },
                new() { Name = "Google Gemini", Category = "AI Chatbots", PriceINR = 0, IsFree = true, WebsiteURL = "https://gemini.google.com", Description = "Google's multimodal AI assistant with deep integration across Google services.", LogoURL = "https://upload.wikimedia.org/wikipedia/commons/8/8a/Google_Gemini_logo.svg" },
                new() { Name = "Claude", Category = "AI Chatbots", PriceINR = 1660, IsFree = false, WebsiteURL = "https://claude.ai", Description = "Anthropic's AI assistant focused on safety and helpful conversations.", LogoURL = "https://upload.wikimedia.org/wikipedia/commons/2/28/Anthropic_logo.svg" },
                new() { Name = "Microsoft Copilot", Category = "AI Chatbots", PriceINR = 0, IsFree = true, WebsiteURL = "https://copilot.microsoft.com", Description = "Microsoft's AI companion powered by GPT-4, integrated with Office 365.", LogoURL = "" },
                new() { Name = "Perplexity AI", Category = "AI Chatbots", PriceINR = 1660, IsFree = false, WebsiteURL = "https://perplexity.ai", Description = "AI-powered search engine that provides answers with citations.", LogoURL = "" },
                new() { Name = "Midjourney", Category = "AI Image Generators", PriceINR = 830, IsFree = false, WebsiteURL = "https://midjourney.com", Description = "Industry-leading AI art generator known for stunning, artistic images.", LogoURL = "" },
                new() { Name = "DALL-E 3", Category = "AI Image Generators", PriceINR = 1660, IsFree = false, WebsiteURL = "https://openai.com/dall-e-3", Description = "OpenAI's advanced image generation model with precise prompt adherence.", LogoURL = "" },
                new() { Name = "Stable Diffusion", Category = "AI Image Generators", PriceINR = 0, IsFree = true, WebsiteURL = "https://stability.ai", Description = "Open-source image generation model with extensive customization options.", LogoURL = "" },
                new() { Name = "Adobe Firefly", Category = "AI Image Generators", PriceINR = 1700, IsFree = false, WebsiteURL = "https://firefly.adobe.com", Description = "Adobe's generative AI designed for safe commercial use with Creative Cloud.", LogoURL = "" },
                new() { Name = "Leonardo.AI", Category = "AI Image Generators", PriceINR = 1200, IsFree = false, WebsiteURL = "https://leonardo.ai", Description = "AI image generation platform popular among game developers and artists.", LogoURL = "" },
                new() { Name = "Runway ML", Category = "AI Video Tools", PriceINR = 1200, IsFree = false, WebsiteURL = "https://runwayml.com", Description = "AI-powered video editing and generation platform for creatives.", LogoURL = "" },
                new() { Name = "Synthesia", Category = "AI Video Tools", PriceINR = 2490, IsFree = false, WebsiteURL = "https://synthesia.io", Description = "Create professional AI videos with avatars in minutes without cameras.", LogoURL = "" },
                new() { Name = "HeyGen", Category = "AI Video Tools", PriceINR = 2490, IsFree = false, WebsiteURL = "https://heygen.com", Description = "AI video generation platform for creating personalized video at scale.", LogoURL = "" },
                new() { Name = "Invideo AI", Category = "AI Video Tools", PriceINR = 2000, IsFree = false, WebsiteURL = "https://invideo.io", Description = "Text-to-video AI tool for marketing and social media content.", LogoURL = "" },
                new() { Name = "Pictory", Category = "AI Video Tools", PriceINR = 1500, IsFree = false, WebsiteURL = "https://pictory.ai", Description = "Transform blog posts and scripts into engaging short videos automatically.", LogoURL = "" },
                new() { Name = "GitHub Copilot", Category = "AI Code Assistants", PriceINR = 830, IsFree = false, WebsiteURL = "https://github.com/features/copilot", Description = "AI pair programmer that suggests code completions in your IDE.", LogoURL = "" },
                new() { Name = "Cursor", Category = "AI Code Assistants", PriceINR = 1660, IsFree = false, WebsiteURL = "https://cursor.sh", Description = "AI-first code editor with GPT-4 integration for faster development.", LogoURL = "" },
                new() { Name = "Tabnine", Category = "AI Code Assistants", PriceINR = 830, IsFree = false, WebsiteURL = "https://tabnine.com", Description = "AI code completion tool supporting 30+ programming languages.", LogoURL = "" },
                new() { Name = "Codeium", Category = "AI Code Assistants", PriceINR = 0, IsFree = true, WebsiteURL = "https://codeium.com", Description = "Free AI coding assistant with autocomplete and chat features.", LogoURL = "" },
                new() { Name = "Replit AI", Category = "AI Code Assistants", PriceINR = 830, IsFree = false, WebsiteURL = "https://replit.com", Description = "Cloud-based IDE with AI assistance for coding and deployment.", LogoURL = "" },
                new() { Name = "Jasper AI", Category = "AI Writing Tools", PriceINR = 3300, IsFree = false, WebsiteURL = "https://jasper.ai", Description = "AI content platform for marketing teams to create brand-consistent content.", LogoURL = "" },
                new() { Name = "Copy.ai", Category = "AI Writing Tools", PriceINR = 3300, IsFree = false, WebsiteURL = "https://copy.ai", Description = "AI marketing copy tool for emails, ads, social media, and blog posts.", LogoURL = "" },
                new() { Name = "Writesonic", Category = "AI Writing Tools", PriceINR = 1250, IsFree = false, WebsiteURL = "https://writesonic.com", Description = "AI writer and chatbot for SEO-optimized content creation.", LogoURL = "" },
                new() { Name = "Grammarly", Category = "AI Writing Tools", PriceINR = 0, IsFree = true, WebsiteURL = "https://grammarly.com", Description = "AI writing assistant for grammar, clarity, and tone improvements.", LogoURL = "" },
                new() { Name = "Notion AI", Category = "AI Writing Tools", PriceINR = 830, IsFree = false, WebsiteURL = "https://notion.so/product/ai", Description = "AI writing assistant integrated into Notion workspace.", LogoURL = "" },
                new() { Name = "ElevenLabs", Category = "AI Voice & Audio", PriceINR = 1660, IsFree = false, WebsiteURL = "https://elevenlabs.io", Description = "Hyper-realistic AI voice synthesis and text-to-speech platform.", LogoURL = "" },
                new() { Name = "Murf AI", Category = "AI Voice & Audio", PriceINR = 1660, IsFree = false, WebsiteURL = "https://murf.ai", Description = "Professional AI voice-over generator for videos and presentations.", LogoURL = "" },
                new() { Name = "Descript", Category = "AI Voice & Audio", PriceINR = 1250, IsFree = false, WebsiteURL = "https://descript.com", Description = "All-in-one audio/video editor with AI transcription and voice cloning.", LogoURL = "" },
                new() { Name = "Whisper", Category = "AI Voice & Audio", PriceINR = 0, IsFree = true, WebsiteURL = "https://openai.com/research/whisper", Description = "OpenAI's open-source automatic speech recognition system.", LogoURL = "" },
                new() { Name = "Resemble AI", Category = "AI Voice & Audio", PriceINR = 830, IsFree = false, WebsiteURL = "https://resemble.ai", Description = "AI voice cloning and real-time voice conversion platform.", LogoURL = "" },
                new() { Name = "Suno", Category = "AI Music Generators", PriceINR = 830, IsFree = false, WebsiteURL = "https://suno.com", Description = "Create complete songs with vocals from a simple text prompt.", LogoURL = "" },
                new() { Name = "Udio", Category = "AI Music Generators", PriceINR = 0, IsFree = true, WebsiteURL = "https://udio.com", Description = "AI music generation tool for creating unique tracks in any genre.", LogoURL = "" },
                new() { Name = "Soundraw", Category = "AI Music Generators", PriceINR = 1660, IsFree = false, WebsiteURL = "https://soundraw.io", Description = "AI music composer for royalty-free background music.", LogoURL = "" },
                new() { Name = "AIVA", Category = "AI Music Generators", PriceINR = 1660, IsFree = false, WebsiteURL = "https://aiva.ai", Description = "AI composer for emotional soundtracks and music production.", LogoURL = "" },
                new() { Name = "Mubert", Category = "AI Music Generators", PriceINR = 830, IsFree = false, WebsiteURL = "https://mubert.com", Description = "AI-generated royalty-free music streaming and licensing platform.", LogoURL = "" },
                new() { Name = "Salesforce Einstein", Category = "AI Business Tools", PriceINR = 8300, IsFree = false, WebsiteURL = "https://salesforce.com/products/einstein", Description = "AI-powered CRM analytics and automation for sales and marketing.", LogoURL = "" },
                new() { Name = "HubSpot AI", Category = "AI Business Tools", PriceINR = 4150, IsFree = false, WebsiteURL = "https://hubspot.com", Description = "AI-powered CRM platform for marketing, sales, and customer service.", LogoURL = "" },
                new() { Name = "Zapier AI", Category = "AI Business Tools", PriceINR = 1660, IsFree = false, WebsiteURL = "https://zapier.com", Description = "AI automation platform connecting thousands of apps without coding.", LogoURL = "" },
                new() { Name = "Otter.ai", Category = "AI Business Tools", PriceINR = 830, IsFree = false, WebsiteURL = "https://otter.ai", Description = "AI meeting assistant for real-time transcription and summaries.", LogoURL = "" },
                new() { Name = "Fireflies.ai", Category = "AI Business Tools", PriceINR = 1250, IsFree = false, WebsiteURL = "https://fireflies.ai", Description = "AI notetaker for meetings across Zoom, Teams, and Google Meet.", LogoURL = "" },
                new() { Name = "Notion", Category = "AI Productivity", PriceINR = 0, IsFree = true, WebsiteURL = "https://notion.so", Description = "All-in-one workspace for notes, docs, and project management with AI.", LogoURL = "" },
                new() { Name = "Mem.ai", Category = "AI Productivity", PriceINR = 1660, IsFree = false, WebsiteURL = "https://mem.ai", Description = "AI-powered personal knowledge base that organizes itself automatically.", LogoURL = "" },
                new() { Name = "Reclaim.ai", Category = "AI Productivity", PriceINR = 830, IsFree = false, WebsiteURL = "https://reclaim.ai", Description = "AI scheduling assistant that protects focus time and automates calendar.", LogoURL = "" },
                new() { Name = "Clickup AI", Category = "AI Productivity", PriceINR = 415, IsFree = false, WebsiteURL = "https://clickup.com", Description = "Project management platform with built-in AI for tasks and docs.", LogoURL = "" },
                new() { Name = "Motion", Category = "AI Productivity", PriceINR = 1660, IsFree = false, WebsiteURL = "https://usemotion.com", Description = "AI task manager that automatically plans your day and schedule.", LogoURL = "" },
                new() { Name = "Canva AI", Category = "AI Design Tools", PriceINR = 3999, IsFree = false, WebsiteURL = "https://canva.com", Description = "Design platform with Magic AI tools for image generation and editing.", LogoURL = "" },
                new() { Name = "Figma AI", Category = "AI Design Tools", PriceINR = 1250, IsFree = false, WebsiteURL = "https://figma.com", Description = "Collaborative design tool with AI features for UI/UX workflows.", LogoURL = "" },
                new() { Name = "Uizard", Category = "AI Design Tools", PriceINR = 1250, IsFree = false, WebsiteURL = "https://uizard.io", Description = "AI design tool for wireframes and prototypes from text descriptions.", LogoURL = "" },
                new() { Name = "Locofy.ai", Category = "AI Design Tools", PriceINR = 1660, IsFree = false, WebsiteURL = "https://locofy.ai", Description = "Convert Figma and Adobe XD designs to production-ready frontend code.", LogoURL = "" },
                new() { Name = "Khroma", Category = "AI Design Tools", PriceINR = 0, IsFree = true, WebsiteURL = "https://khroma.co", Description = "AI color tool that learns your color preferences to generate palettes.", LogoURL = "" },
            };
            context.AIWebsites.AddRange(websites);
            await context.SaveChangesAsync();
        }
    }
}
