using System.Reflection;

namespace TennisTour.Application.Services.Impl;

public class TemplateService : ITemplateService
{
    private readonly string _templatesPath;

    public TemplateService()
    {
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        var templateProject = Assembly.GetExecutingAssembly().GetName().Name;

        _templatesPath = Path.Combine(projectPath, templateProject, "Templates");
    }

    public async Task<string> GetTemplateAsync(string templateName)
    {
        using var reader = new StreamReader(Path.Combine(_templatesPath, templateName));

        return await reader.ReadToEndAsync();
    }

    public string ReplaceInTemplate(string input, IDictionary<string, string> replaceWords)
    {
        foreach (var temp in replaceWords)
            input = input.Replace(temp.Key, temp.Value);

        return input;
    }
}
