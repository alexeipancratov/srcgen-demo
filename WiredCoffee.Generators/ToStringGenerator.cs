using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WiredCoffee.Generators;

[Generator]
public class ToStringGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // static lambdas are recommended to avoid using closures
        var classes = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node);
            
        context.RegisterSourceOutput(classes,
            static (ctx, source) => Execute(ctx, source));
    }

    private static void Execute(SourceProductionContext context, ClassDeclarationSyntax classDeclarationSyntax)
    {
        const string namespaceName = "NotYetImplemented";
        var className = classDeclarationSyntax.Identifier.Text;
        var fileName = $"{namespaceName}.{className}.g.cs"; // .g. is a convention for generated files in C#
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($@"namespace {namespaceName}
{{
    public partial class {className}
    {{
        public override string ToString()
        {{
            return ""Not yet implemented"";
        }}
    }}
}}");
        
        context.AddSource(fileName, stringBuilder.ToString());
    }
}