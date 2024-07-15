using BlazorMonaco.Editor;
using CodeRank.Web.Entities;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;
using System.Text;

namespace CodeRank.Web.Pages;

public partial class CodeExecutor
{

 
    private string Code { get; set; } = @"
using System;
using System.Linq;

public class Program
{
    public static void Main(string args)
    {
        Console.WriteLine(args + ""Recived"");
    }
}
";
  
    private string Output { get; set; } = "";
    private string CommandLineArgs { get; set; } = "Hello CodeRank 123";
    private StandaloneCodeEditor _editor;
    private bool AllTestsPassed => TestCases.All(tc => tc.Passed);
    private int FailedTestCount => TestCases.Count(tc => !tc.Passed);
    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout = true,
            Language = "csharp",
            Value = Code
        };
    }
    private List<TestCase> TestCases { get; set; } = new List<TestCase>
{
    new TestCase {
        Name = "Sample Test case 0",
        Input = "4\n0 2 3 0",
        ExpectedOutput = "23"
    },
    new TestCase {
        Name = "Sample Test case 1",
        Input = "5\n1 1 1 1 1",
        ExpectedOutput = "5"
    },
    // Add more test cases as needed
};

    private static readonly List<string> AllowedNamespaces = new List<string>
{
    "System",
    "System.Linq",
    "System.Collections.Generic",
    "System.Text",
    // Add more allowed namespaces here
};

    private static readonly List<Assembly> AllowedAssemblies = new List<Assembly>
{
    typeof(System.Console).Assembly,
    typeof(System.Linq.Enumerable).Assembly,
    typeof(System.Collections.Generic.List<>).Assembly,
    // Add more allowed assemblies here
};

    private async Task<string> RunCode(string input)
    {
        try
        {
            var code = await _editor.GetValue();
            var usingStatements = ParseUsingStatements(code);

            // Remove using statements from the code
          //  code = RemoveUsingStatements(code);

         

            var outputBuilder = new StringBuilder();
            var inputReader = new StringReader(input);


            var options = ScriptOptions.Default
           .AddImports(AllowedNamespaces
           .Intersect(usingStatements))
           .AddReferences(AllowedAssemblies);


            // string wrappedCode = $"{Code} Program.Main(@\"{CommandLineArgs}\");";
            string wrappedCode = $@" {code.Replace("Console.", "CustomConsole.")}
            public class CustomConsole
            {{
                public static System.IO.TextReader InputReader;
                public static System.IO.TextWriter OutputWriter;

                public static string ReadLine()
                {{
                    return InputReader.ReadLine();
                }}

                public static void WriteLine(object s)
                {{
                    OutputWriter.WriteLine(s.ToString());
                }}
            }}
            CustomConsole.InputReader = new System.IO.StringReader(@""{input.Replace("\"", "\"\"")}"");
            CustomConsole.OutputWriter = new System.IO.StringWriter();
            Program.Main(null);
            return ((System.IO.StringWriter)CustomConsole.OutputWriter).ToString();
        ";




            var result = await CSharpScript.EvaluateAsync<string>(wrappedCode, options); 

            return result.Trim();
        }
        catch (CompilationErrorException ex)
        {
            return string.Join("\n", ex.Diagnostics);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    private bool IsRunningTests { get; set; } = false;
    private async Task RunTestCases()
    {
        try
        {
            IsRunningTests = true;
            StateHasChanged(); // Trigger UI update
            foreach (var testCase in TestCases)
            {
                testCase.ActualOutput = await RunCode(testCase.Input);
                testCase.Passed = testCase.ActualOutput.Trim() == testCase.ExpectedOutput.Trim();
            }
        } 
        finally
        {
            IsRunningTests = false;
            StateHasChanged(); // Trigger UI update again
        }
       
       
    }


    private List<string> ParseUsingStatements(string code)
    {
        var usingStatements = new List<string>();
        var lines = code.Split('\n');
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("using ") && trimmedLine.EndsWith(";"))
            {
                var namespaceName = trimmedLine.Substring(6, trimmedLine.Length - 7).Trim();
                usingStatements.Add(namespaceName);
            }
        }
        return usingStatements;
    }

    private string RemoveUsingStatements(string code)
    {
        var lines = code.Split('\n');
        var nonUsingLines = lines.Where(line => !line.Trim().StartsWith("using "));
        return string.Join("\n", nonUsingLines);
    }
}

